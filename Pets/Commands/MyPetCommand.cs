using Synapse.Command;
using Synapse;
using Synapse.Api;
using System.Linq;

namespace Pets.Commands
{
    [CommandInformation(
        Name = "Pet",
        Aliases = new[] { "MyPet", "MP" },
        Description = "A Command for managing Pets",
        Permission = "pet.me",
        Platforms = new[] { Platform.ClientConsole },
        Usage = ".Pet / .pet spawn ID / .pet player"
        )]
    public class MyPetCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            if (context.Arguments.Count == 0) return new CommandResult
            {
                Message = PluginClass.PetPlugin.Translation.ActiveTranslation.CommandText.Replace("\\n", "\n").Replace("%petlist%", GetPetList(context.Player)),
                State = CommandResultState.Ok,
            };

            if(context.Arguments.At(0).ToLower() == "spawn")
            {
                if (context.Arguments.Count < 2) return new CommandResult
                {
                    Message = PluginClass.PetPlugin.Translation.ActiveTranslation.MissingID,
                    State = CommandResultState.Error,
                };

                if (!int.TryParse(context.Arguments.At(1), out var result)) return new CommandResult
                {
                    Message = PluginClass.PetPlugin.Translation.ActiveTranslation.InvalidID,
                    State = CommandResultState.Error
                };

                if (!context.Player.CanUsePet(result)) return new CommandResult
                {
                    Message = PluginClass.PetPlugin.Translation.ActiveTranslation.Denied,
                    State = CommandResultState.NoPermission,
                };


                if (Pet.SpawnPet(context.Player, result, out var pet))
                    return new CommandResult
                    {
                        Message = PluginClass.PetPlugin.Translation.ActiveTranslation.Spawned,
                        State = CommandResultState.Ok
                    };

                return new CommandResult
                {
                    Message = PluginClass.PetPlugin.Translation.ActiveTranslation.Limit,
                    State = CommandResultState.Error
                };
            }

            var ply = Server.Get.GetPlayer(context.Arguments.At(0));
            if (ply == null) return new CommandResult
            {
                Message = PluginClass.PetPlugin.Translation.ActiveTranslation.NoFound,
                State = CommandResultState.Error
            };

            return new CommandResult
            {
                Message = PluginClass.PetPlugin.Translation.ActiveTranslation.OtherPet.Replace("\\n", "\n").Replace("%petlist%", GetPetList(ply)),
                State = CommandResultState.Ok
            };
        }

        public string GetPetList(Player player)
        {
            var pets = player.GetAvailablePets().OrderByDescending(x => x.PetID);

            var msg = "";
            foreach (var pet in pets)
                msg += $"\n  - {pet.PetID} : {pet.Name}";

            return msg;
        }
    }
}
