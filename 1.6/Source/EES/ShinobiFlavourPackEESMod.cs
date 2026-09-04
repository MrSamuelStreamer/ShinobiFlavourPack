using System;
using HarmonyLib;
using Verse;

namespace ShinobiFlavourPack.EES;

/// <summary>
/// Compatibility layer for Evolving Enemy Strongholds
/// (packageId wowgag.EvolvingEnemyStrongholds). Loaded only when that mod is
/// active via loadFolders.xml.
/// </summary>
public class ShinobiFlavourPackEESMod : Mod
{
    public ShinobiFlavourPackEESMod(ModContentPack content)
        : base(content)
    {
        try
        {
            Harmony harmony = new Harmony("MrSamuelStreamer.rimworld.ShinobiFlavourPack.EES.main");
            harmony.PatchAll();
        }
        catch (Exception e)
        {
            Log.Error("[Shinobi Flavour Pack] Failed to apply EES compatibility patches: " + e);
        }
    }
}
