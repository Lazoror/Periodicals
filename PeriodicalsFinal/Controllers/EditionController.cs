using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Repository;
using PeriodicalsFinal.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PeriodicalsFinal.Controllers
{

    [MyAuthorize(Roles = "Admin")]
    public class EditionController : Controller
    {
        private readonly EditionRepository _editionRepository = new EditionRepository();
        private readonly MagazineRepository _magazineRepository = new MagazineRepository();
        private readonly TopicRepository _topicRepository = new TopicRepository();

        [Route("{magazine}/{year}/{month}")]
        [AllowAnonymous]
        [MyCustomException]
        public ActionResult Index(string magazine, string year, string month)
        {
            Int32.TryParse(month, out int monthNum);
            Int32.TryParse(year, out int yearNum);

            if ( (monthNum > 0 && monthNum < 13) && (yearNum >= 2000 && yearNum <= 2100))
            {
                var edition = _editionRepository.GetEdition(magazine, yearNum, (Month)monthNum);

                if( (edition.EditionStatus == EditionStatus.Creating || edition.EditionStatus == EditionStatus.Deleted) && User.IsInRole("Admin"))
                {
                    ViewBag.Articles = _editionRepository.GetArticles(edition.EditionId);

                    return View(edition);
                }
                else if (edition.EditionStatus == EditionStatus.Active)
                {
                    ViewBag.Articles = _editionRepository.GetArticles(edition.EditionId);

                    return View(edition);
                }

                
            }

            return View();
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            // Get magazines for correct display for magazine select on create edition page
            var magazines = _magazineRepository.GetAll();
            ViewBag.MagazineTitles = magazines.Select(a => a.MagazineName);

            // Get topics for correct display for topic select on create edition page
            var topics = _topicRepository.GetAll();
            ViewBag.Topics = topics.Select(a => a.TopicName);

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditionModel edition, string magazineName, string customMagazine, string topicName, HttpPostedFileBase editionCover)
        {
            // Get magazines for correct display for magazine select on create edition page
            var magazines = _magazineRepository.GetAll();
            ViewBag.MagazineTitles = magazines.Select(a => a.MagazineName);

            // Get topics for correct display for topic select on create edition page
            var topics = _topicRepository.GetAll();
            ViewBag.Topics = topics.Select(a => a.TopicName);

            #region Validation

            // Validation of custom fields
            if (String.IsNullOrWhiteSpace(magazineName) && String.IsNullOrWhiteSpace(customMagazine))
            {
                ModelState.AddModelError(nameof(magazineName), "The Magazine field is reqiured.");
            }

            if (!String.IsNullOrWhiteSpace(magazineName) && !String.IsNullOrWhiteSpace(customMagazine))
            {
                ModelState.AddModelError(nameof(magazineName), "Select existing magazine or create new, not both.");
            }

            if (String.IsNullOrWhiteSpace(topicName))
            {
                ModelState.AddModelError(nameof(topicName), "The Topic field is reqiured.");
            }

            if (String.IsNullOrWhiteSpace(customMagazine) && String.IsNullOrWhiteSpace(magazineName))
            {
                ModelState.AddModelError(nameof(magazineName), "The Magazine field is reqiured.");
            }

            if (customMagazine.Length > 200 || magazineName.Length > 200)
            {
                ModelState.AddModelError(nameof(magazineName), "The Magazine name field must be less than 200 characters.");
            }

            if (edition.EditionMonth == 0)
            {
                ModelState.AddModelError(nameof(edition.EditionMonth), "The Edition month field is required.");
            }

            // Clear errors from model field, because we have our custom field
            ModelState[nameof(edition.EditionImage)].Errors.Clear();

            // Checks if image is null
            if (editionCover == null)
            {
                ModelState.AddModelError(nameof(editionCover), "Edition cover is required.");
            }
            // if not null checks for type
            else if (editionCover.ContentType != "image/png" && editionCover.ContentType != @"image/jpeg")
            {
                ModelState.AddModelError(nameof(editionCover), "Only png and jpeg allowed");
            }
            // If not null and proper type checks if file is smaller than 3mb
            else if (editionCover.ContentLength > 3000000)
            {
                ModelState.AddModelError(nameof(editionCover), "Cover must be smaller than 3mb.");
            }

            #endregion

            if (ModelState.IsValid)
            {
                string magName = String.Empty;

                // Checks what custom field for magazine selection was selected
                if (!String.IsNullOrWhiteSpace(magazineName))
                {
                    magName = magazineName;
                }
                else
                {
                    // If custom field was selected, the new Magazine is creating
                    MagazineModel magazineCreated = new MagazineModel() { MagazineId = Guid.NewGuid(), MagazineName = customMagazine };

                    _magazineRepository.Create(magazineCreated);
                    _magazineRepository.Save();
                    magName = customMagazine;
                }

                MagazineModel magazine = _magazineRepository.GetByName(magName);
                TopicModel topic = _topicRepository.GetByName(topicName);

                // Converting image to byte array
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(editionCover.InputStream))
                {
                    bytes = br.ReadBytes(editionCover.ContentLength);
                }

                // Sets retrieved data from custom fields
                edition.EditionId = Guid.NewGuid();
                edition.MagazineId = magazine.MagazineId;
                edition.TopicId = topic.TopicId;
                edition.EditionStatus = EditionStatus.Creating;
                edition.EditionImage = bytes;

                _editionRepository.Create(edition);
                _editionRepository.Save();

                return RedirectToAction("Index", "Edition", new { magazine = magazine.MagazineName, year = edition.EditionYear, month = (int)edition.EditionMonth});

            }

            return View(edition);
        }

        [HttpGet]
        public ActionResult Delete(Guid editionId)
        { 
            return View(editionId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string deleteConfirm, Guid editionId)
        {
            if(deleteConfirm == "Yes")
            {
                _editionRepository.DeleteById(editionId);
                _editionRepository.Save();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [MyAuthorize()]
        public ActionResult Subscribe()
        {
            return View();
        }

        public ActionResult Edit(Guid editionId)
        {
            // Get magazines for correct display for magazine select on create edition page
            var magazines = _magazineRepository.GetAll();
            ViewBag.MagazineTitles = magazines.Select(a => a.MagazineName);

            // Get topics for correct display for topic select on create edition page
            var topics = _topicRepository.GetAll();
            ViewBag.Topics = topics.Select(a => a.TopicName);

            EditionModel edition = _editionRepository.GetById(editionId);

            return View(edition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditionModel edition, Guid editionId, string magazineName, string customMagazine, string topicName, HttpPostedFileBase editionCoverNew, string coverOld)
        {
            #region Validation

            // Validation of custom fields
            if (String.IsNullOrWhiteSpace(magazineName) && String.IsNullOrWhiteSpace(customMagazine))
            {
                ModelState.AddModelError(nameof(magazineName), "The Magazine field is reqiured.");
            }

            if (!String.IsNullOrWhiteSpace(magazineName) && !String.IsNullOrWhiteSpace(customMagazine))
            {
                ModelState.AddModelError(nameof(magazineName), "Select existing magazine or create new, not both.");
            }

            if (String.IsNullOrWhiteSpace(topicName))
            {
                ModelState.AddModelError(nameof(topicName), "The Topic field is reqiured.");
            }

            if (String.IsNullOrWhiteSpace(customMagazine) && String.IsNullOrWhiteSpace(magazineName))
            {
                ModelState.AddModelError(nameof(magazineName), "The Magazine field is reqiured.");
            }

            if (customMagazine.Length > 200 || magazineName.Length > 200)
            {
                ModelState.AddModelError(nameof(magazineName), "The Magazine name field must be less than 200 characters.");
            }

            if (edition.EditionMonth == 0)
            {
                ModelState.AddModelError(nameof(edition.EditionMonth), "The Edition month field is required.");
            }

            // Clear errors from model field, because we have our custom field
            ModelState[nameof(edition.EditionImage)].Errors.Clear();

            if(editionCoverNew != null)
            {
                // if not null checks for type
                if (editionCoverNew.ContentType != "image/png" && editionCoverNew.ContentType != @"image/jpeg")
                {
                    ModelState.AddModelError(nameof(edition.EditionImage), "Only png and jpeg allowed");
                }
                // If not null and proper type checks if file is smaller than 3mb
                else if (editionCoverNew.ContentLength > 3000000)
                {
                    ModelState.AddModelError(nameof(edition.EditionImage), "Cover must be smaller than 3mb.");
                }
            }

            #endregion

            // Get magazines for correct display for magazine select on create edition page
            var magazines = _magazineRepository.GetAll();
            ViewBag.MagazineTitles = magazines.Select(a => a.MagazineName);

            // Get topics for correct display for topic select on create edition page
            var topics = _topicRepository.GetAll();
            ViewBag.Topics = topics.Select(a => a.TopicName);

            if (ModelState.IsValid)
            {
                string magName = String.Empty;

                // Checks what custom field for magazine selection was selected
                if (!String.IsNullOrWhiteSpace(magazineName))
                {
                    magName = magazineName;
                }
                else
                {
                    // If custom field was selected, the new Magazine is creating
                    MagazineModel magazineCreated = new MagazineModel() { MagazineId = Guid.NewGuid(), MagazineName = customMagazine };

                    _magazineRepository.Create(magazineCreated);
                    _magazineRepository.Save();
                    magName = customMagazine;
                }

                TopicModel topic = _topicRepository.GetByName(topicName);
                MagazineModel magazine = _magazineRepository.GetByName(magName);

                // Convert image to byte array
                byte[] bytes;

                if (editionCoverNew != null)
                {
                    using (BinaryReader br = new BinaryReader(editionCoverNew.InputStream))
                    {
                        bytes = br.ReadBytes(editionCoverNew.ContentLength);
                    }
                }
                else
                {
                    bytes = Convert.FromBase64String(coverOld);
                }

                edition.EditionImage = bytes;
                edition.EditionId = editionId;
                edition.MagazineId = magazine.MagazineId;
                edition.TopicId = topic.TopicId;

                _editionRepository.Update(edition);
                _editionRepository.Save();

                return RedirectToAction("Index", "Edition", new { magazine = magazine.MagazineName, year = edition.EditionYear, month = (int)edition.EditionMonth });
            }

            return View(edition);
        }

        [MyAuthorize(Roles = "Admin, Publisher")]
        public ActionResult Creating()
        {
            var creatingEditions = _editionRepository.GetCreating();

            ViewBag.editions = creatingEditions;

            return View();
        }

        public ActionResult Deleted()
        {
            var deletedEditions = _editionRepository.GetDeleted();

            ViewBag.editions = deletedEditions;

            return View();
        }

        public ActionResult Activate(Guid editionId)
        {
            _editionRepository.Activate(editionId);
            _editionRepository.Save();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(Guid editionId)
        {
            _editionRepository.Remove(editionId);
            _editionRepository.Save();

            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public  ActionResult Subscribe()
        //{
        //    return View();
        //}

    }
}