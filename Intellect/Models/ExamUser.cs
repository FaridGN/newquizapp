using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class ExamUser: IdentityUser
    {
        public TestTaker TestTaker { get; set; }
        public int? TestTakerId { get; set; }
    }
}
