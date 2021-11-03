using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Controllers
{
    public class ContestantController : Controller
    {
        private readonly IContestantService _contestant;
        private readonly IElectionService _electionService;
        private readonly IPositionService _position;

        public ContestantController(IContestantService contestant, IPositionService position, IElectionService electionService)
        {
            _contestant = contestant;
            _position = position;
            _electionService = electionService;
        }

        [HttpGet]
        public async  Task<IActionResult> Index()
        {
           var model =await _contestant.ListOfContestants();
            return View(model);
        }

        public async Task<IActionResult> Vote(int id)
        {
            await _contestant.VoteContestant(id, User.Identity.Name);
            //return RedirectToAction("Vote", "Contestant");
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<ElectionDto> elections =_electionService.GetAllElections();
            List<SelectListItem> listContestants = new List<SelectListItem>();
            foreach (ElectionDto election in elections)
            {
                SelectListItem item = new SelectListItem(election.Name, election.Id.ToString());
                listContestants.Add(item);
            }
            ViewBag.Elections = listContestants;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateContestant model)
        {
            Contestant contestant = new Contestant
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Gender = model.Gender,
                Email = model.Email,
                PositionId = model.PositionId,
                
            };
            if (ModelState.IsValid)
            {
                _contestant.AddContestant(contestant);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public JsonResult GetPositionsByElectionId(string id)
        {
            var electionId = id.Replace('_', ' ');

            var positions = _position.GetPositionByElectionId(Convert.ToInt32(electionId));

            List<PositionVM> positionVms = new List<PositionVM>();

            foreach (var position in positions)
            {
                PositionVM equipmentBrandVM = new PositionVM
                {
                    Id = position.Id,
                    Name = position.Name
                };

                positionVms.Add(equipmentBrandVM);
            }

            var res = Json(positionVms);
            return res;
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {

            var update = _contestant.GetContestantById(id.Value);
            if (update == null)
            {
                return NotFound();
            }
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(UpdateContestant model, int id)
        {

            UpdateContestantDto contestantDto = new UpdateContestantDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email
            };

            if (ModelState.IsValid)
            {
                _contestant.UpdateContestant(contestantDto, id);
                return RedirectToAction("Index", "Contestant");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
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
        [HttpPost]
        public IActionResult Details( Contestant contestant)
        {
            _contestant.GetContestantById(contestant.Id);
            ContestantVM model = new ContestantVM
            {
               FirstName = contestant.FirstName,
               LastName = contestant.LastName,
               MiddleName = contestant.MiddleName,
               Gender = contestant.Gender,
               Email= contestant.Email
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var model = _contestant.GetContestantById(id.Value);
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

            _contestant.DeleteContestant(id);
            return RedirectToAction("Index", "Contestant");
        }
    }
}
