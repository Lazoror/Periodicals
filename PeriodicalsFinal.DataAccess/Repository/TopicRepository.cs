using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class TopicRepository : IRepository<TopicModel>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(TopicModel entity)
        {
            entity.TopicId = Guid.NewGuid();
            _db.Topics.Add(entity);
        }

        public void Delete(TopicModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<TopicModel> GetAll()
        {
            return _db.Topics.ToList();
        }

        public TopicModel GetById(Guid id)
        {
            return _db.Topics.FirstOrDefault(a => a.TopicId == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(TopicModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public TopicModel GetByName(string name)
        {
            return _db.Topics.FirstOrDefault(a => a.TopicName == name);
        }

        public bool IsUsedTopic(string name)
        {
            TopicModel topic = GetByName(name);

            return _db.Editions.Any(a => a.TopicId == topic.TopicId);
        }
    }
}
