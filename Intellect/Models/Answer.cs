using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public bool Correct { get; set; } 
    }
}
