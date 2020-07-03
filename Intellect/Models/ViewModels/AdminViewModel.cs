using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models.ViewModels
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public IEnumerable<Exam> Exams { get; set; }
        public List<SelectListItem> Examlist { get; set; }
        public List<SelectListItem> QuestionList { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public TestTaker TestTaker { get; set; }
        public IEnumerable<TestTaker> TestTakers { get; set; }
        public Question CurrentQuestion { get; set; }
        public Question NextQuestion { get; set; }
        public List<Question> Equestions { get; set; }
        public string SelectedAnswer { get; set; }
    }
}
