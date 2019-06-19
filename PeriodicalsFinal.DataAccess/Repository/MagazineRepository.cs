using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class MagazineRepository : IRepository<MagazineModel>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(MagazineModel entity)
        {
            _db.Magazines.Add(entity);
        }

        public void Delete(MagazineModel entity)
        {
            entity.MagazineStatus = ActiveStatus.Deleted;
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<MagazineModel> GetAll()
        {
            return _db.Magazines.ToList();
        }

        public MagazineModel GetById(Guid id)
        {
            return _db.Magazines.FirstOrDefault(a => a.MagazineId == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(MagazineModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public MagazineModel GetByName (string name)
        {
            return _db.Magazines.FirstOrDefault(a => a.MagazineName == name);
        }
    }
}
