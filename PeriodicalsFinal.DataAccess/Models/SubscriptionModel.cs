using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Models
{

    [Table(name: "Subscription")]
    public class SubscriptionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid SubscriptionId { get; set; }
        public Guid EditionId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public virtual EditionModel Edition { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
