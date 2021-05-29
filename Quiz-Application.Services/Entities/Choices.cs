using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Application.Services.Entities
{
    public class Choice:BaseEntity
    {
        [Key]
        public int ChoiceID { get; set; }
        public int QuestionID { get; set; }      
        public string DisplayText { get; set; }               
    }
}
