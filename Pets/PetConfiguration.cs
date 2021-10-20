using System.Collections.Generic;
using Synapse.Config;

namespace Pets
{
    public class PetConfiguration : IConfigSection
    {
        public int PetID { get; set; }
        public RoleType Role { get; set; }
        public bool GodMode { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public string Badge { get; set; }
        public string BadgeColor { get; set; }
        public SerializedVector3 Scale { get; set; }
        public ItemType ItemInHand { get; set; }
        public int MaxAmount { get; set; }
        public bool EveryoneCanUse { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> Users { get; set; }
    }
}
