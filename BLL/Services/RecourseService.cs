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
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IResourceRepository _resourceRepository;

        #region Public Methods
        public ResourceService(IUnitOfWork uow, IResourceRepository repository)
        {
            _uow = uow;
            _resourceRepository = repository;
        }

        public IEnumerable<ResourceEntity> GetByUser(int id_user)
        {
            return GetAllByPredicate(user => user.id_User == id_user);
        }

        public IEnumerable<ResourceEntity> GetByTypeResource(int id_typeResource)
        {
            return GetAllByPredicate(user => user.id_TypeResource == id_typeResource);
        }

        public IEnumerable<ResourceEntity> GetByFile(byte[] file)
        {
            return GetAllByPredicate(user => user.File == file);
        }

        public IEnumerable<ResourceEntity> GetByDescription(string description)
        {
            return GetAllByPredicate(user => user.Description == description);
        }

        public IEnumerable<Entities.ResourceEntity> GetAllEntities()
        {
            return _resourceRepository.GetAll().Select(resource => resource.ToBllResource());
        }

        public ResourceEntity GetEntitieById(int key)
        {
            var response = _resourceRepository.GetById(key);
            return response == null ? null : response.ToBllResource();
        }

        public ResourceEntity GetFirstByPredicate(System.Linq.Expressions.Expression<Func<ResourceEntity, bool>> f)
        {
            var criteria = ConvertExpression<ResourceEntity, DalResource>.Convert(f, "DalResource");
            var response =  _resourceRepository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBllResource();
        }

        public IEnumerable<ResourceEntity> GetAllByPredicate(System.Linq.Expressions.Expression<Func<ResourceEntity, bool>> f)
        {
            var criteria = ConvertExpression<ResourceEntity,DalResource>.Convert(f, "DalResource");
            return _resourceRepository.GetAllByPredicate(criteria).Select(resource => resource.ToBllResource());
        }

        public void Create(Entities.ResourceEntity e)
        {
            _resourceRepository.Create(e.ToDalResource());
            _uow.Commit();
        }

        public void Delete(Entities.ResourceEntity e)
        {
            _resourceRepository.Delete(e.ToDalResource());
            _uow.Commit();
        }

        public void Update(ResourceEntity e)
        {
            _resourceRepository.Update(e.ToDalResource());
            _uow.Commit();
        }

        #endregion

    }
}
