using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Models
{

    [Table(name: "Topic")]
    public class TopicModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid TopicId { get; set; }

        public string TopicName { get; set; }

    }
}
