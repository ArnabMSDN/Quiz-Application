using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz_Application.Services.Entities
{
  public  class Question:BaseEntity
    {
        [Key]
        public int QuestionID { get; set; }
        public int QuestionType { get; set; }  //MCQ-1      
        public string DisplayText { get; set; }
        public int ExamID { get; set; }
    }
}
