using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Models
{
    [Table(name: "Magazine")]
    public class MagazineModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MagazineId { get; set; }

        [MaxLength(350)]
        [Required]
        [DisplayName("Magazine name")]
        public string MagazineName { get; set; }
    }
}
