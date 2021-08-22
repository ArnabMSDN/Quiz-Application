using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Entities
{
    public  class Result:BaseEntity
    {
        [Key]
        public int Sl_No { get; set; }

        [MaxLength]
        public string SessionID { get; set; }
        public int CandidateID { get; set; }        
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public int SelectedOptionID { get; set; }
        public bool IsCorrent { get; set; }
    }
}
