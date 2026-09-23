using System;
using Verse;
using HarmonyLib;

namespace ShinobiFP.VFED;

public class ShinobiFPVFEDMod : Mod
{
    public ShinobiFPVFEDMod(ModContentPack content) : base(content)
    {
        try
        {
            var harmony = new Harmony("ShinobiFP.VFED");
            harmony.PatchAll();
        }
        catch (Exception e)
        {
            Log.Error("[Shinobi Flavour Pack] Failed to apply VFED compatibility patches: " + e);
        }
    }
}