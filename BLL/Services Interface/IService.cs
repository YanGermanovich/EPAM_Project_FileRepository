using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services_Interface
{
    public interface IService<TEntity>
    {
        TEntity GetFirstByPredicate(Expression<Func<TEntity, bool>> f);
        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> f);
        TEntity GetEntitieById(int key);
        IEnumerable<TEntity> GetAllEntities();
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity e);
    }
}
