using BLL.Services_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lume.Models;
using System.IO;
using Lume.Infrastructure.Mappers;
using System.ComponentModel.DataAnnotations;

namespace Lume.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;
        private readonly IResourceService _resourceService;

        public HomeController(IUserService userService, IRatingService ratingService, IResourceService resourceService)
        {
            _userService = userService;
            _ratingService = ratingService;
            _resourceService = resourceService;
        }


        public ActionResult Index()
        {
            Session["userId"] = _userService.GetByEmail(User.Identity.Name).Id;
            return View();
        }

        public ActionResult GetMostPopular()
        {
            var all_resources = _resourceService.GetAllEntities().ToList();
            var all_ratings = _ratingService.GetAllEntities().ToList();
            int max_view = 0;
            if (all_ratings.Count > 0)
                max_view = all_resources.Max(res => res.Views);
            var average_rating = all_ratings.Sum(rat => rat.Mark) / all_ratings.Count;

            all_resources.Sort((r1, r2) =>
            {
                var pop1 = GetPopularity(r1.ToMvcResource().Rating, r1.Views, max_view, average_rating);
                var pop2 = GetPopularity(r2.ToMvcResource().Rating, r2.Views, max_view, average_rating);
                return pop1.CompareTo(pop2);
            });
            var most_popular = all_resources.Take(3).Select(res => res.ToMvcResource());
            return PartialView("_GetMostPopular", most_popular);
        }

        private double GetPopularity(double mark, double view, double max_views, double global_rating)
        {

            var w = max_views > 0 ? view / max_views : 1;
            return w * mark + (1 - w) * global_rating;
        }


        public ActionResult GetAllResource()
        {
            var resources = _resourceService.GetAllEntities().Select(res => res.ToMvcResource());
            return PartialView("_GetAllResource", resources);
        }

        [HttpPost]
        public ActionResult ResourceView(int id_resource)
        {
            var current_resource = _resourceService.GetEntitieById(id_resource);
            current_resource.Views++;
            _resourceService.Update(current_resource);
            var mvc_resource = current_resource.ToMvcResource();
            mvc_resource.DownloadFile = null;
            var isRated = _ratingService.GetFirstByPredicate(rat => rat.id_Users == (int)Session["userId"] && rat.id_Resource == id_resource) != null;
            return Json(new object[] { mvc_resource, isRated });
        }

        public ActionResult Upload()
        {
            return PartialView("_Upload");
        }

        [HttpPost]
        public ActionResult Upload(ResourceViewModel resource)
        {
            string fileType = string.Empty;
            var enumerator = resource.UploadFile.FileName.Reverse().GetEnumerator();
            try
            {
                while (enumerator.MoveNext() && enumerator.Current != '.')
                {
                    var item = enumerator.Current;
                    fileType += item;
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
            fileType = new string((fileType + ".").Reverse().ToArray());
            resource.Name += fileType;
            switch (fileType)
            {
                case ".mp4":
                case ".avi": 
                case ".mkv":{
                    resource.TypeResource = TypeResource.Video;
                    break;
                }
                case ".mp3":
                    {
                        resource.TypeResource = TypeResource.Audio;
                        break;
                    }
                case".txt":
                case".doc":
                case ".docx":
                    {
                        resource.TypeResource = TypeResource.Text;
                        break;
                    }
                default:
                    {
                        ModelState.AddModelError("UploadFile", "Incorrect file extension.");
                        break;
                    }

            }
            resource.id_User = (int)Session["userId"];
            _resourceService.Create(resource.ToBllResource());
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(int id_resource)
        {
            var current_resource = _resourceService.GetEntitieById(id_resource).ToMvcResource();
            return PartialView("_Edit", current_resource);
        }
        [HttpPost]
        public ActionResult Edit(ResourceViewModel model)
        {
            var current_resource = _resourceService.GetEntitieById(model.Id);
            current_resource.Name = model.Name;
            current_resource.Description = model.Description;
            _resourceService.Update(current_resource);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Search()
        {

            return View();
        }

        public FileResult Download(int id_resource)
        {
            var resource = _resourceService.GetEntitieById(id_resource);
            return File(resource.File, System.Net.Mime.MediaTypeNames.Application.Octet, resource.Name);
        }

        public ActionResult Rating(double mark, int id_resource)
        {
            //var rating_mark = mark.Replace('.', ',');
            //var d = Convert.ToDouble(rating_mark);
            RatingViewModel rat = new RatingViewModel()
            {
                Date = DateTime.Now,
                id_Resource = id_resource,
                id_Users = (int)Session["userId"],
                Mark = mark
            };
            _ratingService.Create(rat.ToBllRating());
            var resource = _resourceService.GetEntitieById(id_resource).ToMvcResource();
            return Json(resource.Rating);
        }

        public ActionResult Remove(int id_resource)
        {
            var ratings = _ratingService.GetByResource(id_resource).ToList();
            foreach (var rat in ratings)
            {
                _ratingService.Delete(rat);
            }
            _resourceService.Delete(_resourceService.GetEntitieById(id_resource));

            return null;
        }
    }

}
