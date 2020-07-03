using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Intellect.Models;
using Intellect.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Intellect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IntellectDbContext _intellectDbContext;
        private readonly UserManager<ExamUser> _userManager;
        private readonly SignInManager<ExamUser> _signInManager;
        static int exam_id = 0;
        static int? PreviousId = 0;
        //static int result = 0;
        static int correctAnswer = 0;
        static List<Question> RemainedQuestions = new List<Question>();
        static List<int> trueAnswers = new List<int>();

        public HomeController(IntellectDbContext intellectDbContext, UserManager<ExamUser> userManager, SignInManager<ExamUser> signInManager)
        {
            _intellectDbContext = intellectDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Test(int Id)
        {
            var currentExam = _intellectDbContext.Exams.FirstOrDefault(x => x.Id == Id);
            if(currentExam.ExamDate.Date != DateTime.Now.Date || currentExam.ExamStartDateTime.TimeOfDay > DateTime.Now.TimeOfDay || currentExam.ExamEndDateTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                ViewBag.ExamNotAllowed = true;
            }
            HttpContext.Session.SetObject(Sessions.CurrentExam, currentExam);
            AdminViewModel admodel = new AdminViewModel();
            admodel.Equestions = GetQuestions(Id).Where(q => q.ExamId == Id).ToList();
            admodel.CurrentQuestion = GetQuestions(Id).Where(q => q.ExamId == Id).FirstOrDefault();

            RemainedQuestions = admodel.Equestions;
            PreviousId = admodel.CurrentQuestion.Id;
            HttpContext.Session.SetString(Sessions.CurrentExamId, Id.ToString());
            return View(admodel);
        }


        [HttpGet]
        public async Task<IActionResult> Question(int Id, int count)
        {
            var currentExam = HttpContext.Session.GetObject<Exam>(Sessions.CurrentExam);
            if (currentExam.ExamDate.Date != DateTime.Now.Date || currentExam.ExamStartDateTime.TimeOfDay > DateTime.Now.TimeOfDay || currentExam.ExamEndDateTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                return RedirectToAction(nameof(Finish));
            }
            int currentExamId = int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId));
            AdminViewModel admodel = new AdminViewModel();
            admodel.CurrentQuestion = GetQuestions(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).Where(x => x.Id == Id).SingleOrDefault();
            admodel.Answers = GetAnswers(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).Where(y => y.QuestionId == Id).ToList();
            admodel.Equestions = GetQuestions(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).Where(q => q.ExamId == currentExamId).ToList();

            // admodel.CurrentQuestion.Remainedtime = new TimeSpan(0, 0, 60);
            if (admodel.CurrentQuestion != null)
            {
                HttpContext.Session.SetString(Sessions.CurrentQuestionStartTime, DateTime.Now.ToString());
                HttpContext.Session.SetString(Sessions.CurrentQuestionId, admodel.CurrentQuestion.Id.ToString());
            }

            if (count == -1)
            {
                return RedirectToAction(nameof(Finish));
            }
            if (count > 1)
            {
                var question = RemainedQuestions.Single(r => r.Id == admodel.CurrentQuestion.Id);
                PreviousId = question.Id;
                RemainedQuestions.Remove(question);
                admodel.NextQuestion = RemainedQuestions[0];
            }
            else
            {
                admodel.NextQuestion = RemainedQuestions[0];
            }
            count -= 1;

            ViewBag.Equestions = count;

            return View(admodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Question(int Id, int count, int myanswer)
        {
            var currentExam = HttpContext.Session.GetObject<Exam>(Sessions.CurrentExam);
            if (currentExam.ExamDate.Date != DateTime.Now.Date || currentExam.ExamStartDateTime.TimeOfDay > DateTime.Now.TimeOfDay || currentExam.ExamEndDateTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                return RedirectToAction(nameof(Finish));
            }
            var sesdata = HttpContext.Session.GetString(Sessions.CurrentQuestionStartTime);
            var interval = DateTime.Now - DateTime.Parse(sesdata);
            AdminViewModel admodel = new AdminViewModel();
            int currentExamId = int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId));
            int currentQuestionId = int.Parse(HttpContext.Session.GetString(Sessions.CurrentQuestionId));
            admodel.CurrentQuestion = GetQuestions(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).Where(x => x.Id == Id).SingleOrDefault();
            admodel.Answers = GetAnswers(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).Where(y => y.QuestionId == Id).ToList();
            admodel.Equestions = GetQuestions(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).Where(q => q.ExamId == currentExamId).ToList();

            /*  var date = HttpContext.Session.GetString("currentQuestion_1");

              return Ok(new
              {
                  date,
                  Id,
                  HttpContext.Session.Keys
            */
            //  admodel.CurrentQuestion.Remainedtime = new TimeSpan(0, 0, 60);        
            //  var interval = DateTime.Now - DateTime.Parse(HttpContext.Session.GetString("currentQuestion_" + Id));


            correctAnswer = _intellectDbContext.Answers.Where(a => a.QuestionId == PreviousId && a.Correct == true).SingleOrDefault().Id;

            if (_signInManager.IsSignedIn(User))
            {
                ExamUser examTaker = await _userManager.GetUserAsync(HttpContext.User);

                examTaker.TestTaker = await _intellectDbContext.TestTakers
                                               .Include(tt => tt.UserQuestions)
                                               .Where(t => t.Id == examTaker.TestTakerId)
                                               .FirstOrDefaultAsync();
                admodel.CurrentQuestion = GetQuestions(int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId))).FirstOrDefault(x => x.Id == Id);
                var score = 0;
                if (myanswer == correctAnswer && interval.TotalSeconds < 61.5)
                {
                    //admodel.CurrentQuestion.Score = 60 - (int)interval.TotalMilliseconds / 1000;
                    //// admodel.CurrentQuestion.Score = result;
                    score = 60 - (int)interval.TotalMilliseconds / 1000;
                }
                UserQuestion userQuestion = new UserQuestion();
                userQuestion.AnswerId = myanswer;
                userQuestion.ExamId = int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId));
                userQuestion.QuestionId = int.Parse(HttpContext.Session.GetString(Sessions.CurrentQuestionId));
                userQuestion.Score = score;
                userQuestion.TestTakerId = examTaker.TestTakerId;
                userQuestion.TimeOfQuestion = DateTime.Now;
                userQuestion.TimeSpend = (int)interval.TotalSeconds;
                InsertUserQuestionInSession(userQuestion);
                //await _intellectDbContext.UserQuestions.AddAsync(userQuestion);
                //await _intellectDbContext.SaveChangesAsync();
            }

            if (count == -1)
            {
                return RedirectToAction(nameof(Finish));
            }

            if (count > 1)
            {
                var question = RemainedQuestions.Single(r => r.Id == admodel.CurrentQuestion.Id);
                PreviousId = question.Id;
                RemainedQuestions.Remove(question);
                admodel.NextQuestion = RemainedQuestions[0];
            }
            else
            {
                admodel.NextQuestion = RemainedQuestions[0];
                //count -= 1;
            }
            count -= 1;

            ViewBag.Equestions = count;

            return RedirectToAction(nameof(Question));
        }

        public async Task<IActionResult> Finish()
        {
            if (InsertUserQuestionInDatabase())
            {
                var userQuestions = (HttpContext.Session.GetObject<List<UserQuestion>>(Sessions.CurrentUserQuestions))?? new List<UserQuestion>();
                ViewBag.TotalScore = userQuestions.Sum(x => x.Score);
                ///Report by Exam
                //var examId = int.Parse(HttpContext.Session.GetString(Sessions.CurrentExamId));
                //var otherUserQuestions = _intellectDbContext.UserQuestions.Where(x => x.ExamId == examId).Include(x=> x.Exam).Include(x=> x.TestTaker).ToList();
                //var resultList = new List<ExamResultViewModel>();
                //foreach (var uq in otherUserQuestions)
                //{
                //    var result = resultList.FirstOrDefault(x => x.TestTakerName == uq.TestTaker.Name);
                //    if(result != null)
                //    {
                //        result.Score += uq.Score;
                //    }
                //    else
                //    {
                //        resultList.Add(new ExamResultViewModel
                //        {
                //            ExamName = uq.Exam.Name,
                //            Score = uq.Score,
                //            TestTakerId = uq.TestTakerId.Value,
                //            TestTakerName = uq.TestTaker.Name
                //        });
                //    }
                //}
                ///Report by Exam
            }
            //if (_signInManager.IsSignedIn(User))
            //{
            //    int a = 0;
            //    ExamUser examTaker = await _userManager.GetUserAsync(HttpContext.User);

            //    examTaker.TestTaker = await _intellectDbContext.TestTakers.FirstOrDefaultAsync();
            //    Exam exam = await _intellectDbContext.Exams.Where(e => e.Id == exam_id).SingleOrDefaultAsync();
            //    foreach (Question question in exam.Questions)
            //    {
            //        a = a += question.Score;
            //    }
            //    //  examTaker.TestTaker.Result = result;
            //    await _intellectDbContext.SaveChangesAsync();

            return View();
            //}
            //else
            //{
            //    return RedirectToAction(nameof(Index));
            //}
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Question> GetQuestions(int examId)
        {
            var questions = HttpContext.Session.GetObject<List<Question>>(Sessions.AllQuestions);
            if(questions == null)
            {
                questions = _intellectDbContext.Questions/*.Include(q => q.Answers)*/.Where(x=> x.ExamId == examId).ToList();
                questions.ForEach(x => x.Exam = null);
                HttpContext.Session.SetObject(Sessions.AllQuestions, questions);
            }
            return questions;
        }
        private List<Answer> GetAnswers(int examId)
        {
            var answers = new List<Answer>();
            answers = HttpContext.Session.GetObject<List<Answer>>(Sessions.AllAnswers);
            if (answers == null)
            {
                var questions = _intellectDbContext.Questions.Include(q => q.Answers).Where(x => x.ExamId == examId).ToList();
                answers = new List<Answer>();
                foreach (var qst in questions)
                {
                    var ansList = qst.Answers.ToList();
                    ansList.ForEach(x => x.Question = null);
                    answers.AddRange(ansList);
                }
                HttpContext.Session.SetObject(Sessions.AllAnswers, answers);
            }
            //return questions;
            return answers;
        }

        private bool InsertUserQuestionInSession(UserQuestion userQuestion)
        {
            try
            {
                var userQuestions = HttpContext.Session.GetObject<List<UserQuestion>>(Sessions.CurrentUserQuestions);
                if(userQuestions == null)
                {
                    userQuestions = new List<UserQuestion>();
                }
                userQuestions.Add(userQuestion);
                HttpContext.Session.SetObject(Sessions.CurrentUserQuestions, userQuestions);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool InsertUserQuestionInDatabase()
        {
            try
            {
                var userQuestions = HttpContext.Session.GetObject<List<UserQuestion>>(Sessions.CurrentUserQuestions);
                if(userQuestions == null)
                {
                    return false;
                }
                _intellectDbContext.UserQuestions.AddRange(userQuestions);
                _intellectDbContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
