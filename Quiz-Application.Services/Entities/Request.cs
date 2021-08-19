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
}
