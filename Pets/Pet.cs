using Synapse.Api;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pets
{
    [API]
    public class Pet : Dummy
    {
        [API]
        public static bool SpawnPet(Player owner, int id, out Pet pet)
        {
            pet = null;

            var conf = PluginClass.PetPlugin.PetHandler.GetPet(id);
            if (conf == null) return false;

            var script = owner.GetPetOwnerScript();

            if (script.SpawnedPets.Count >= PluginClass.PetPlugin.Config.MaxPets) return false;

            if (conf.MaxAmount >= 0 && script.SpawnedPets.Count(x => x == id) >= conf.MaxAmount) return false;

            pet = new Pet(owner, conf);

            return true;
        }

        public Player Owner { get; }

        public PetConfiguration Configuration { get; }

        public Pet(Player player, int ID) : this(player, PluginClass.PetPlugin.PetHandler.GetPet(ID)) { }

        public Pet(Player player, PetConfiguration configuration) :
            base(player.Position, player.Rotation, configuration.Role, PluginClass.PetPlugin.Translation.ActiveTranslation.Owner.Replace("%player%", player.NickName), configuration.Badge.Replace("\\n", "\n"), configuration.BadgeColor)
        {
            player.GetPetOwnerScript().SpawnedPets.Add(configuration.PetID);
            Owner = player;
            Configuration = configuration;
            Player.GodMode = configuration.GodMode;
            Player.Health = configuration.Health;
            Player.DisplayInfo = configuration.Name.Replace("\\n", "\n");

            if (!PluginClass.PetPlugin.Config.ShowOwnerName)
                Player.RemoveDisplayInfo(PlayerInfoArea.Nickname);

            if (!PluginClass.PetPlugin.Config.ShowRole)
                Player.RemoveDisplayInfo(PlayerInfoArea.Role);

            Player.RemoveDisplayInfo(PlayerInfoArea.PowerStatus);
            Player.RemoveDisplayInfo(PlayerInfoArea.UnitName);

            Scale = configuration.Scale.Parse();
            HeldItem = configuration.ItemInHand;

            Movement = PlayerMovementState.Sprinting;
            Timing.RunCoroutine(Walk());
        }

        private IEnumerator<float> Walk()
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(0.1f);

                if (Owner == null) Destroy();
                if (GameObject == null) yield break;
                RotateToPosition(Owner.Position);

                var distance = Vector3.Distance(Owner.Position, Position);

                if ((PlayerMovementState)Owner.AnimationController.Network_curMoveState == PlayerMovementState.Sneaking) Movement = PlayerMovementState.Sneaking;
                else Movement = PlayerMovementState.Sprinting;

                if (Movement == PlayerMovementState.Sneaking)
                {
                    if (distance > 5f) Position = Owner.Position;

                    else if (distance > 1f) Direction = Synapse.Api.Enum.MovementDirection.Forward;

                    else if (distance <= 1f) Direction = Synapse.Api.Enum.MovementDirection.Stop;

                    continue;
                }

                if (distance > 10f)
                    Position = Owner.Position;

                else if (distance > 2f)
                    Direction = Synapse.Api.Enum.MovementDirection.Forward;

                else if (distance <= 1.25f)
                    Direction = Synapse.Api.Enum.MovementDirection.Stop;

            }
        }
    }
}