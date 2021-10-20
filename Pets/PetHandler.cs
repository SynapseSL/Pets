using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Synapse;
using Synapse.Api;
using Synapse.Config;

namespace Pets
{
    public class PetHandler
    {
        internal List<PetConfiguration> Pets { get; } = new List<PetConfiguration>();

        [API]
        public PetConfiguration GetPet(int ID) => Pets.FirstOrDefault(x => x.PetID == ID);

        [API]
        public bool PetIsRegistered(int ID) => Pets.Any(x => x.PetID == ID);

        internal void LoadPets()
        {
            var spath = Path.Combine(Server.Get.Files.SharedConfigDirectory, "pets");
            var path = Path.Combine(Server.Get.Files.ConfigDirectory, "pets");

            if (!Directory.Exists(spath)) Directory.CreateDirectory(spath);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var files = Directory.GetFiles(spath, "*.syml").ToList();
            files.AddRange(Directory.GetFiles(path, "*.syml"));

            if (files.Count == 0) CreateExampleFile(spath);
            Pets.Clear();

            foreach (var file in files)
            {
                try
                {
                    var syml = new SYML(file);
                    syml.Load();

                    if (syml.Sections.Count == 0) continue;
                    var config = syml.Sections.First().Value.LoadAs<PetConfiguration>();
                    Pets.Add(config);
                }
                catch (Exception e)
                {
                    Synapse.Api.Logger.Get.Send($"Pets: Error while loading a pet:\n{e}", System.ConsoleColor.Red);
                }
            }
        }

        private void CreateExampleFile(string path)
        {
            var syml = new SYML(Path.Combine(path, "example.syml"));
            syml.GetOrSetDefault("Example", new PetConfiguration
            {
                Badge = "",
                BadgeColor = "",
                EveryoneCanUse = true,
                GodMode = false,
                Health = 150,
                ItemInHand = ItemType.GunLogicer,
                Name = "Example Pet",
                Permissions = new List<string> { "pet.example" },
                Users = new List<string> { "000@steam" },
                PetID = 0,
                Role = RoleType.ClassD,
                MaxAmount = 1,
                Scale = new SerializedVector3(0.3f, 0.3f, 0.3f),
            });
        }
    }
}
