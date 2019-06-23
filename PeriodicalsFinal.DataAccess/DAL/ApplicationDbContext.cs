using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using PeriodicalsFinal.DataAccess.Models;

namespace PeriodicalsFinal.DataAccess.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PeriodicalsConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<SubscriptionModel> Subscriptions { get; set; }
        public virtual DbSet<EditionModel> Editions { get; set; }
        public virtual DbSet<ArticleModel> Articles { get; set; }
        public virtual DbSet<TopicModel> Topics { get; set; }
        public virtual DbSet<MagazineModel> Magazines { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleModel>()
                        .HasRequired(r => r.Edition)
                        .WithMany(s => s.Articles)
                        .HasForeignKey(f => f.EditionId)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}
