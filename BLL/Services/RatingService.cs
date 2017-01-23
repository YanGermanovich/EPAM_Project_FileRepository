using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services_Interface;
using DAL.DalToOrmMappers;
using DAL.DTO;
using DAL.Interface;
using BLL.BLLEntityToDalMappers;
using BLL.Entities;
using CustomExpressionVisitor;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRatingRepository _ratingRepository;

        #region Public Methods
        public RatingService(IUnitOfWork uow, IRatingRepository repository)
        {
            _uow = uow;
            _ratingRepository = repository;
        }

        public IEnumerable<RatingEntity> GetByMark(double mark)
        {
            return GetAllByPredicate(rating => rating.Mark == mark);
        }
        public IEnumerable<RatingEntity> GetByUser(int id_user)
        {
            return GetAllByPredicate(rating => rating.id_Users == id_user);

        }
        public IEnumerable<RatingEntity> GetByResource(int id_resource)
        {
            return GetAllByPredicate(rating => rating.id_Resource == id_resource);

        }
        public IEnumerable<RatingEntity> GetByDate(DateTime date)
        {
            return GetAllByPredicate(rating => rating.Date == date);
        }

        public IEnumerable<Entities.RatingEntity> GetAllEntities()
        {
            return _ratingRepository.GetAll().Select(rating => rating.ToBllRating());
        }

        public RatingEntity GetEntitieById(int key)
        {
            var response = _ratingRepository.GetById(key);
            return response == null ? null : response.ToBllRating();
        }

        public RatingEntity GetFirstByPredicate(System.Linq.Expressions.Expression<Func<RatingEntity, bool>> f)
        {
            var criteria = ConvertExpression<RatingEntity, DalRating>.Convert(f, "DalRating");
            var response = _ratingRepository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBllRating();
        }

        public IEnumerable<RatingEntity> GetAllByPredicate(System.Linq.Expressions.Expression<Func<RatingEntity, bool>> f)
        {
            var criteria = ConvertExpression<RatingEntity, DalRating>.Convert(f, "DalRating");
            return _ratingRepository.GetAllByPredicate(criteria).Select(rating => rating.ToBllRating());
        }

        public void Create(Entities.RatingEntity e)
        {
            _ratingRepository.Create(e.ToDalRating());
            _uow.Commit();
        }

        public void Delete(Entities.RatingEntity e)
        {
            _ratingRepository.Delete(e.ToDalRating());
            _uow.Commit();
        }

        public void Update(RatingEntity e)
        {
            _ratingRepository.Update(e.ToDalRating());
            _uow.Commit();
        }
        #endregion
    }
}
