﻿using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal.Controllers
{

    [Authorize(Roles = "Admin")]
    public class EditionController : Controller
    {
        private readonly EditionRepository _editionRepository = new EditionRepository();
        private readonly MagazineRepository _magazineRepository = new MagazineRepository();
        private readonly TopicRepository _topicRepository = new TopicRepository();

        [Route("{magazine}/{year}/{month}")]
        [AllowAnonymous]
        public ActionResult Index(string magazine, string year, string month)
        {
            var edition = _editionRepository.GetEdition(magazine, year, (Month)Convert.ToInt32(month));

            return View(edition);
        }

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

                return RedirectToAction("Index", "Magazine");

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
                EditionModel edition = _editionRepository.GetById(editionId);

                _editionRepository.Delete(edition);
                _editionRepository.Save();
            }

            return RedirectToAction("Index", "Home");
        }
        
    }
}