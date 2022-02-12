using HarmonyLib;

namespace Pets
{
    [HarmonyPatch(typeof(NineTailedFoxAnnouncer), nameof(NineTailedFoxAnnouncer.AnnounceScpTermination))]
	internal static class TerminationPatch
	{
		[HarmonyPrefix]
		private static bool Prefix(ReferenceHub scp)
		{
			var pet = scp.GetPlayer();
			if (pet.IsDummy) return false;
			return true;
		}
	}
}
