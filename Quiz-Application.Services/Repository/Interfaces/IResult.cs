using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz_Application.Services.Entities;

namespace Quiz_Application.Services.Repository.Interfaces
{
    public interface IResult<TEntity>
    {
        Task<IEnumerable<TEntity>> GetResult(List<Request> entity);
        Task<IEnumerable<TEntity>> ScoreReport(int CandidateID);
        Task<int> AddResult(List<TEntity> entity);
    }
}
