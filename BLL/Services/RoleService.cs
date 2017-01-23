using BLL.Entities;
using BLL.Services_Interface;
using CustomExpressionVisitor;
using DAL.DTO;
using DAL.Interface;
using ORM;
using BLL.BLLEntityToDalMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRolesRepository _roleRepository;

        #region Public Methods
        public RoleService(IUnitOfWork uow, IRolesRepository repository)
        {
            _uow = uow;
            _roleRepository = repository;
        }
        public RoleEntity GetByName(string Name)
        {
            return GetFirstByPredicate(role => role.Name == Name);
        }

        public RoleEntity GetFirstByPredicate(System.Linq.Expressions.Expression<Func<RoleEntity, bool>> f)
        {
            var criteria = ConvertExpression<RoleEntity, DalRole>.Convert(f, "DalRole");
            var response = _roleRepository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllByPredicate(System.Linq.Expressions.Expression<Func<RoleEntity, bool>> f)
        {
            var criteria = ConvertExpression<RoleEntity, DalRole>.Convert(f, "DalRole");
            return _roleRepository.GetAllByPredicate(criteria).Select(role => role.ToBllRole());
        }

        public RoleEntity GetEntitieById(int key)
        {
            var response = _roleRepository.GetById(key);
            return response == null ? null : response.ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllEntities()
        {
            return _roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        public void Create(RoleEntity e)
        {
            throw new NotImplementedException();
        }

        public void Delete(RoleEntity e)
        {
            throw new NotImplementedException();
        }

        public void Update(RoleEntity e)
        {
            _roleRepository.Update(e.ToDalRole());
            _uow.Commit();
        }

        #endregion


        
    }
}
