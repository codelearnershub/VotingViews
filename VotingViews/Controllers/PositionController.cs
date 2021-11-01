using Microsoft.AspNetCore.Authorization;
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
    
    public class PositionController : Controller
    {
        private readonly IPositionService _position;
        private readonly IElectionService _election;

        public PositionController(IElectionService election, IPositionService position)
        {
            _election = election;
            _position = position;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _position.ListOfPositions();
            return View(model);
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
        public IActionResult Create(CreatePosition model)
        {
            CreatePositionDto create = new CreatePositionDto
            {
                Name = model.Name
            };
            if (ModelState.IsValid)
            {
                _position.AddPosition(create);
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
