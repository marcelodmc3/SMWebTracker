using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Entities
{
    public class SuperMetroidTracker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid SuperMetroidGameId { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        public int PlayerIndex { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        [MinLength(3, ErrorMessage = "Nome do usário precisa ter no mínimo 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "Nome do usário precisa ter no máximo 200 caracteres.")]
        public string PlayerName { get; set; }

        [Required]
        public bool VariaSuit { get; set; }

        [Required]
        public bool GravitySuit { get; set; }

        [Required]
        public bool ChargeBeam { get; set; }

        [Required]
        public bool IceBeam { get; set; }

        [Required]
        public bool WaveBeam { get; set; }

        [Required]
        public bool SpazerBeam { get; set; }

        [Required]
        public bool PlasmaBeam { get; set; }

        [Required]
        public bool MorphBall { get; set; }

        [Required]
        public bool BombJump { get; set; }

        [Required]
        public bool HighJumpBoots { get; set; }

        [Required]
        public bool SpeedBooster { get; set; }

        [Required]
        public bool SpaceJump { get; set; }

        [Required]
        public bool SpringBall { get; set; }

        [Required]
        public bool Kraid { get; set; }

        [Required]
        public bool Phantoon { get; set; }

        [Required]
        public bool Draygon { get; set; }

        [Required]
        public bool Ridey { get; set; }

        [Required]
        public bool ScrewAttack { get; set; }

        [Required]
        public bool SporeSpawn { get; set; }

        [Required]
        public bool Crocomire { get; set; }

        [Required]
        public bool Botwoon { get; set; }

        [Required]
        public bool GoldenTorizo { get; set; }
    }
}
