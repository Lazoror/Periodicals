using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeriodicalsFinal.Attributes;

namespace PeriodicalsFinal.Controllers
{
    [MyAuthorize(Roles = "Admin")]
    public class TopicController : Controller
    {
        private readonly TopicRepository _topicRepository = new TopicRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string topicName, string topicManage)
        {
            if (!String.IsNullOrWhiteSpace(topicName))
            {
                if (topicManage == "Delete")
                {
                    return RedirectToAction("Delete", new { topicName = topicName });
                }
                if (topicManage == "Create")
                {
                    return RedirectToAction("Create", new { topicName = topicName });
                }
                if (topicManage == "Edit")
                {
                    return RedirectToAction("Edit", new { topicName = topicName });
                }
            }

            return View();
        }

       
        public RedirectToRouteResult Create(string topicName)
        {
            TopicModel topic = new TopicModel { TopicName = topicName };

            _topicRepository.Create(topic);
            _topicRepository.Save();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(string topicName)
        {
            TopicModel topic = _topicRepository.GetByName(topicName);

            ViewBag.topicId = topic.TopicId;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(TopicModel topic, Guid topicId)
        {
            //Guid.TryParse(topicId, out Guid topicIdGuid);

            //TopicModel topic = _topicRepository.GetById(topicIdGuid);

            topic.TopicId = topicId;

            if (topic != null)
            {
                _topicRepository.Update(topic);
                _topicRepository.Save();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        
        public RedirectToRouteResult Delete(string topicName)
        {
            TopicModel topic = _topicRepository.GetByName(topicName);

            if (!_topicRepository.IsUsedTopic(topicName))
            {
                _topicRepository.Delete(topic);
                _topicRepository.Save();

                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index");
        }
    }
}