using Quiz_Application.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Repository.Question
{
    public interface IQuestion<TEntity>
    {
        Task<IEnumerable<QnA>> GetQuestionList(int ExamID);       
        Task<IQueryable<TEntity>> SearchQuestion(Expression<Func<TEntity, bool>> search = null);
        Task<int> InsertQuestion(TEntity entity);
        Task<int> UpdateQuestion(TEntity entity);
        Task<int> DeleteQuestion(TEntity entity);
    }
}
