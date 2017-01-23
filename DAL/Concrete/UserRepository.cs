using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using System.Data.Entity;
using DAL.DTO;
using DAL.DalToOrmMappers;
using ORM;
using CustomExpressionVisitor;
using DAL.Helpers;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        #region Public Methods
        public UserRepository(DbContext uow)
        {
            _context = uow;
        }
        #region OldImplementation
        //public DTO.DalUser GetByFirstName(string firstName)
        //{
        //    var currentUser = _context.Set<User>().FirstOrDefault(user => user.Users_FirstName == firstName);
        //    return currentUser.ToDalUser();
        //}

        //public DTO.DalUser GetByLastName(string lastName)
        //{
        //    var currentUser = _context.Set<User>().FirstOrDefault(user => user.Users_LastName == lastName);
        //    return currentUser.ToDalUser();
        //}

        //public DTO.DalUser GetByEmail(string email)
        //{
        //    var currentUser = _context.Set<User>().FirstOrDefault(user => user.Users_Email == email);
        //    return currentUser.ToDalUser();
        //}

        //public DTO.DalUser GetByLogin(string login)
        //{
        //    var currentUser = _context.Set<User>().FirstOrDefault(user => user.Users_Login == login);
        //    return currentUser.ToDalUser();
        //}

        //public DTO.DalUser GetByPassword(string password)
        //{
        //    var currentUser = _context.Set<User>().FirstOrDefault(user => user.Users_Password == password);
        //    return currentUser.ToDalUser();
        //}
        //public DTO.DalUser GetById(int key)
        //{
        //    var currentUser = _context.Set<User>().FirstOrDefault(user => user.Id == key);
        //    return currentUser.ToDalUser();
        //}
        #endregion

        public DalUser GetById(int key)
        {
            return GetFirstByPredicate(user => user.Id == key);
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<User>().ToList().Select(user => user.ToDalUser());
        }

        public DalUser GetFirstByPredicate(System.Linq.Expressions.Expression<Func<DalUser, bool>> f)
        {
            var criteria = ConvertExpression<DalUser, User>.Convert(f, "OrmUser");
            var response = _context.Set<User>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDalUser();
        }

        public IEnumerable<DalUser> GetAllByPredicate(System.Linq.Expressions.Expression<Func<DalUser, bool>> f)
        {
            var criteria = ConvertExpression<DalUser, User>.Convert(f, "OrmUser");
            return _context.Set<User>().Where(criteria.Compile()).ToList().Select(user => user.ToDalUser());
        }

        public void Create(DalUser e)
        {
            var user = e.ToOrmUser();
            _context.Set<User>().Add(user);
        }

        public void Delete(DTO.DalUser e)
        {
            var user = e.ToOrmUser();
            user = _context.Set<User>().Single(u => u.Id == e.Id);
            _context.Set<User>().Remove(user);
        }

        public void Update(DTO.DalUser entity)
        {
            var user = entity.ToOrmUser();
            var userToUpdate = _context.Set<User>().Single(u => u.Id == entity.Id);
            entity.CopyPropertiesTo(userToUpdate);
        }
        #endregion
    }
}
