using RimWorld;
using UnityEngine;
using Verse;

namespace ShinobiFlavourPack.EES;

/// <summary>
/// Sight capacity for EES cloak detection and stronghold scouting.
/// Blindsight Unified does not restore Sight; BU_TrueSight severity maps to
/// advertised virtual sight (0.1 per psylink, cap 0.6 → 25%–150%).
/// </summary>
public static class EffectiveSense
{
    public const float TrueSightToSense = 2.5f;

    public static float Of(Pawn pawn)
    {
        if (pawn?.health?.capacities == null)
        {
            return 0f;
        }

        float sight = pawn.health.capacities.GetLevel(PawnCapacityDefOf.Sight);

        HediffDef trueSightDef = DefDatabase<HediffDef>.GetNamedSilentFail("BU_TrueSight");
        if (trueSightDef != null)
        {
            Hediff trueSight = pawn.health.hediffSet?.GetFirstHediffOfDef(trueSightDef);
            if (trueSight != null)
            {
                return Mathf.Max(sight, trueSight.Severity * TrueSightToSense);
            }
        }

        HediffDef attunementDef = DefDatabase<HediffDef>.GetNamedSilentFail("BU_BlindAttunement");
        if (attunementDef != null && pawn.health.hediffSet != null && pawn.health.hediffSet.HasHediff(attunementDef))
        {
            return Mathf.Max(sight, pawn.health.capacities.GetLevel(PawnCapacityDefOf.Hearing));
        }

        return sight;
    }
}
