﻿using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class EditionRepository : IRepository<EditionModel>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Create(EditionModel entity)
        {
            _db.Editions.Add(entity);
        }

        public void Delete(EditionModel entity)
        {
            entity.EditionStatus = EditionStatus.Deleted;
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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
    }
}
