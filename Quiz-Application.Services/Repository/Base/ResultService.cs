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
    public class ResultService<TEntity> : IResult<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public ResultService(QuizDBContext dbContext)
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
        public async Task<IEnumerable<Attempt>> GetAttemptHistory(string argCandidateID)
        {
            try
            {
                List<Attempt> obj = await _dbContext.Set<Attempt>().FromSqlRaw(@"SELECT
                CAST(ROW_NUMBER() OVER (ORDER BY R.SessionID) AS int) Sl_No,
                R.SessionID,
                R.ExamID,
                E.Name AS Exam,
                CONVERT(varchar, R.CreatedOn, 106) AS Date,
                (CAST(COUNT(R.Sl_No) as varchar(20)) + '/' + CAST(CAST(E.FullMarks AS INT) AS VARCHAR(20))) AS Score
                FROM Result R
                LEFT JOIN Exam E ON R.ExamID = E.ExamID
                WHERE R.CandidateID = {0} AND R.IsCorrent = 1
                GROUP BY R.SessionID, R.ExamID, E.Name, E.FullMarks, R.CreatedOn", argCandidateID).ToListAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
            }
        }
        public async Task<IEnumerable<Report>> ScoreReport(ReqReport argRpt)
        {
            try
            {
                List<Report> obj = await _dbContext.Set<Report>().FromSqlRaw(@"EXEC GetReport {0},{1},{2}", argRpt.ExamID, argRpt.CandidateID, argRpt.SessionID).ToListAsync();  
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
            }
        }
    
    }   
}
