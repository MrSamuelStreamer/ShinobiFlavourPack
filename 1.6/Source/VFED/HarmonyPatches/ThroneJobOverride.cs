using HarmonyLib;
using RimWorld;
using Verse.AI;
using Verse;
 
namespace ShinobiFP.VFED.HarmonyPatches;
 
[HarmonyPatch(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.JobTrackerTick))]
public static class Patch_ForceThroneOverrideCheck
{
    public static void Postfix(Pawn_JobTracker __instance, Pawn ___pawn)
    {
        if (___pawn?.mindState?.duty?.def == global::VFED.VFED_DefOf.VFED_SitOnThrone && ___pawn.IsHashIntervalTick(250) && __instance.curJob?.def != JobDefOf.Ingest)
        {
            __instance.CheckForJobOverride();
        }
    }
}