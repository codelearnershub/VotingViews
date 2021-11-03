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
        private readonly IPositionService _position;

        public ContestantController(IContestantService contestant, IPositionService position)
        {
            _contestant = contestant;
            _position = position;
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
            List<PositionDto> positions = _position.ListOfPositions();
            List<SelectListItem> listContestants = new List<SelectListItem>();
            foreach (PositionDto position in positions)
            {
                SelectListItem item = new SelectListItem(position.Name, position.Id.ToString());
                listContestants.Add(item);
            }
            ViewBag.Positions = listContestants;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Contestant model)
        {
            if (ModelState.IsValid)
            {
                _contestant.AddContestant(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
