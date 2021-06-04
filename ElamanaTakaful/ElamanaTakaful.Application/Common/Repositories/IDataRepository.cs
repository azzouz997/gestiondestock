using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Common.Repositories
{
    public interface IDataRepository<TEntity>
    {
        TEntity Add(TEntity T);

        List<TEntity> GetAll();

        void Update(TEntity T);

        void Delete(int id);

        TEntity Get(int id);
    }
}
