using System.ComponentModel;
using Synapse.Config;

namespace Pets
{
    public class PluginConfig : AbstractConfigSection
    {
        [Description("The max Amount of Pets a Player can have at the same time")]
        public int MaxPets { get; set; } = 1;

        [Description("If enabled only the Owner of the pet can see it")]
        public bool InvisiblePet { get; set; } = false;

        [Description("If Enabled the Name of the Owner will also be displayed")]
        public bool ShowOwnerName { get; set; } = true;

        [Description("If Disabled the Role of the Pet will no longer be displayed")]
        public bool ShowRole { get; set; } = false;
    }
}
