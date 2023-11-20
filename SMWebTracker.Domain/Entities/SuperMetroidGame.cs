using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Entities
{
    public class SuperMetroidGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]        
        public int PlayerCount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public bool IsOpen { get; set; }

        public DateTime? ClosedAt { get; set; }

        public Guid? ClosedBy { get; set; }

        public virtual IEnumerable<SuperMetroidTracker> SuperMetroidTrackers { get; set; }
    }
}
