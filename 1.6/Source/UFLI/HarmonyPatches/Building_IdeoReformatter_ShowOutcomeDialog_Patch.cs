using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ShinobiFlavourPack.UFLI.HarmonyPatches;

/// <summary>
/// Skips the colonist/slave/unchanged dialog after ideology conversion.
/// ApplyIdeoWash still runs from CompleteReformatting (ideo + Recruitable).
/// Bound by string because ShowOutcomeDialog is private.
/// </summary>
[HarmonyPatch]
public static class Building_IdeoReformatter_ShowOutcomeDialog_Patch
{
    private const string TargetName = "UFLIFB.Building_IdeoReformatter:ShowOutcomeDialog";

    public static MethodBase TargetMethod() => AccessTools.Method(TargetName);

    public static bool Prepare()
    {
        if (TargetMethod() != null)
        {
            return true;
        }

        Log.Error("[Shinobi Flavour Pack] Could not resolve " + TargetName + "; brainwasher status dialog will not be skipped.");
        return false;
    }

    [HarmonyPrefix]
    public static bool Prefix(Pawn __0)
    {
        Pawn pawn = __0;
        if (pawn != null && !pawn.Destroyed)
        {
            Messages.Message(
                "MSS_SFP_IdeoReformatterComplete".Translate(pawn.Named("PAWN")),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                true
            );
        }

        return false;
    }
}
