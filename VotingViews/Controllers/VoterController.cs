using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingViews.Context;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Controllers
{
   [AllowAnonymous]
    public class VoterController : Controller
    {
        private readonly IVoterService _service;
        private readonly IVoteService _vote;
        private readonly ApplicationContext _context;
        private readonly IElectionService _election;
        private readonly IPositionService _position;
        private readonly IContestantService _contestant;
        private readonly IResultService _result;

        public VoterController(IVoterService service, ApplicationContext context, IElectionService election, IPositionService position, IContestantService contestant, IVoteService vote, IResultService result)
        {
            _service = service;
            _context = context;
            _election = election;
            _position = position;
            _contestant = contestant;
            _vote = vote;
            _result = result;
        }

        [Authorize(Roles ="admin")]
        public IActionResult Index()
        {
            var model = _service.GetAll();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "voter")]
        public IActionResult DashBoard(string message)
        {
            ViewBag.Message = message;
            return View();
        }

       
        [AllowAnonymous]
        public IActionResult Result(int? id, Guid code )
        {
            var result = _contestant.GetContestantByPositionId(id.Value);

            ResultPageDto model = new ResultPageDto
            {
                Contestants = result,
                Code = code
            };

            return View( model);
        }

        [AllowAnonymous]
        public IActionResult Election(Guid code)
        {
            string message = "";
            Guid vcode = Guid.Empty;  
            if (code == vcode)
            {
                message = "Enter Election Code";
                return RedirectToAction(nameof(DashBoard), message);
            }
            else
            {
                var elect = _election.GetElectionByCode(code);
                if (elect == null )
                {
                    message = "Invalid Election Code";
                    return RedirectToAction(nameof(DashBoard), message);
                }

                var election = _position.GetPositionByElectionCode(code);

                if (elect.StartDate > DateTime.Now)
                {
                    return View(elect);
                }
                else if (elect.StartDate <= DateTime.Now && elect.EndDate > DateTime.Now)
                {
                    return View(elect);
                }
                else if (elect.EndDate <= DateTime.Now)
                {
                    return View(elect);
                }
               
            }
            return View();

        }

        [AllowAnonymous]
        public IActionResult CompletedElection()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var model = _service.FindById(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _service.DeleteVoter(id);
            return RedirectToAction("Index", "Voter");
        }

        [Authorize(Roles = "voter")]
        [HttpGet]
        public IActionResult VoterVote(int? id, Guid code)
        {
            var position = _contestant.GetContestantByPositionId(id.Value);
            ResultPageDto model = new ResultPageDto
            {
                Contestants = position,
                Code = code
            };
            return View(model);
        }

        [Authorize(Roles = "voter")]
        public IActionResult Vote(int positionId, int contestantId, Guid code)
        {
            var loggedInUserEmail = User.FindFirst(ClaimTypes.Name).Value;

            var vote = _vote.Vote(positionId, loggedInUserEmail, contestantId);
            string message = "";
            if (vote == null)
            {
                message = "You Have already voted for this Position before.";
            }
            else
            {
                message = "Thanks for Voting.";
            }

            return RedirectToAction("AlreadyVoted", "Voter", new { id = positionId, message, code });
            
        }
        [AllowAnonymous]
        public IActionResult AlreadyVoted(int id, string message, Guid code)
        {
            ViewBag.Message = message;
            var position = _position.GetPositionById(id);
            ResultPageDto model = new ResultPageDto
            {
                Position = position,
                Code = code
            };
            return View(model);

           
        }

        [HttpPost]
        public IActionResult AlreadyVoted(Guid code)
        {
            var listPosition = _position.GetPositionByElectionCode(code);
            return View(listPosition);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var voter = _service.FindById(id.Value);
            if (voter == null)
            {
                return NotFound();
            }

            return View(voter);
        }

        [Authorize(Roles = "voter")]
        public IActionResult Profile()
        {
            var email = User.FindFirst(ClaimTypes.Name).Value;
            var voter = _service.FindByEmail(email);
            return View(voter);
        }

        [Authorize(Roles = "voter")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            var update = _service.FindById(id.Value);
            if (update == null)
            {
                return NotFound();
            }
            return View(update);
        }
        [Authorize(Roles = "voter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, UpdateVoterVM voter)
        {
            UpdateVoterDto voterDto = new UpdateVoterDto
            {
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                MiddleName = voter.MiddleName,
                Password = voter.Password,
                Address = voter.Address
            };

            if (ModelState.IsValid)
            {
                _service.Update(voterDto, id);

                return RedirectToAction(nameof(Profile));
            }
            return View(voter);
        }
        [Authorize(Roles ="voter")]
        [HttpPost]
        public IActionResult UpdatePassword()
        {
            return RedirectToAction(nameof(Update));
        }

        [AllowAnonymous]
        public IActionResult ContestantDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = _contestant.GetContestantById(id.Value);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }
    }
}
