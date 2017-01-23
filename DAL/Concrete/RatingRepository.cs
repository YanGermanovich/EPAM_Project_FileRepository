using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.Interface;
using DAL.DalToOrmMappers;
using System.Data.Entity;
using ORM;
using CustomExpressionVisitor;
using DAL.Helpers;

namespace DAL.Concrete
{
    public class RatingRepository: IRatingRepository
    {
        private readonly DbContext _context;

        #region Public Methods
        public RatingRepository(DbContext uow)
        {
            _context = uow;
        }

        public DalRating GetById(int key)
        {
            return GetFirstByPredicate(rating => rating.Id == key);
        }

        public IEnumerable<DalRating> GetAll()
        {
            return _context.Set<Rating>().ToList().Select(rating => rating.ToDalRating());
        }
        #region OldImplementation
        
        //public DalRating GetById(int key)
        //{
        //    var currentRating = _context.Set<Rating>().FirstOrDefault(rating => rating.Id == key);
        //    return currentRating.ToDalRating();
        //}
        
        //public DalRating GetByMark(double mark)
        //{
        //    var currentRating = _context.Set<Rating>().FirstOrDefault(rating => rating.Ratings_Mark == mark);
        //    return currentRating.ToDalRating();
        //}
        //public DalRating GetByComment(string comment)
        //{
        //    var currentRating = _context.Set<Rating>().FirstOrDefault(rating => rating.Ratings_Comment == comment);
        //    return currentRating.ToDalRating();
        //}
        //public DalRating GetByUser(int id_user)
        //{
        //    var currentRating = _context.Set<Rating>().FirstOrDefault(rating => rating.Ratings_id_Users == id_user);
        //    return currentRating.ToDalRating();
        //}
        //public DalRating GetByResource(int id_resource)
        //{
        //    var currentRating = _context.Set<Rating>().FirstOrDefault(rating => rating.Ratings_id_Resource == id_resource);
        //    return currentRating.ToDalRating();
        //}
        //public DalRating GetByDate(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion

        public DalRating GetFirstByPredicate(System.Linq.Expressions.Expression<Func<DalRating, bool>> f)
        {
            var criteria = ConvertExpression<DalRating, Rating>.Convert(f,"OrmRating");
            var response = _context.Set<Rating>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDalRating();
        }

         public IEnumerable<DalRating> GetAllByPredicate(System.Linq.Expressions.Expression<Func<DalRating, bool>> f)
        {
            var criteria = ConvertExpression<DalRating, Rating>.Convert(f, "OrmRating");
            var result_list = new List<DalRating>();
            return _context.Set<Rating>().Where(criteria.Compile()).ToList().Select(rating => rating.ToDalRating());
        }
        
        public void Create(DalRating e)
        {
            var rating = e.ToOrmRating();
            _context.Set<Rating>().Add(rating);
        }
        public void Delete(DalRating e)
        {
            var rating = e.ToOrmRating();
            rating = _context.Set<Rating>().Single(r => r.Id == e.Id);
            _context.Set<Rating>().Remove(rating);
        }
        public void Update(DalRating entity)
        {
            var rating = entity.ToOrmRating();
            var ratingToUpdate = _context.Set<Rating>().Single(r => r.Id == entity.Id);
            entity.CopyPropertiesTo(ratingToUpdate);
        }

        #endregion



        
    }
}
