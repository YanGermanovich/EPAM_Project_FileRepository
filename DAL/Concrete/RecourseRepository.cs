using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.DalToOrmMappers;
using DAL.DTO;
using System.Data.Entity;
using ORM;
using CustomExpressionVisitor;
using DAL.Helpers;

namespace DAL.Concrete
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly DbContext _context;

        #region Public Methods
        public ResourceRepository(DbContext uow)
        {
            _context = uow;
        }

        #region OldImplementation

        //public DalResource GetByUser(int id_user)
        //{
        //    var currentResource = context.Set<Resource>().FirstOrDefault(resource => resource.Resource_id_User == id_user);
        //    return currentResource.ToDalResource();
        //}

        //public DalResource GetByTypeResource(int id_typeResource)
        //{
        //    var currentResource = context.Set<Resource>().FirstOrDefault(resource => resource.Resource_id_TypeResource == id_typeResource);
        //    return currentResource.ToDalResource();
        //}

        //public DalResource GetByFile(byte[] file)
        //{
        //    var currentResource = context.Set<Resource>().FirstOrDefault(resource => resource.Resource_File.Equals(file));
        //    return currentResource.ToDalResource();
        //}

        //public DalResource GetByDescription(string description)
        //{
        //    var currentResource = context.Set<Resource>().FirstOrDefault(resource => resource.Resource_Description == description);
        //    return currentResource.ToDalResource();
        //}

        //public DalResource GetById(int key)
        //{
        //    var currentResource = context.Set<Resource>().FirstOrDefault(resource => resource.Id == key);
        //    return currentResource.ToDalResource();
        //}

        #endregion

        public DalResource GetById(int key)
        {
            return GetFirstByPredicate(resource => resource.Id == key);
        }

        public IEnumerable<DalResource> GetAll()
        {
            return _context.Set<Resource>().ToList().Select(resource => resource.ToDalResource());
        }


        public DalResource GetFirstByPredicate(System.Linq.Expressions.Expression<Func<DalResource, bool>> f)
        {
             var criteria = ConvertExpression<DalResource, Resource>.Convert(f, "OrmResource");
             var response = _context.Set<Resource>().FirstOrDefault(criteria.Compile());
             return response == null ? null : response.ToDalResource();
        }

        public IEnumerable<DalResource> GetAllByPredicate(System.Linq.Expressions.Expression<Func<DalResource, bool>> f)
        {
            var criteria = ConvertExpression<DalResource, Resource>.Convert(f,"OrmResource");
            return _context.Set<Resource>().Where(criteria.Compile()).ToList().Select(resource => resource.ToDalResource());
        }
        public void Create(DalResource e)
        {
            var resource = e.ToOrmResource();
            _context.Set<Resource>().Add(resource);
        }

        public void Delete(DalResource e)
        {
            var resource = e.ToOrmResource();
            resource = _context.Set<Resource>().Single(r => r.Id == e.Id);
            _context.Set<Resource>().Remove(resource);
        }

        public void Update(DalResource entity)
        {
            var resource = entity.ToOrmResource();
            var resourceToUpdate = _context.Set<Resource>().Single(r => r.Id == entity.Id);
            entity.CopyPropertiesTo(resourceToUpdate);
        }
        #endregion
    }
}
