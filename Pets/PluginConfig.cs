using Synapse.Config;
using System.ComponentModel;

namespace Pets
{
    public class PluginConfig : AbstractConfigSection
    {
        [Description("The max Amount of Pets a Player can have at the same time")]
        public int MaxPets = 1;

        [Description("If enabled only the Owner of the pet can see it")]
        public bool InvisiblePet = false;

        [Description("If Enabled the Name of the Owner will also be displayed")]
        public bool ShowOwnerName = true;

        [Description("If Disabled the Role of the Pet will no longer be displayed")]
        public bool ShowRole = false;
    }
}
