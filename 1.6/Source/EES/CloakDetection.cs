using RimWorld;
using UnityEngine;
using Verse;

namespace ShinobiFlavourPack.EES;

/// <summary>
/// Cloak reveal radius matching EES, using virtual sense for blindsight.
/// </summary>
public static class CloakDetection
{
    public const float BaseVisibleRadius = 14f;

    public static float Radius(Pawn detector, Pawn scout)
    {
        float radius = BaseVisibleRadius;
        float sightCapacity = detector?.health?.capacities != null
            ? detector.health.capacities.GetLevel(PawnCapacityDefOf.Sight)
            : 0f;

        Map map = scout?.Map ?? detector?.Map;
        if (sightCapacity > 0.001f && detector != null && (detector.genes == null || detector.genes.AffectedByDarkness) && scout != null && map != null)
        {
            float glow = map.glowGrid.GroundGlowAt(scout.Position, false, false);
            radius *= Mathf.Lerp(0.33f, 1f, glow);
        }

        return radius * EffectiveSense.Of(detector);
    }
}
