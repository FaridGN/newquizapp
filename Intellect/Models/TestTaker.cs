using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class TestTaker
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public DateTime Birth { get; set; }
        public ExamUser ExamUser { get; set; }
        public int Result { get; set; }
        public ICollection<UserQuestion> UserQuestions { get; set; }
    }
}
