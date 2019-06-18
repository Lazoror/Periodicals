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
            throw new NotImplementedException();
        }

        public void Delete(TopicModel entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TopicModel> GetAll()
        {
            return _db.Topics.ToList();
        }

        public TopicModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TopicModel entity)
        {
            throw new NotImplementedException();
        }

        public TopicModel GetByName(string name)
        {
            return _db.Topics.FirstOrDefault(a => a.TopicName == name);
        }
    }
}
