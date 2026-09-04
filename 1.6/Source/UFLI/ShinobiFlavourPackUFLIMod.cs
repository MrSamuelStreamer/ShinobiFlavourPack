using System;
using HarmonyLib;
using Verse;

namespace ShinobiFlavourPack.UFLI;

/// <summary>
/// Compatibility layer for UF Light Industries Furniture (packageId KindSeal.UFLI).
/// Loaded only when that mod is active via loadFolders.xml.
/// </summary>
public class ShinobiFlavourPackUFLIMod : Mod
{
    public ShinobiFlavourPackUFLIMod(ModContentPack content)
        : base(content)
    {
        try
        {
            Harmony harmony = new Harmony("MrSamuelStreamer.rimworld.ShinobiFlavourPack.UFLI.main");
            harmony.PatchAll();
        }
        catch (Exception e)
        {
            Log.Error("[Shinobi Flavour Pack] Failed to apply UFLI compatibility patches: " + e);
        }
    }
}
