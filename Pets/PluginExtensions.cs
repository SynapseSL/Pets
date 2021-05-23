using Synapse.Api;
using System.Collections.Generic;
using System.Linq;

namespace Pets
{
    public static class PluginExtensions
    {
        public static List<int> GetDatabasePets(this Player player)
        {
            try
            {
                var petstring = player.GetData("pets");
                var pets = petstring.Split(';');
                return pets.Select(x => int.Parse(x)).ToList();
            }
            catch
            {
                return new List<int>();
            }
        }

        public static void SetDatabasePets(this Player player, List<int> pets) => player.SetData("pets", string.Join(";", pets));

        public static bool CanUsePet(this Player player, int id)
        {
            var conf = PluginClass.PetPlugin.PetHandler.GetPet(id);

            if (conf == null) return false;

            if (conf.EveryoneCanUse) return true;

            if (conf.Users != null && conf.Users.Contains(player.UserId) ) return true;

            if (conf.Permissions != null && conf.Permissions.Any(x => player.HasPermission(x))) return true;

            var pets = player.GetDatabasePets();

            if (pets != null && pets.Contains(id)) return true;

            return false;
        }

        public static List<PetConfiguration> GetAvailablePets(this Player player)
        {
            var list = new List<PetConfiguration>();

            foreach (var conf in PluginClass.PetPlugin.PetHandler.Pets)
                if (player.CanUsePet(conf.PetID))
                    list.Add(conf);

            return list;
        }

        public static PetOwnerScript GetPetOwnerScript(this Player player) => player.GetComponent<PetOwnerScript>();
    }
}
