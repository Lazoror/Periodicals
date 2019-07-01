using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PeriodicalsFinal.DataAccess.Models
{
    public enum ActiveStatus
    {
        Active,
        Deleted
    }

    [Table(name: "Article")]
    public class ArticleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid ArticleId { get; set; }
        public Guid EditionId { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Display(Name = "Article title")]
        public string ArticleTitle { get; set; }

        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 300)]
        [Required]
        [AllowHtml]
        [Display(Name = "Article Content")]
        public string ArticleContent { get; set; }

        [Display(Name = "Article short description")]
        public string ShortDescription { get; set; }
        public ActiveStatus ArticleStatus { get; set; }

        public virtual EditionModel Edition { get; set; }
    }
}
