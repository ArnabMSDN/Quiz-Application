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
    public class CandidateService<TEntity> : ICandidate<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public CandidateService(QuizDBContext dbContext)
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
        
        public async Task<IQueryable<TEntity>> SearchCandidate(Expression<Func<TEntity, bool>> search = null)
        {
            IQueryable<TEntity> query=_dbSet;
            if (search != null)
            { 
                query =query.Where(search); 
            }           
            return query;
        }

        public async Task<int> AddCandidate(TEntity entity)
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
