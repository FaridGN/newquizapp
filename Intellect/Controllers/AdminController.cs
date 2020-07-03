using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intellect.Models;
using Intellect.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Intellect.Controllers
{
    public class AdminController : Controller
    {
        private readonly IntellectDbContext _intellectDbContext;

            public AdminController(IntellectDbContext intellectDbContext)
        {
            _intellectDbContext = intellectDbContext;
        }

        public async Task<IActionResult> Admin()
        {
            AdminViewModel adminModel = new AdminViewModel();
            adminModel.Exams = await _intellectDbContext.Exams.ToListAsync();
            adminModel.Exam = await _intellectDbContext.Exams.LastOrDefaultAsync();
            return View(adminModel);
        }

        //Exam
        [HttpGet]
        public IActionResult CreateExam()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExam(Exam exam)
        {
            if (ModelState.IsValid)
            {
                _intellectDbContext.Exams.Add(exam);
                await _intellectDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't create");
                return View();
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> EditExam(int Id)
        {
            Exam exam = new Exam();
            exam = await _intellectDbContext.Exams.SingleOrDefaultAsync(x => x.Id == Id);
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExam(Exam exam, int Id)
        {
            if(Id != exam.Id)
            {
                ModelState.AddModelError("", "Cannot edit");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Exam myexam = new Exam();
                     myexam = await _intellectDbContext.Exams.SingleOrDefaultAsync(x => x.Id == Id);

                    if(myexam != null)
                    {
                        myexam.Name = exam.Name;
                        myexam.ExamDate = exam.ExamDate;
                        myexam.ExamStartDateTime = exam.ExamStartDateTime;
                        myexam.ExamEndDateTime = exam.ExamEndDateTime;
                        await _intellectDbContext.SaveChangesAsync();
                        return RedirectToAction(nameof(Admin));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Couldn't Edit");
                    }
                   
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteExam(int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Exam = await _intellectDbContext.Exams.SingleOrDefaultAsync(x => x.Id == Id);
            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExam(Exam exam, int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Exam = await _intellectDbContext.Exams.SingleOrDefaultAsync(y => y.Id == Id);
            exam = admodel.Exam;
            _intellectDbContext.Exams.Remove(exam);
            await _intellectDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        //Question 

        [HttpGet]
        public async Task<IActionResult> Exams()
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Exams = await _intellectDbContext.Exams.ToListAsync();
            return View(admodel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateQuestion()
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Exams = await _intellectDbContext.Exams.ToListAsync();
            admodel.Examlist = await _intellectDbContext.Exams.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();

            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                 question.Score = 0;
                _intellectDbContext.Questions.Add(question);
                await _intellectDbContext.SaveChangesAsync();
                AdminViewModel admodel = new AdminViewModel();
                admodel.Exams = await _intellectDbContext.Exams.ToListAsync();
                admodel.Examlist = await _intellectDbContext.Exams.Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name

                }).ToListAsync();

                return RedirectToAction(nameof(Exams));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't Create");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExamQuestions(int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Question = await _intellectDbContext.Questions.Where(x => x.ExamId == Id).LastOrDefaultAsync();
            admodel.Questions = await _intellectDbContext.Questions.Where(x => x.ExamId == Id).ToListAsync();
            admodel.Exams = await _intellectDbContext.Exams.ToListAsync();
            admodel.Exam = await _intellectDbContext.Exams.SingleOrDefaultAsync(z => z.Id == Id);
            admodel.Examlist = await _intellectDbContext.Exams.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();

            admodel.Answers = await _intellectDbContext.Answers.ToListAsync();

            return View(admodel);
        }

        [HttpGet]
        public async Task<IActionResult> EditQuestion(int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Question = await _intellectDbContext.Questions.SingleOrDefaultAsync(x => x.Id == Id);
            admodel.Questions = await _intellectDbContext.Questions.ToListAsync();
            admodel.Exams = await _intellectDbContext.Exams.ToListAsync();
            admodel.Examlist = await _intellectDbContext.Exams.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();

            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(Question question, int Id)
        {
            if (Id != question.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
                return View();
            }

            if (ModelState.IsValid)
            {
                AdminViewModel admodel = new AdminViewModel();
                admodel.Question = await _intellectDbContext.Questions.SingleOrDefaultAsync(x => x.Id == Id);

                if(admodel.Question != null)
                {
                    admodel.Question.Name = question.Name;
                    admodel.Question.Description = question.Description;
                    admodel.Question.ExamId = question.ExamId;
                    await _intellectDbContext.SaveChangesAsync();

                    admodel.Examlist = await _intellectDbContext.Exams.Select(a => new SelectListItem()
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name

                    }).ToListAsync();
                    admodel.Exams = await _intellectDbContext.Exams.ToListAsync();
                    admodel.Questions = await _intellectDbContext.Questions.ToListAsync();
                    admodel.Question = await _intellectDbContext.Questions.LastOrDefaultAsync();       
                  
                }
                return RedirectToAction(nameof(ExamQuestions));

            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteQuestion(int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Question = await _intellectDbContext.Questions.SingleOrDefaultAsync(x => x.Id == Id);
            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(Question question, int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Question = await _intellectDbContext.Questions.SingleOrDefaultAsync(x => x.Id == Id);
            question = admodel.Question;
            _intellectDbContext.Questions.Remove(question);
            await _intellectDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Exams));
        }

        //Answer

        [HttpGet]
        public async Task<IActionResult> CreateAnswer()
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Questions = await _intellectDbContext.Questions.ToListAsync();
            admodel.QuestionList = await _intellectDbContext.Questions.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();

            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnswer(Answer answer, bool correct)
        {
             answer.Correct = correct;
            _intellectDbContext.Answers.Add(answer);
            await _intellectDbContext.SaveChangesAsync();
            AdminViewModel admodel = new AdminViewModel();
            admodel.Questions = await _intellectDbContext.Questions.ToListAsync();
            admodel.QuestionList = await _intellectDbContext.Questions.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();

            return RedirectToAction(nameof(Exams));
        }

        [HttpGet]
        public async Task<IActionResult> EditAnswer(int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Questions = await _intellectDbContext.Questions.ToListAsync();
            admodel.QuestionList = await _intellectDbContext.Questions.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();
            admodel.Answer = await _intellectDbContext.Answers.SingleOrDefaultAsync(x => x.Id == Id);

            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnswer(Answer answer, int Id, bool correct)
        {
            if(Id != answer.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
                return View();
            }

            if (ModelState.IsValid)
            {
                AdminViewModel admodel = new AdminViewModel();
                admodel.Answer = await _intellectDbContext.Answers.SingleOrDefaultAsync(x => x.Id == Id);

                if(admodel.Answer != null)
                {
                    answer.Correct = correct;
                    admodel.Answer.Description = answer.Description;
                    admodel.Answer.Correct = answer.Correct;
                    admodel.Answer.QuestionId = answer.QuestionId;
                    await _intellectDbContext.SaveChangesAsync();

                    admodel.Questions = await _intellectDbContext.Questions.ToListAsync();
                    admodel.QuestionList = await _intellectDbContext.Questions.Select(a => new SelectListItem()
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name

                    }).ToListAsync();

                }
                return RedirectToAction(nameof(Exams));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAnswer(int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Answer = await _intellectDbContext.Answers.SingleOrDefaultAsync(x => x.Id == Id);
            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAnswer(Answer answer, int Id)
        {
            AdminViewModel admodel = new AdminViewModel();
            admodel.Answer = await _intellectDbContext.Answers.SingleOrDefaultAsync(x => x.Id == Id);
            answer = admodel.Answer;
            _intellectDbContext.Answers.Remove(answer);
            await _intellectDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Exams));
        }
    }
}
