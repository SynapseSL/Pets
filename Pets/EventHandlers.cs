using Synapse.Api;
using System.Linq;
using ev = Synapse.Api.Events.EventHandler;

namespace Pets
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            ev.Get.Player.LoadComponentsEvent += LoadComponents;
            ev.Get.Server.TransmitPlayerDataEvent += TransmitData;
            ev.Get.Player.PlayerDamagePermissionEvent += DamagePermission;
        }

        private void DamagePermission(Synapse.Api.Events.SynapseEventArguments.PlayerDamagePermissionEventArgs ev)
        {
            if (ev.Victim.IsDummy)
            {
                var dummy = Map.Get.Dummies.FirstOrDefault(x => x.Player == ev.Victim);

                if (dummy != null && dummy is Pet p)
                    ev.AllowDamage = SynapseExtensions.GetHarmPermission(ev.Attacker, p.Owner);
            }
        }

        private void TransmitData(Synapse.Api.Events.SynapseEventArguments.TransmitPlayerDataEventArgs ev)
        {
            if (ev.PlayerToShow.IsDummy)
            {
                var dummy = Map.Get.Dummies.FirstOrDefault(x => x.Player == ev.PlayerToShow);

                if (dummy != null && dummy is Pet p && p.Owner != ev.Player)
                {
                    if(p.Owner.RoleType == RoleType.Spectator)
                    {
                        ev.Invisible = true;
                        return;
                    }

                    if(p.Owner != ev.Player)
                    {
                        if (PluginClass.PetPlugin.Config.InvisiblePet || (p.Owner.Invisible && !ev.Player.HasPermission("synapse.see.invisible")) || p.Owner.PlayerEffectsController.GetEffect<CustomPlayerEffects.Invisible>().IsEnabled)
                        {
                            ev.Invisible = true;
                            return;
                        }

                        if (ev.Player.RoleType == RoleType.Scp93953 || ev.Player.RoleType == RoleType.Scp93989)
                        {
                            if (SynapseExtensions.CanHarmScp(p.Owner, false) && !p.Owner.GetComponent<Scp939_VisionController>().CanSee(ev.Player.PlayerEffectsController.GetEffect<CustomPlayerEffects.Visuals939>()))
                                ev.Invisible = true;
                        }
                    }
                }
            }
        }

        private void LoadComponents(Synapse.Api.Events.SynapseEventArguments.LoadComponentEventArgs ev)
        {
            if (ev.Player.GetComponent<PetOwnerScript>() == null)
                ev.Player.AddComponent<PetOwnerScript>();
        }
    }
}
