using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz_Application.Web.Authentication;
using Quiz_Application.Services.Entities;
using Quiz_Application.Web.Models;
using Quiz_Application.Services.Repository.Interfaces;


namespace Quiz_Application.Web.Controllers
{    
    [BasicAuthentication]   
    public class ExamController : Controller
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IExam<Services.Entities.Exam> _exam;
        private readonly IQuestion<Services.Entities.Question> _question;
        private readonly IResult<Services.Entities.Result> _result;
        public ExamController(ILogger<ExamController> logger, IExam<Services.Entities.Exam> exam, IQuestion<Services.Entities.Question> question, IResult<Services.Entities.Result> result)
        {
            _logger = logger;
            _exam = exam;
            _question = question;
            _result = result;
        }
             
        [HttpGet]
        [Route("~/api/Exams")]
        public async Task<IActionResult> Exams()
        {           
            try
            {
                IEnumerable<Exam> lst = await _exam.GetExamList();               
                return Ok(lst.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally 
            {
            }
        }

        [HttpGet]
        [Route("~/api/Exam/{ExamID?}")]
        public async Task<IActionResult> Exam(int ExamID)
        {
            try
            {
                Exam exm = await _exam.GetExam(ExamID);
                return Ok(exm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally 
            {
            }
        }

        [HttpGet]
        [Route("~/api/Questions/{ExamID?}")]
        public async Task<IActionResult> Questions(int ExamID)
        {
            try
            {
                QnA _obj = await _question.GetQuestionList(ExamID);
                return Ok(_obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally 
            {
            }
        }

        [HttpPost]
        [Route("~/api/Score")]       
        public async Task<IActionResult> Score(List<Option> objRequest)
        {
            int i = 0;
            bool IsCorrect = false;
            List<Result> objList = null;
            string _SessionID = null;
            try
            {               
                if (objRequest.Count > 0)
                {
                    _SessionID = Guid.NewGuid().ToString() + "-" + DateTime.Now;
                    objList = new List<Result>();
                    foreach (var item in objRequest)
                    {
                        if (item.AnswerID == item.SelectedOption)
                            IsCorrect = true;
                        else
                            IsCorrect = false;

                        Result obj = new Result()
                        {
                            CandidateID = item.CandidateID,
                            ExamID = item.ExamID,
                            QuestionID = item.QuestionID,
                            AnswerID = item.AnswerID,
                            SelectedOptionID = item.SelectedOption,
                            IsCorrent = IsCorrect,
                            SessionID= _SessionID,
                            CreatedBy = "SYSTEM",
                            CreatedOn = DateTime.Now
                        };
                        objList.Add(obj);
                    }
                    i = await _result.AddResult(objList);
                }
               
            }
            catch (Exception ex)
            {
                i = 0;
                throw new Exception(ex.Message, ex.InnerException);           
            }
            finally
            {                
            }
            return Ok(i);
        }
        
    }
}
