using KPD.Contexts;
using KPD.Models;
using KPD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPD.Controllers
{
    [ApiController]
    [Route("KPDController")]
    public class KPDController : Controller
    {

        ApplicationContext db;
        public KPDController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMainPage(string button)
        {
            return View("~/Views/MainPage.cshtml");
        }

        [HttpGet]
        [Route("AddQuestion")]
        public async Task<IActionResult> GetAddQuestionPage()
        {
            return View("~/Views/AddQuestion.cshtml");
        }

        [HttpPost]
        [Route("AddQuestion")]
        public async Task<IActionResult> AddQuestion([FromForm]string Event, [FromForm]string text, [FromForm]string correctAnswer)
        {
            Question question = new Question() { Event = Event, Text = text, CorrectAnswer = correctAnswer };
            if (question != null)
            {
                db.Questions.Add(question/*new Question() { Event = question.Event, text = question.text }*/);
                db.SaveChanges();
                return View("~/Views/QuestionAdded.cshtml", new QuestionAddedViewModel() {QuestionId = question.Id });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("AddAnswer")]
        public async Task<ActionResult> GetAddAnswerPage()
        {
            return View("~/Views/AddAnswer.cshtml");
        }


        [HttpPost]
        [Route("AddAnswer")]
        public async Task<ActionResult> AddAnswer([FromForm]int questionNumber, [FromForm]string sender , [FromForm]string text )
        {

            if ((sender == default) || (text == default) || (questionNumber > db.Questions.OrderBy(p => p.Id).LastOrDefault().Id))
            {
                return BadRequest();
            }
            else
            {
                Contestant contestant = new Contestant() { Answer = text, Nickname = sender, QuestionId = questionNumber};
                db.Contestants.Add(contestant);
                db.SaveChanges();
                if (contestant.Answer == db.Questions.Find(questionNumber).CorrectAnswer && db.Questions.Find(questionNumber).WinnerId == default)
                {
                    db.Questions.Find(questionNumber).WinnerId = contestant.Id;
                }
                db.SaveChanges();
                return View("~/Views/AnswerAdded.cshtml");
            }
        }


        //[HttpGet]
        //[Route("CheckResult")]
        //public async Task<IActionResult> Get()
        //{
        //    return View("~/Views/Get.cshtml");
        //}


        [HttpGet]  
        [Route("CheckResult")]
        public async Task<IActionResult> CheckResult(string password, int questionNumber)
        {
            if (password == null || questionNumber == default)
            {
                return View("~/Views/Get.cshtml");
            }
            if (password == "qweqwe")
            {
                var winner = db.Questions.Find(questionNumber).WinnerId;
                if (winner != default)
                {

                    return View("~/Views/Winner.cshtml", new GetViewModel() { WinnersName = db.Contestants.Find(winner).Nickname, Answer = db.Contestants.Find(winner).Answer});
                }
                else
                {
                    return View("~/Views/Winner.cshtml", new GetViewModel() { WinnersName = "No winners"});
                }
            }
            else
            {
                return Forbid("Wrong password");
            }
        }
    }
}
