using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Entities
{
    public class QnA:BaseEntity
    {
        public int ExamID { get; set; }
        public int QuestionType { get; set; }
        public int QuestionID { get; set; }
        public int ChoiceID { get; set; }
        public int AnswarID { get; set; }
        public string Question { get; set; }
        public string Option { get; set; }
        public string Answar { get; set; }
    }
}
