using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Entities
{
    public class QnA
    {
        public int ExamID { get; set; }
        public string Exam { get; set; }
        public List<QuestionDetails>  questions { get; set; }
    }
    public class QuestionDetails
    {       
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public List<OptionDetails> options { get; set; }
        public AnswerDetails answer { get; set; }
    }
    public class OptionDetails
    {
        public int OptionID { get; set; }
        public string Option { get; set; }    
    }
    public class AnswerDetails
    {
        public int AnswarID { get; set; }
        public int OptionID { get; set; }
        public string Answar { get; set; }
    }

}
