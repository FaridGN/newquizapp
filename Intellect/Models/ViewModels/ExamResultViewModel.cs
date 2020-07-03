using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models.ViewModels
{
    public class ExamResultViewModel
    {
        public string ExamName { get; set; }
        public int TestTakerId { get; set; }
        public string TestTakerName { get; set; }
        public int Score { get; set; }

    }
}
