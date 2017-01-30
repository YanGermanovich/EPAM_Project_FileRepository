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
using System.Linq.Expressions;
using BLL.Entities;
using System.Web.Routing;
using NLog;

namespace Lume.Controllers
{
    [HandleError] 
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;
        private readonly IResourceService _resourceService;
        private readonly ILogger _logger;

        public HomeController(IUserService userService, IRatingService ratingService, IResourceService resourceService, ILogger logger)
        {
            _userService = userService;
            _ratingService = ratingService;
            _resourceService = resourceService;
            _logger = logger;
        }


        public ActionResult Index()
        {
            Session["search"] = null;
            var resourcesCount = _resourceService.GetAllEntities().Count() / 6;
            var pagination = new PaginationViewModel()
            {
                ActionName = "Index",
                PagesCount = resourcesCount + 1,
                CurrentPage = 1
            };
            if (Request.RequestContext.RouteData.Values.Any(val => val.Key == "id"))
            {
                pagination.CurrentPage = Convert.ToInt32(Request.RequestContext.RouteData.Values["id"]);
            }
            Session["userId"] = _userService.GetByEmail(User.Identity.Name).Id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Index", pagination);
            }
            return View(pagination);
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


        public ActionResult DataTable(PaginationViewModel model)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_DataTable", model);
            }
            return PartialView("_DataTable", model);
        }

        public ActionResult GetAllResource(int? currentPage)
        {
            var resources = _resourceService.GetAllEntities().Select(res => res.ToMvcResource()).ToList();
            if (Session["search"] != null)
                resources = resources.Where(((SearchViewModel) Session["search"]).GetPredicate()).ToList();
            if (currentPage != null)
            {
                var n = (int)(currentPage - 1) * 5;
                var skip = resources.Skip(n).ToList();
                resources = skip.Take(5).ToList();
            }
            return PartialView("_GetAllResource", resources);
        }

        public ActionResult ResourceView(int id_resource)
        {
            var current_resource = _resourceService.GetEntitieById(id_resource);
            current_resource.Views++;
            _resourceService.Update(current_resource);
            var mvc_resource = current_resource.ToMvcResource();
            mvc_resource.DownloadFile = null;
            var isRated = _ratingService.GetFirstByPredicate(rat => rat.id_Users == (int)Session["userId"] && rat.id_Resource == id_resource) != null;
            if (Request.IsAjaxRequest())
                return Json(new object[] { mvc_resource, isRated });
            else
                return View("ResourceView", new object[] { mvc_resource, isRated });
        }

        public ActionResult PartialUpload()
        {
            return PartialView("_Upload");
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string Name, string Description, HttpPostedFileBase UploadFile)
        {
            var resource = new ResourceViewModel();
            resource.Name = Name;
            resource.Description = Description;
            resource.UploadFile = UploadFile;

            if (Request.IsAjaxRequest())
            {
                resource.UploadFile = Request.Files["file"];
            }
            var result = UploadHelper(resource);
            if (result != null)
                return result;

            if (Request.IsAjaxRequest())
                return null;
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult UploadModel(ResourceViewModel resource)
        {

            if (resource.UploadFile == null)
                ModelState.AddModelError("UploadFile", "Pleas enter file");
            if (ModelState.IsValid)
            {
                var result = UploadHelper(resource);
                if (result != null)
                    return result;
            }
            else
            {
                return View("Upload");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id_resource)
        {
            var current_resource = _resourceService.GetEntitieById(id_resource).ToMvcResource();
            if (Request.IsAjaxRequest())
                return PartialView("_Edit", current_resource);
            else
                return View(current_resource);
        }

        [HttpPost]
        public ActionResult Edit(ResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var current_resource = _resourceService.GetEntitieById(model.Id);
                current_resource.Name = model.Name;
                current_resource.Description = model.Description;
                _resourceService.Update(current_resource);
                _logger.Info(String.Format("User {0}, edited file: '{1}'", User.Identity.Name, model.Name));
                return RedirectToAction("Index", "Home");
            }

            if (Request.IsAjaxRequest())
                return RedirectToAction("Index", "Home");
            else
                return View(model);
        }

        public ActionResult Search()
        {
            var resources = _resourceService.GetAllEntities().Select(res => res.ToMvcResource()).ToList();
            var resourcesCount = _resourceService.GetAllEntities().Count() / 6;
            if (Session["search"] != null)
                resourcesCount = resources.Where(((SearchViewModel)Session["search"]).GetPredicate()).Count() / 6;
            var pagination = new PaginationViewModel()
            {
                ActionName = "Search",
                PagesCount = resourcesCount + 1,
                CurrentPage = 1
            };
            if (Request.RequestContext.RouteData.Values.Any(val => val.Key == "id"))
            {
                pagination.CurrentPage = Convert.ToInt32(Request.RequestContext.RouteData.Values["id"]);
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Search", pagination);
            }
            return View(pagination);
        }

        public ActionResult SearchForm()
        {
            if (Session["search"] != null)
                return PartialView("_SearchForm", (SearchViewModel)Session["search"]);
            else
                return PartialView("_SearchForm");
        }

        [HttpPost]
        public ActionResult SearchForm(SearchViewModel model)
        {
            Session["search"] = model;
            if (Request.IsAjaxRequest())
            {
                var resources = _resourceService.GetAllEntities().Select(res => res.ToMvcResource()).ToList();
                var resourcesCount = _resourceService.GetAllEntities().Count() / 6;
                if (Session["search"] != null)
                    resourcesCount = resources.Where(((SearchViewModel)Session["search"]).GetPredicate()).Count() / 6;
                var pagination = new PaginationViewModel()
                {
                    ActionName = "Search",
                    PagesCount = resourcesCount + 1,
                    CurrentPage = 1
                };
                return DataTable(pagination);
            }
            else
            {
                return RedirectToAction("Search", "Home");
            }
        }

        public FileResult Download(int id_resource)
        {
            var resource = _resourceService.GetEntitieById(id_resource);
            _logger.Info(String.Format("User {0}, downloaded file: '{1}'", User.Identity.Name, _resourceService.GetEntitieById(id_resource).Name));
            return File(resource.File, System.Net.Mime.MediaTypeNames.Application.Octet, resource.Name);
        }

        [ChildActionOnly]
        public ActionResult Rating(int id_resource, double mark)
        {
            return PartialView("_Rating", new RatingResourceViewModel() { id_resource = id_resource, mark = mark });
        }
        [HttpPost]
        public ActionResult Rating(RatingResourceViewModel model)
        {
            ResourceViewModel resource = null;
            if (ModelState.IsValid)
            {
                RatingViewModel rat = new RatingViewModel()
                {
                    Date = DateTime.Now,
                    id_Resource = model.id_resource,
                    id_Users = (int)Session["userId"],
                    Mark = (double)model.mark
                };
                _ratingService.Create(rat.ToBllRating());
                resource = _resourceService.GetEntitieById(model.id_resource).ToMvcResource();
            }
            if (Request.IsAjaxRequest())
                return Json(resource.Rating);
            else
                return RedirectToAction("ResourceView", "Home", new { id_resource = model.id_resource });
        }

        public ActionResult RemoveView(int id_resource)
        {
            return View(_resourceService.GetEntitieById(id_resource).ToMvcResource());
        }

        public ActionResult Remove(int id_resource)
        {
            var ratings = _ratingService.GetByResource(id_resource).ToList();
            foreach (var rat in ratings)
            {
                _ratingService.Delete(rat);
            }
            _resourceService.Delete(_resourceService.GetEntitieById(id_resource));
            _logger.Info(String.Format("User {0}, remove file: '{1}'", User.Identity.Name, _resourceService.GetEntitieById(id_resource).Name));
            if (Request.IsAjaxRequest())
                return null;
            else
                return RedirectToAction("Index", "Home");
        }

        private ActionResult UploadHelper(ResourceViewModel resource)
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
                case ".mkv":
                    {
                        resource.TypeResource = TypeResource.Video;
                        break;
                    }
                case ".mp3":
                    {
                        resource.TypeResource = TypeResource.Audio;
                        break;
                    }
                case ".txt":
                case ".doc":
                case ".docx":
                    {
                        resource.TypeResource = TypeResource.Text;
                        break;
                    }
                default:
                    {

                        ModelState.AddModelError("UploadFile", "Incorrect file extension.");
                        if (Request.IsAjaxRequest())
                            return PartialView("_Upload");
                        else
                            return View();
                    }

            }
            resource.id_User = (int)Session["userId"];
            _resourceService.Create(resource.ToBllResource());
            _logger.Info(String.Format("User {0}, uploaded file: '{1}'", User.Identity.Name,resource.Name));
            return null;
        }
        private double GetPopularity(double mark, double view, double max_views, double global_rating)
        {

            var w = max_views > 0 ? view / max_views : 1;
            return w * mark + (1 - w) * global_rating;
        }
    }

}
