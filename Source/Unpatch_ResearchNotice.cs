using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

//Razor 2.3's code
namespace PawnsChooseResearch
{
    [StaticConstructorOnStartup]
    public class Startup
    {
        static Startup()
        {
            HarmonyUnpatching(); // Since I already did the legwork . . .
            DefDatabase<StatDef>.Add(MakeProjectStatDef()); // Could/Should do this via XML, but this keeps it all in C#
        }

        static private void HarmonyUnpatching()
        {
            var researchDone = typeof(ResearchManager).GetMethod(nameof(ResearchManager.FinishProject));
            var harmPatches = Harmony.GetPatchInfo(researchDone);
            var prefixes = harmPatches?.Prefixes?.Where(p => p.owner == "Fluffy.ResearchTree" || p.owner == "rimworld.ResearchPal");
            if (prefixes != null && prefixes.Any())
            {
                Harmony harmony = new Harmony("razor2.3.PawnsDiscloseResearch");
                foreach (var p in prefixes)
                {
                    Log.Message($"PDR: Belt found suspenders ({p.owner}). Re-enabling vanilla notification . . . ");
                    harmony.Unpatch(researchDone, HarmonyPatchType.Prefix, p.owner);
                }
            }
        }

        static private StatDef MakeProjectStatDef()
        {
            var s = new StatDef();
            s.defName = "ResearchProject";
            s.label = "PCR_StatDef_ResearchProject".Translate();
            s.description = "PCR_StatDef_ResearchProjectDesc".Translate();
            s.category = StatCategoryDefOf.BasicsPawn;
            s.workerClass = typeof(StatWorker_ResearchProject);
            s.toStringStyle = ToStringStyle.PercentZero;
            s.defaultBaseValue = 0;
            return s;
        }
    }
    /*
    public class StatWorker_ResearchProject : StatWorker
    {
        public override bool ShouldShowFor(StatRequest req)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return false;
            return pawn.IsColonist && !pawn.skills.GetSkill(SkillDefOf.Intellectual).TotallyDisabled;
        }

        public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return 0;
            return ResearchRecord.CurrentProject(pawn)?.ProgressPercent ?? 0;
        }

        public override string GetExplanationUnfinalized(StatRequest req, ToStringNumberSense numberSense)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return String.Empty;
            return GenText.ToTitleCaseSmart(ResearchRecord.CurrentProject(pawn)?.label ?? String.Empty);
        }

        public override string GetExplanationFinalizePart(StatRequest req, ToStringNumberSense numberSense, float finalVal)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return string.Empty;
            return ResearchRecord.CurrentProject(pawn)?.description ?? string.Empty;
        }
    }*/
}