using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quiz_Application.Web.Extensions;
using Quiz_Application.Web.Authentication;
using Quiz_Application.Services.Entities;
using Microsoft.Extensions.Logging;
using Quiz_Application.Services.Repository.Interfaces;

namespace Quiz_Application.Web.Controllers
{
    [BasicAuthentication]
    public class ScoreController : Controller
    {

       private readonly ILogger<ScoreController> _logger;
       private readonly IResult<Services.Entities.Result> _result;

       public ScoreController(ILogger<ScoreController> logger, IResult<Services.Entities.Result> result)
        {
            _logger = logger;
            _result = result;
        }

       public async Task<IActionResult> Result()
       {
            try
            {
                Candidate _objCandidate = HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");

                IEnumerable<Attempt> _obj = await _result.GetAttemptHistory(_objCandidate.Candidate_ID);
                Root objRoot = new Root(){
                    objCandidate= _objCandidate,
                    objAttempt = _obj.ToList() 
                };               
                return View(objRoot);
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
        [Route("~/api/Report")]
        public async Task<IActionResult> Report(ReqReport argRpt)
        {
            try
            {
                IEnumerable<Report> lst = await _result.ScoreReport(argRpt);
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
    }
}
