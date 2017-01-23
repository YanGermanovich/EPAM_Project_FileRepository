using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Services_Interface
{
    public interface IRatingService : IService<RatingEntity>
    {
        IEnumerable<RatingEntity> GetByMark(double mark);
        IEnumerable<RatingEntity> GetByUser(int id_user);
        IEnumerable<RatingEntity> GetByResource(int id_resource);
        IEnumerable<RatingEntity> GetByDate(DateTime date);        
    }
}
