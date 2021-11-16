using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Controllers
{
    [Authorize(Roles = "admin")]
    public class ContestantController : Controller
    {
        private readonly IContestantService _contestant;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPositionService _position;
        private readonly IElectionService _election;
        private readonly IVoteService _vote;

        public ContestantController(IContestantService contestant, IPositionService position, IVoteService vote, IElectionService election, IWebHostEnvironment webHostEnvironment)
        {
            _contestant = contestant;
            _position = position;
            _vote = vote;
            _election = election;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
           var model =await _contestant.ListOfContestants();
            return View(model);
        }

        //public IActionResult Vote(int id)
        //{
        //    _vote.Vote(id, User.Identity.Name);
        //    return View();
        //}

        [HttpGet]
        public IActionResult Create()
        {
            List<ElectionDto> elections =_election.GetAllElections();
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
        public IActionResult Create(CreateContestant model, IFormFile file)
        {
            
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "contestants");
                    Directory.CreateDirectory(imageDirectory);
                    string contentType = file.ContentType.Split('/')[1];
                    string fileName = $"{Guid.NewGuid()}.{contentType}";
                    string fullPath = Path.Combine(imageDirectory, fileName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ItemPictureURL = fileName;
                }
                _contestant.AddContestant(model);
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
