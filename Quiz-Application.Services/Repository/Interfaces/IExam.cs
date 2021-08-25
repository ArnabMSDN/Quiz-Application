using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Repository.Interfaces
{
    public interface IExam<TEntity>
    {
        Task<IEnumerable<TEntity>> GetExamList();
        Task<TEntity> GetExam(int id);
        Task<IQueryable<TEntity>> SearchExam(Expression<Func<TEntity, bool>> search = null);
        Task<int> AddExam(TEntity entity);
        Task<int> UpdateExam(TEntity entity);
        Task<int> DeleteExam(TEntity entity);
       
    }
}
