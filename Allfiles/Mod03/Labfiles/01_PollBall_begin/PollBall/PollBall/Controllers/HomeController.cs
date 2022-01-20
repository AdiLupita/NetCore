using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PollBall.Models;
using PollBall.Services;

namespace PollBall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPollResultsService _pollResults;

        public HomeController(
            IPollResultsService pollResults
            ,ILogger<HomeController> logger)
        {
            _pollResults = pollResults;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Request.Query.ContainsKey("submitted"))
            {
                StringBuilder results = new StringBuilder();
                SortedDictionary<SelectedGame, int> voteList = _pollResults.GetVoteResult();
                foreach (var gameVotes in voteList)
                    results.Append($"Game name: {gameVotes.Key}. Votes: {gameVotes}{Environment.NewLine}");
                return Content(results.ToString());
            }
            else
            {
                return Redirect("poll-questions.html");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
