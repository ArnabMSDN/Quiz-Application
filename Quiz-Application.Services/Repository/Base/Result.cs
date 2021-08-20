using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Quiz_Application.Services.Entities;
using Quiz_Application.Services.Repository.Interfaces;

namespace Quiz_Application.Services.Repository.Base
{
    public class Result<TEntity> : IResult<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public Result(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<int> AddResult(List<TEntity> entity)
        {
            int output = 0;
            _dbSet.AddRange(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }

        public async Task<IEnumerable<TEntity>> GetResult(List<Request> entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> ScoreReport(int CandidateID)
        {
            throw new NotImplementedException();
        }
    }   
}
