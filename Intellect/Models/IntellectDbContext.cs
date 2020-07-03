using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class IntellectDbContext:IdentityDbContext<ExamUser>
    {
        public IntellectDbContext(DbContextOptions<IntellectDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserQuestion>()
            //    .HasKey(a => new { a.TestTakerId, a.QuestionId });

            //modelBuilder.Entity<UserQuestion>()
            //    .HasOne(a => a.TestTaker)
            //    .WithMany(b => b.UserQuestions)
            //    .HasForeignKey(a => a.TestTakerId);

            //modelBuilder.Entity<UserQuestion>()
            //    .HasOne(a => a.Question)
            //    .WithMany(c => c.UserQuestions)
            //    .HasForeignKey(a => a.QuestionId);
            
          
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamUser> ExamUsers { get; set; }
        public DbSet<UserQuestion> UserQuestions { get; set; }
        public DbSet<TestTaker> TestTakers { get; set; }
    }
}
