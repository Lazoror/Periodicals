using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class SubscribeRepository : IRepository<SubscriptionModel>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(SubscriptionModel entity)
        {
            if(_db.Subscriptions.FirstOrDefault(a => a.EditionId == entity.EditionId && a.Id == entity.Id) == null)
            {
                entity.SubscriptionId = Guid.NewGuid();
                _db.Subscriptions.Add(entity);
            }  
        }

        public void Delete(SubscriptionModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<SubscriptionModel> GetAll()
        {
            return _db.Subscriptions.ToList();
        }

        public SubscriptionModel GetById(Guid id)
        {
            return _db.Subscriptions.FirstOrDefault(a => a.SubscriptionId == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(SubscriptionModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
