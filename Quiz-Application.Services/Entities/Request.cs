using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Entities
{    
    public class Option
    {
       public int CandidateID { get; set; }
       public int ExamID { get; set; }
       public int QuestionID { get; set; }
       public int AnswerID { get; set; }
       public int SelectedOption { get; set; }
    }
    public class Root
    {
        public Candidate objCandidate { get; set; }
        public List<QuizAttempt> objAttempt { get; set; }
    }
    public class QuizAttempt
    {
        public int Sl_No { get; set; }
        public string SessionID { get; set; }
        public int ExamID { get; set; }
        public string Exam { get; set; }
        public string Date { get; set; }
        public string Score { get; set; }
        public string Status { get; set; }
    }    
    public class QuizReport
    {
        public int CandidateID { get; set; }
        public string SessionID { get; set; }
        public int ExamID { get; set; }
        public string Exam { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }    
    public class ReqReport
    {
        public int ExamID { get; set; }
        public string CandidateID { get; set; }
        public string SessionID { get; set; }
    }
    public class ReqCertificate
    {
        public int CandidateID { get; set; }
        public string SessionID { get; set; }
        public int ExamID { get; set; }
        public string Exam { get; set; }
        public string Date { get; set; }
        public string Score { get; set; }
    }

    public class Request
    {
    }
    public class ResPDF
    {
        public bool IsSuccess { get; set; }
        public string Path { get; set; }
    }
}
