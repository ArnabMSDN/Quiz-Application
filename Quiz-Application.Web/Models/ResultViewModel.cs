using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_Application.Web.Models
{
    public class ResultViewModel
    {
        [Required]
        public int CandidateID;
        [Required]
        public int ExamID;
        [Required]
        public int QuestionID;
        [Required]
        public int AnswerID;
        [Required]
        public int SelectedOption;
    }
}
