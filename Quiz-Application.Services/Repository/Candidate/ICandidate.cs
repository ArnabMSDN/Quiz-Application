using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Repository.Candidate
{
    public interface ICandidate<TEntity>
    {
        Task<IEnumerable<TEntity>> GetCandidateList();
        Task<TEntity> GetCandidate(int id);
        Task<IQueryable<TEntity>> SearchCandidate(Expression<Func<TEntity, bool>> search = null);
        Task<int> InsertCandidate(TEntity entity);
        Task<int> UpdateCandidate(TEntity entity);
        Task<int> DeleteCandidate(TEntity entity);

       
    }
}
