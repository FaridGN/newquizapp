using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Exam Exam { get; set; }
        public int ExamId { get; set; }
        public TimeSpan? Remainedtime { get; set; }
        public int Score { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<UserQuestion> UserQuestions { get; set; }

    }    
}
