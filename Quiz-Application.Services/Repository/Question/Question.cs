﻿using Microsoft.EntityFrameworkCore;
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

       public async Task<QnA> GetQuestionList(int ExamID)
        {
            QnA objQnA = null;
            AnswerDetails _objA = null;
            List<QuestionDetails> _objQlst = new List<QuestionDetails>();            
           
            var questions =await _dbContext.Question.Where(q => q.ExamID == ExamID).ToListAsync();
            foreach (var Qitem in questions)
            {
                List<OptionDetails> _objOlst = new List<OptionDetails>();
                QuestionDetails _objQ = new QuestionDetails();
                _objQ.QuestionID = Qitem.QuestionID;
                _objQ.QuestionType = Qitem.QuestionType;
                _objQ.QuestionText = Qitem.DisplayText;
                                          
                var options = await _dbContext.Choice.Where(q => q.QuestionID == Qitem.QuestionID).Select(o => new { OptionID = o.ChoiceID, Option = o.DisplayText }).ToListAsync();
               
                foreach (var Oitem in options)
                {
                    OptionDetails _objO = new OptionDetails()
                    {
                        OptionID=Oitem.OptionID,
                        Option=Oitem.Option
                    };
                    _objOlst.Add(_objO);
                }
                _objQ.options = _objOlst;

                var ans =await _dbContext.Answer.Where(q => q.QuestionID == Qitem.QuestionID).Select(o => new { AnswerID = o.Sl_No, OptionID = o.ChoiceID, Answer = o.DisplayText, }).FirstOrDefaultAsync();
               
                _objA = new AnswerDetails()
                {
                    AnswarID = ans.AnswerID,
                    OptionID=ans.OptionID,
                    Answar = ans.Answer                   
                };
                _objQ.answer = _objA;

                _objQlst.Add(_objQ);
            }

            objQnA = new QnA()
            {
                ExamID = ExamID,
                questions = _objQlst
            };
            return objQnA;
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