using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace ShinobiFlavourPack.EES.HarmonyPatches;

/// <summary>
/// Stronghold scout chance uses EffectiveSense instead of raw Sight.
/// Patching the List overload also covers PartyCanSee and RollChanceOf.
/// Bound by string because BestSight is private.
/// </summary>
[HarmonyPatch]
public static class ScoutUtility_BestSight_Patch
{
    private const string TypeName = "EvolvingEnemyStrongholds.ScoutUtility";

    public static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.TypeByName(TypeName), "BestSight", new[] { typeof(List<Pawn>) });
    }

    public static bool Prepare()
    {
        if (TargetMethod() != null)
        {
            return true;
        }

        Log.Error("[Shinobi Flavour Pack] Could not resolve " + TypeName + ".BestSight(List<Pawn>); stronghold scouting will still require Sight.");
        return false;
    }

    [HarmonyPostfix]
    public static void Postfix(List<Pawn> pawns, ref float __result)
    {
        if (pawns == null)
        {
            return;
        }

        float best = 0f;
        bool anyHumanlike = false;
        for (int i = 0; i < pawns.Count; i++)
        {
            Pawn pawn = pawns[i];
            if (pawn == null || pawn.Dead || pawn.health == null || pawn.RaceProps == null || !pawn.RaceProps.Humanlike)
            {
                continue;
            }

            anyHumanlike = true;
            float sense = EffectiveSense.Of(pawn);
            if (sense > best)
            {
                best = sense;
            }
        }

        if (anyHumanlike)
        {
            __result = best;
        }
    }
}
