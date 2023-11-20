using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Dtos
{
    public class NewSuperMetroidGameModel
    {
        public Guid SuperMetroidGameId { get; set; }

        public List<GuidIndexModel> SuperMetroidGameTrackers { get; set; }
    }
}
