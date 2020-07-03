using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class UserQuestion
    {
        [Key]
        public int UserQuestionId { get; set; }

        public int Score { get; set; }
        public int TimeSpend { get; set; }
        public DateTime TimeOfQuestion { get; set; }

        public int? TestTakerId { get; set; }
        public int? ExamId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }

        #region Navigation Properties
        [ForeignKey(nameof(TestTakerId))]
        public virtual TestTaker TestTaker { get; set; }
        [ForeignKey(nameof(ExamId))]
        public virtual Exam Exam { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; }

        [ForeignKey(nameof(AnswerId))]
        public virtual Answer Answer { get; set; }
        #endregion
    }
}
