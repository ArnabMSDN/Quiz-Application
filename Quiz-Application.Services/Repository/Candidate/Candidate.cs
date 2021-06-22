using Quiz_Application.Services.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Quiz_Application.Services.Repository
{
    public class Candidate<TEntity> : ICandidate<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public Candidate(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
            
        public async Task<IEnumerable<TEntity>> GetCandidateList()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetCandidate(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IQueryable<TEntity>> IsValidCandidate(Expression<Func<TEntity, bool>> search = null)
        {
            IQueryable<TEntity> query=_dbSet;
            if (search != null){ query =query.Where(search); }           
            return query;
        }

        public async Task<int> InsertCandidate(TEntity entity)
        {
            int output = 0;
            _dbSet.Add(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }

        public async Task<int> UpdateCandidate(TEntity entity)
        {
            int output = 0;
            _dbSet.Update(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }
        public async Task<int> DeleteCandidate(TEntity entity)
        {
            int output = 0;
            _dbSet.Remove(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }

      
    }
}
