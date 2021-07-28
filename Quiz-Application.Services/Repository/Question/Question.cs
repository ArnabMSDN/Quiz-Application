using Microsoft.EntityFrameworkCore;
using Quiz_Application.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Threading.Tasks;


namespace Quiz_Application.Services.Repository.Question
{
    public class Question<TEntity> : IQuestion<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public Question(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
      
        public Task<int> DeleteQuestion(TEntity entity)
        {
            throw new NotImplementedException();
        }
       
        public async Task<IEnumerable<QnA>> GetQuestionList(int ExamID)
        {
               List<QnA> _lst = null;               
               var Data =              
               from Q in _dbContext.Question
               join C in _dbContext.Choice on Q.QuestionID equals C.QuestionID
               join A in _dbContext.Answer on C.ChoiceID equals A.ChoiceID               
               where Q.ExamID == ExamID
               select new {
                   Q.QuestionID,
                   Q.QuestionType,
                   Question=  Q.DisplayText,
                   Q.ExamID,
                   C.ChoiceID,
                   Option=C.DisplayText,
                   AnswarID= A.Sl_No,
                   Answar=A.DisplayText
               };
            _lst = new List<QnA>();
            foreach (var item in Data)
            {
                QnA _obj = new QnA()
                {
                    QuestionID = item.QuestionID,
                    QuestionType = item.QuestionID,
                    Question = item.Question,
                    ExamID = item.ExamID,
                    ChoiceID = item.ChoiceID,
                    Option = item.Option,
                    AnswarID = item.AnswarID,
                    Answar = item.Answar
                };
                _lst.Add(_obj);
            }
            return _lst;          
        }

        public Task<int> InsertQuestion(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<TEntity>> SearchQuestion(Expression<Func<TEntity, bool>> search = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (search != null) 
            {
                query = query.Where(search); 
            }
            return query;
        }

        public Task<int> UpdateQuestion(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
