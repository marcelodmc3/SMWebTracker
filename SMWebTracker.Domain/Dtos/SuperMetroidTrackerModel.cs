using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Dtos
{
    public class SuperMetroidTrackerModel
    {        
        public int? Position { get; set; }

        public bool? VariaSuit { get; set; }
        
        public bool? GravitySuit { get; set; }
        
        public bool? ChargeBeam { get; set; }
        
        public bool? IceBeam { get; set; }
        
        public bool? WaveBeam { get; set; }
        
        public bool? SpazerBeam { get; set; }
        
        public bool? PlasmaBeam { get; set; }
        
        public bool? MorphBall { get; set; }
        
        public bool? Bombs { get; set; }
        
        public bool? HighJumpBoots { get; set; }
       
        public bool? SpeedBooster { get; set; }
        
        public bool? SpaceJump { get; set; }
        
        public bool? SpringBall { get; set; }

        public bool? Kraid { get; set; }

        public bool? Phantoon { get; set; }
        
        public bool? Draygon { get; set; }
        
        public bool? Ridley { get; set; }
        
        public bool? ScrewAttack { get; set; }
        
        public bool? SporeSpawn { get; set; }
        
        public bool? Crocomire { get; set; }
        
        public bool? Botwoon { get; set; }
        
        public bool? GoldenTorizo { get; set; }

        public bool? Grapple { get; set; }

        public bool? Xray { get; set; }
    }
}
