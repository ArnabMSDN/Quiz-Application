using Quiz_Application.Services.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Quiz_Application.Services.Repository.Exam
{
    public class Exam<TEntity> : IExam<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public Exam(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }            
        public async Task<IEnumerable<TEntity>> GetExamList()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetExam(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IQueryable<TEntity>> SearchExam(Expression<Func<TEntity, bool>> search = null)
        {
            IQueryable<TEntity> query=_dbSet;
            if (search != null)
            { 
                query =query.Where(search); 
            }           
            return query;
        }

        public async Task<int> InsertExam(TEntity entity)
        {
            int output = 0;
            _dbSet.Add(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }

        public async Task<int> UpdateExam(TEntity entity)
        {
            int output = 0;
            _dbSet.Update(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }
        public async Task<int> DeleteExam(TEntity entity)
        {
            int output = 0;
            _dbSet.Remove(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }
      
    }
}
