using System.Reflection;
using Verse;
using HarmonyLib;

namespace PawnsChooseResearch
{
    [HarmonyPatch]
    class Patch_TechnologyBlueprint
    {      
        static bool Prepare(MethodInfo original)
        {
            //Log.Message("Preparing");
            return ModLister.GetActiveModWithIdentifier("GwinnBleidd.ResearchTweaks") != null;
        }
        
        static MethodBase TargetMethod()
        {
            MethodBase method = AccessTools.Method(Verse.GenTypes.GetTypeInAnyAssembly("TBnRE.Utility", null), "BoostResearch");
            //Log.Message("Found method");
            if (method == null) Log.Message("But it is null");
            return method;
        }

        static void Postfix(ref ResearchProjectDef chosenResearchProject, Pawn usedBy)
        {
            //Log.Message("Patching Research Boost");
            if (!ModSettings_PawnsChooseResearch.groupResearch && !ResearchCapability.IsIncapable(usedBy, chosenResearchProject) && !ResearchCapability.IsAbhorrent(usedBy, chosenResearchProject))
            {
                ResearchRecord.SetResearchPlan(usedBy, chosenResearchProject);
            }
        }
        /*
        static Exception Cleanup()
        {
            return null;
        }
        */
    }
}
