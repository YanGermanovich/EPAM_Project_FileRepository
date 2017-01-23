using DAL.Interface;
using DAL.DTO;
using ORM;
using DAL.DalToOrmMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CustomExpressionVisitor;
using DAL.Helpers;

namespace DAL.Concrete
{
    public class RoleRepository: IRolesRepository
    {

        private readonly DbContext _context;

        public RoleRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _context.Set<Role>().ToList().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            return GetFirstByPredicate(role => role.Id == key);
        }

        public DalRole GetFirstByPredicate(System.Linq.Expressions.Expression<Func<DalRole, bool>> f)
        {
            var criteria = ConvertExpression<DalRole, Role>.Convert(f, "OrmRole");
            var response = _context.Set<Role>().FirstOrDefault(criteria.Compile());
            return response == null ? null : response.ToDalRole();
        }

        public IEnumerable<DalRole> GetAllByPredicate(System.Linq.Expressions.Expression<Func<DalRole, bool>> f)
        {
            var criteria = ConvertExpression<DalRole, Role>.Convert(f, "OrmRole");
            return _context.Set<Role>().Where(criteria.Compile()).ToList().Select(role => role.ToDalRole());
        }

        public void Create(DalRole e)
        {
            var role = e.ToOrmRole();
            _context.Set<Role>().Add(role);
        }

        public void Delete(DalRole e)
        {
            var role = e.ToOrmRole();
            role = _context.Set<Role>().Single(r => r.Id == e.Id);
            _context.Set<Role>().Remove(role);
        }

        public void Update(DalRole entity)
        {
            var role = entity.ToOrmRole();
            var roleToUpdate = _context.Set<Role>().Single(r => r.Id == entity.Id);
            entity.CopyPropertiesTo(roleToUpdate);
        }
    }
}
