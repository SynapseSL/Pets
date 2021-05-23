using Synapse.Translation;

namespace Pets
{
    public class PluginTranslation : IPluginTranslation
    {
        public string CommandText { get; set; } = "\\n=====Pets=====\\n.Pet Spawn PetID : spawns one of your Pets\\n.Pet player : shows you all Pets from the Player\\nPets you have:%petlist%\\n==============";

        public string OtherPet { get; set; } = "\\n=====Pets=====\\nAll the Pets of the Player:%petlist%\\n==============";

        public string MissingID { get; set; } = "Missing ID";

        public string InvalidID { get; set; } = "Invalid ID";

        public string Denied { get; set; } = "You can't use this Pet";

        public string Spawned { get; set; } = "Pet was spawned";

        public string Limit { get; set; } = "You can't spawn more of these pets";

        public string NoFound { get; set; } = "No Player was found";

        public string Owner { get; set; } = "%player%'s Pet";
    }
}
