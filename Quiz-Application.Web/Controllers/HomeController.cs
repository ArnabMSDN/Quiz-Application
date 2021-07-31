using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz_Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Quiz_Application.Web.Authentication;
using Microsoft.AspNetCore.Http;
using Quiz_Application.Web.Common;
using Quiz_Application.Services.Entities;
using System.IO;
using System.Net;
using Quiz_Application.Services.Repository.Candidate;

namespace Quiz_Application.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICandidate<Services.Entities.Candidate> _candidate;

        public HomeController(ILogger<HomeController> logger, ICandidate<Services.Entities.Candidate> candidate)
        {
            _logger = logger;
            _candidate = candidate;
        }

        [BasicAuthentication]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            Candidate objCandidate = HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");
            return View(objCandidate);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SaveImage()
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    string candidateID = Convert.ToString(Request.Form["Candidate-ID"]);
                    var file = Request.Form.Files["Candidate-Img"];

                    IQueryable<Services.Entities.Candidate> candidate = await _candidate.SearchCandidate(x => x.Candidate_ID == candidateID.DecodeBase64());
                    if (candidate.Any())
                    {
                        string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                        string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string UploadPath = Path.Combine(UploadFolder, UniqueFileName);
                        var item = candidate.FirstOrDefault();
                        item.ImgFile = UniqueFileName;
                        await _candidate.UpdateCandidate(item);
                        await file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
                    }
                }
                return Json(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            { }
        }

    }
}
