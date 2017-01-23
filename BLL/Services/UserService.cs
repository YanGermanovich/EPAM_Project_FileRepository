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
using CustomExpressionVisitor;
using BLL.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        #region Public Methods
        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            _uow = uow;
            _userRepository = repository;
        }


        public UserEntity GetByEmail(string email)
        {
            return GetFirstByPredicate(user => user.Email == email);

        }

        public IEnumerable<UserEntity> GetByPassword(string password)
        {
            return GetAllByPredicate(user => user.Password == password);

        }

        public IEnumerable<UserEntity> GetByRole(int id_Role)
        {
            return GetAllByPredicate(user => user.id_Role == id_Role);
        }

        public IEnumerable<Entities.UserEntity> GetAllEntities()
        {
            return _userRepository.GetAll().Select(user => user.ToBllUser()); 
        }

        public Entities.UserEntity GetEntitieById(int key)
        {
            var response = _userRepository.GetById(key);
            return response == null ? null : response.ToBllUser();
        }

        public Entities.UserEntity GetFirstByPredicate(System.Linq.Expressions.Expression<Func<Entities.UserEntity, bool>> f)
        {
            var criteria = ConvertExpression<UserEntity, DalUser>.Convert(f, "DalUser");
            var response = _userRepository.GetFirstByPredicate(criteria);
            return response == null ? null : response.ToBllUser();
        }

        public IEnumerable <UserEntity> GetAllByPredicate(System.Linq.Expressions.Expression<Func<Entities.UserEntity, bool>> f)
        {
            var criteria = ConvertExpression<UserEntity, DalUser>.Convert(f, "DalUser");
            return _userRepository.GetAllByPredicate(criteria).Select(user=> user.ToBllUser());
        }

        public void Create(Entities.UserEntity e)
        {
            _userRepository.Create(e.ToDalUser());
            _uow.Commit();
        }

        public void Delete(Entities.UserEntity e)
        {
            _userRepository.Delete(e.ToDalUser());
            _uow.Commit();
        }

        public void Update(UserEntity e)
        {
            _userRepository.Update(e.ToDalUser());
            _uow.Commit();
        }

        #endregion
        
    }
}
