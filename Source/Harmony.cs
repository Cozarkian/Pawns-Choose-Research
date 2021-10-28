using System.Reflection;
using Verse;
using HarmonyLib;

namespace PawnsChooseResearch
{
    //Runs Harmony Patcher
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("com.rimworld.pawnschooseresearch");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            //Log.Message("Research Patched");
            //Harmony.DEBUG = true;
        }
    }
}
