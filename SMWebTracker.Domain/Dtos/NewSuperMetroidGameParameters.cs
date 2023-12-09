using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Dtos
{
    public class NewSuperMetroidGameParameters
    {
        public List<string> PlayerNames { get; set; } = new List<string>();

        public string Description { get; set; } = "Super Metroid";
    }
}
