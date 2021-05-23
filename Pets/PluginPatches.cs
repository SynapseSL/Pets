using System;
using System.Linq;
using Synapse;
using Synapse.Api;
using HarmonyLib;
using Respawning.NamingRules;
using System.Text;
using System.Collections.Generic;

namespace Pets
{
    [HarmonyPatch(typeof(NineTailedFoxNamingRule),nameof(NineTailedFoxNamingRule.PlayEntranceAnnouncement))]
    internal static class AnnouncePatch
    {
        private static bool Prefix(NineTailedFoxNamingRule __instance, string regular)
        {
            try
            {
				string cassieUnitName = __instance.GetCassieUnitName(regular);
				int num = Server.Get.GetPlayers(x => x.RealTeam == Team.SCP && !x.IsDummy).Count;
				StringBuilder stringBuilder = new StringBuilder();
				if (global::ClutterSpawner.IsHolidayActive(global::Holidays.Christmas))
				{
					stringBuilder.Append("XMAS_EPSILON11 ");
					stringBuilder.Append(cassieUnitName);
					stringBuilder.Append("XMAS_HASENTERED ");
					stringBuilder.Append(num);
					stringBuilder.Append(" XMAS_SCPSUBJECTS");
				}
				else
				{
					stringBuilder.Append("MTFUNIT EPSILON 11 DESIGNATED ");
					stringBuilder.Append(cassieUnitName);
					stringBuilder.Append(" HASENTERED ALLREMAINING ");
					if (num == 0)
					{
						stringBuilder.Append("NOSCPSLEFT");
					}
					else
					{
						stringBuilder.Append("AWAITINGRECONTAINMENT ");
						stringBuilder.Append(num);
						if (num == 1)
						{
							stringBuilder.Append(" SCPSUBJECT");
						}
						else
						{
							stringBuilder.Append(" SCPSUBJECTS");
						}
					}
				}
				__instance.ConfirmAnnouncement(ref stringBuilder);
				return false;
            }
            catch(Exception e)
            {
                Logger.Get.Error($"Error while announcing mtf:\n{e}");
                return true;
            }
        }
    }

	[HarmonyPatch(typeof(NineTailedFoxAnnouncer),nameof(NineTailedFoxAnnouncer.AnnounceScpTermination))]
	internal static class TerminationPatch
    {
		public static List<Player> Killer = new List<Player>();

		private static bool Prefix(PlayerStats.HitInfo hit)
        {
			var ply = Killer.FirstOrDefault(x => x.PlayerId == hit.PlayerId);
			if(ply != null)
            {
				Killer.Remove(ply);
				return false;
            }

			return true;
		}
	}
}
