using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quiz_Application.Web.Models;
using Quiz_Application.Web.Authentication;
using Quiz_Application.Services.Entities;
using Quiz_Application.Services.Repository.Exam;
using Quiz_Application.Services.Repository.Question;

namespace Quiz_Application.Web.Controllers
{
    [BasicAuthentication]
    public class ExamController : Controller
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IExam<Services.Entities.Exam> _exam;
        private readonly IQuestion<Services.Entities.Question> _question;
        public ExamController(ILogger<ExamController> logger, IExam<Services.Entities.Exam> exam, IQuestion<Services.Entities.Question> question)
        {
            _logger = logger;
            _exam = exam;
            _question = question;
        }
        // GET: ExamController
        public IActionResult Index()
        {
            return View();
        }

        // GET: ExamController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExamList()
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
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> ExamDetails(int ExamID)
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
            finally { }
        }

        [HttpPost]
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
            finally { }
        }


        #region CRUD
        // GET: ExamController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExamController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExamController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
