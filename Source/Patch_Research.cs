using System.Linq;
using RimWorld;
using Verse;
using HarmonyLib;

namespace PawnsChooseResearch
{
    [HarmonyPatch]
    public class Patch_Research
    {
        [HarmonyPatch(typeof(ResearchManager), "ResearchPerformed")]
        [HarmonyPrefix]
        public static void Prefix_ResearchPerformed(Pawn researcher)
        {
            //Harmony.DEBUG = true;
            if (!ModSettings_PawnsChooseResearch.restoreControl && researcher?.CurJobDef == JobDefOf.Research)
            {
                //Log.Message("Null check");
                ResearchProjectDef myProject = ResearchRecord.CurrentProject(researcher);
                if (myProject == null || myProject.IsFinished)
                {
                    if (ModSettings_PawnsChooseResearch.groupResearch)
                    {
                        SetResearch.SetRandomGroupResearch();
                    }
                    else
                    {
                        SetResearch.SetRandomResearch(researcher);
                        //Log.Message(researcher.Name.ToString() + " had to pick new research: " + ResearchRecord.CurrentProject(researcher).defName.ToString());
                    }
                }
                Find.ResearchManager.currentProj = ResearchRecord.CurrentProject(researcher);
            }
        }

        [HarmonyPatch(typeof(Alert_NeedResearchProject), "GetReport")]
        [HarmonyPrefix]
        public static void Prefix_SetResearch ()
        {
            if (Find.ResearchManager.currentProj == null)
            {
                if (ModSettings_PawnsChooseResearch.groupResearch)
                {
                    SetResearch.SetRandomGroupResearch();
                }
                else if (DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where((ResearchProjectDef x) => x.CanStartNow).TryRandomElement(out ResearchProjectDef research))
                {
                    Find.ResearchManager.currentProj = research;
                }
            }
        }

    }
}
