using System.Reflection;
using HarmonyLib;
using Verse;

namespace ShinobiFlavourPack.EES.HarmonyPatches;

/// <summary>
/// Cloak reveal radius uses EffectiveSense instead of raw Sight, so blindsight
/// pawns can detect cloaked counter-scouts. Bound by string because SightRadius is private.
/// </summary>
[HarmonyPatch]
public static class MapComponent_EESCounterScout_SightRadius_Patch
{
    private const string TypeName = "EvolvingEnemyStrongholds.MapComponent_EESCounterScout";

    public static MethodBase TargetMethod()
    {
        return AccessTools.Method(TypeName + ":SightRadius");
    }

    public static bool Prepare()
    {
        if (TargetMethod() != null)
        {
            return true;
        }

        Log.Error("[Shinobi Flavour Pack] Could not resolve " + TypeName + ":SightRadius; cloaked scouts will still ignore blindsight.");
        return false;
    }

    [HarmonyPrefix]
    public static bool Prefix(Pawn detector, Pawn scout, ref float __result)
    {
        __result = CloakDetection.Radius(detector, scout);
        return false;
    }
}
