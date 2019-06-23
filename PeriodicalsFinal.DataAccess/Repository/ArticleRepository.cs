using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class ArticleRepository : IRepository<ArticleModel>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(ArticleModel entity)
        {
            _db.Articles.Add(entity);
        }

        public void Delete(ArticleModel entity)
        {
            entity.ArticleStatus = ActiveStatus.Deleted;
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<ArticleModel> GetAll()
        {
            return _db.Articles.ToList();
        }

        public ArticleModel GetById(Guid id)
        {
            return _db.Articles.FirstOrDefault(a => a.ArticleId == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ArticleModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
