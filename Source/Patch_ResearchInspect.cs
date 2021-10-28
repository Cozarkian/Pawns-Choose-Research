using Verse;
using Verse.AI;
using HarmonyLib;

namespace PawnsChooseResearch
{
    [HarmonyPatch]
    public class Patch_ResearchInspect
    {
        [HarmonyPatch(typeof(JobDriver), "GetReport")]
        [HarmonyPostfix]
        public static void Postfix(ref string __result, Pawn ___pawn)
        {
            if (__result == RimWorld.JobDefOf.Research.reportString)
            {
                ResearchProjectDef currentProj = Find.ResearchManager.currentProj;
                if (!ModSettings_PawnsChooseResearch.restoreControl)
                {
                    currentProj = ResearchRecord.CurrentProject(___pawn);
                }
                if (currentProj != null && !currentProj.IsFinished)
                {
                    __result = "PCR_ReportString".Translate(currentProj.label) + " " + currentProj.ProgressPercent.ToStringPercent();
                }
            }
        }
    }
}
