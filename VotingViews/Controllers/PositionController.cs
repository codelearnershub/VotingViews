using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Controllers
{
    [Authorize(Roles = "admin")]
    public class PositionController : Controller
    {
        private readonly IPositionService _position;
        private readonly IElectionService _election;

        public PositionController(IElectionService election, IPositionService position)
        {
            _election = election;
            _position = position;
        }

        [HttpGet]
        public  IActionResult Index(string searchString, string electionPosition )
        {
            IEnumerable<string> electionQuery = from p in _position.ListOfPositions()
                                               orderby p.Election.Name
                                               select p.Election.Name;
            var positions = from p in _position.ListOfPositions()
                            select p;

           

            if (!string.IsNullOrEmpty(searchString))
            {
                positions = positions.Where(p => p.Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(electionPosition))
            {
                positions = positions.Where(x => x.Election.Name == electionPosition);
            }

            var positionListFilterViewModel = new PositionListFilterViewModel
            {
                Elections = new SelectList(electionQuery.Distinct().ToList()),
                Positions = positions.ToList()

            };
            //var model = _position.ListOfPositions();
            return View(positionListFilterViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<ElectionDto> elections = _election.GetAllElections();
            List<SelectListItem> listElections = new List<SelectListItem>();
            foreach(ElectionDto election in elections)
            {
                SelectListItem item = new SelectListItem(election.Name, election.Id.ToString());
                listElections.Add(item);
            }
            ViewBag.Elections = listElections;
            return View();
        }

        [HttpPost]
        public IActionResult Create(PositionDto model)
        {
            if (ModelState.IsValid)
            {
                _position.AddPosition(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var update = _position.GetPositionById(id.Value);
            if (update == null)
            {
                return NotFound();
            }
            return View(update);
        }

        [HttpPost]
        public IActionResult Update(int id, UpdatePosition model)
        {
            UpdatePositionDto update = new UpdatePositionDto
            {
                Name = model.Name
            };
            if (ModelState.IsValid)
            {
                _position.UpdatePosition(update, id);
                return RedirectToAction("Index", "Position");
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var details = _position.GetPositionById(id.Value);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Details(int id, Position position)
        {
            _position.GetPositionById(id);
            PositionVM model = new PositionVM
            {
                Name = position.Name
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _position.GetPositionById(id.Value);
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

            _position.DeletePosition(id);
            return RedirectToAction("Index", "Position");
        }


    }
}
