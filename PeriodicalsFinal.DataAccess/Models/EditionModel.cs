using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodicalsFinal.DataAccess.Models
{
    public enum EditionStatus
    {
        Active,
        Archived,
        Deleted
    }

    [Table(name:"Edition")]
    public class EditionModel
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EditionId { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Display(Name = "Edition title")]
        public string EditionTitle { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Edition title")]
        public DateTime PublishDate { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Edition price")]
        public float EditionPrice { get; set; }
        public Guid TopicId { get; set; }
        public byte[] EditionImage { get; set; }
        public EditionStatus EditionStatus { get; set; }

        public ICollection<ArticleModel> Articles { get; set; }
        public ICollection<SubscriptionModel> Subscriptions { get; set; }
    }
}
