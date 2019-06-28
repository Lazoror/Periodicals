using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class EditionRepository : IRepository<EditionModel>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(EditionModel entity)
        {
            if (_db.Editions.FirstOrDefault(a => a.EditionTitle == entity.EditionTitle && a.MagazineId == entity.MagazineId && a.EditionYear == entity.EditionYear && a.EditionMonth == entity.EditionMonth) == null)
            {
                _db.Editions.Add(entity);
            }

        }

        public void Delete(EditionModel entity)
        {
            entity.EditionStatus = EditionStatus.Deleted;
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteById(Guid editionId)
        {
            EditionModel edition = GetById(editionId);

            edition.EditionStatus = EditionStatus.Deleted;
            _db.Entry(edition).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<EditionModel> GetAll()
        {
            return _db.Editions.ToList();
        }

        public EditionModel GetById(Guid id)
        {
            return _db.Editions.FirstOrDefault(a => a.EditionId == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(EditionModel entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<EditionModel> GetPublisherEditions(Guid publisherId)
        {
            return null;
        }

        public EditionModel GetEdition(string magazineName, int year, Month month)
        {
            int editionYear = Convert.ToInt32(year);
            magazineName = magazineName.ToLower();
            return _db.Editions.FirstOrDefault(a => a.Magazine.MagazineName.ToLower() == magazineName && a.EditionYear == editionYear && a.EditionMonth == month);
        }

        public IEnumerable<EditionModel> GetCreating()
        {
            return _db.Editions.Where(a => a.EditionStatus == EditionStatus.Creating).ToList();
        }

        public IEnumerable<EditionModel> GetDeleted()
        {
            return _db.Editions.Where(a => a.EditionStatus == EditionStatus.Deleted).ToList();
        }

        public IEnumerable<ArticleModel> GetArticles(Guid editionId)
        {
            return _db.Editions.Where(a => a.EditionId == editionId).Include(a => a.Articles).SelectMany(a => a.Articles);
        }

        public void Remove(Guid editionId)
        {
            EditionModel edition = GetById(editionId);

            _db.Entry(edition).State = EntityState.Deleted;
        }

        public void Activate(Guid editionId)
        {
            EditionModel edition = GetById(editionId);
            edition.EditionStatus = EditionStatus.Active;

            _db.Entry(edition).State = EntityState.Modified;
        }

    }
}
