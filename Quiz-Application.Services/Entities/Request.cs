using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Entities
{    
   public class Request
   {
       public int CandidateID { get; set; }
       public int ExamID { get; set; }
       public int QuestionID { get; set; }
       public int AnswerID { get; set; }
       public int SelectedOption { get; set; }
   }
    public class Root
    {
        public List<Attempt> objAttempt { get; set; }      
    }
    public class Attempt
    {
        public int Sl_No { get; set; }
        public string SessionID { get; set; }
        public int ExamID { get; set; }
        public string Exam { get; set; }
        public string Date { get; set; }
        public string Score { get; set; }
    }
    public class ReqReport
    {
        public int ExamID { get; set; }
        public string SessionID { get; set; }
    }
}
