using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PeriodicalsFinal.DataAccess.Models
{
    public enum EditionStatus
    {
        Creating,
        Active,
        Deleted
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
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

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Edition price")]
        public float EditionPrice { get; set; }

        [Required]
        [Display(Name = "Edition month")]
        public Month EditionMonth { get; set; }

        [Range(2000,2100)]
        [Display(Name = "Edition year")]
        public int EditionYear { get; set; }

        [Required]
        [Display(Name = "Edition cover")]
        public byte[] EditionImage { get; set; }
        public EditionStatus EditionStatus { get; set; }

        public Guid MagazineId { get; set; }
        public Guid TopicId { get; set; }

        public virtual MagazineModel Magazine { get; set; }

        public ICollection<ArticleModel> Articles { get; set; }
        public ICollection<SubscriptionModel> Subscriptions { get; set; }
    }
}
