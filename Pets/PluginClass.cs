using HarmonyLib;
using Synapse.Api;
using Synapse.Api.Plugin;
using Synapse.Translation;
using System;

namespace Pets
{
    [PluginInformation(
        Name = "Pets",
        Author = "Dimenzio",
        Description = "A Plugin for Pets in SL",
        LoadPriority = 0,
        SynapseMajor = 2,
        SynapseMinor = 8,
        SynapsePatch = 3,
        Version = "v.1.0.3"
        )]
    public class PluginClass : AbstractPlugin
    {
        [API]
        public PetHandler PetHandler { get; } = new PetHandler();

        [Config(section = "Pets")]
        public PluginConfig Config { get; set; }

        [SynapseTranslation]
        public new SynapseTranslation<PluginTranslation> Translation { get; set; }

        [API]
        public static PluginClass PetPlugin;

        public override void Load()
        {
            Translation.AddTranslation(new PluginTranslation());
            Translation.AddTranslation(new PluginTranslation()
            {
                CommandText = "\\n=====Pets=====\\n.Pet Spawn PetID : Spawnt eins deiner Pets\\n.Pet player : zeigt dir alle pets eines anderen Spielers\\nPets welche du besitzt:%petlist%\\n==============",
                OtherPet = "\\n=====Pets=====\\nAlle Pets vom Spieler:%petlist%\\n==============",
                MissingID = "ID fehlt",
                InvalidID = "Ungültige ID",
                Denied = "Du bist nicht erlaubt dieses Pet zu spawnen",
                Limit = "Du kannst keine weiteren dieser Pets spawnen",
                NoFound = "Kein Spieler wurde gefunden",
                Spawned = "Pet wurde gespawnnt"
            }, "GERMAN");
            PetPlugin = this;
            PetHandler.LoadPets();
            new EventHandlers();
            base.Load();

            try
            {
                var instance = new Harmony("pets.patches");
                instance.PatchAll();
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Pets Harmony Patching failed:\n{e}");
            }
        }

        public override void ReloadConfigs() => PetHandler.LoadPets();
    }
}
