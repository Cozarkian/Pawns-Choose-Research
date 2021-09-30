using System;
using RimWorld;
using Verse;

namespace PawnsChooseResearch
{
    //Razor 2.3 Code
    public class StatWorker_ResearchProject : StatWorker
    {
        public override bool ShouldShowFor(StatRequest req)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return false;
            return ResearchRecord.CurrentProject(pawn, false) != null && !pawn.skills.GetSkill(SkillDefOf.Intellectual).TotallyDisabled;
        }

        public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return 0;
            return ResearchRecord.CurrentProject(pawn, false)?.ProgressPercent ?? 0;
        }

        public override string GetExplanationUnfinalized(StatRequest req, ToStringNumberSense numberSense)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return String.Empty;
            return GenText.ToTitleCaseSmart(ResearchRecord.CurrentProject(pawn, false)?.label ?? String.Empty);
        }

        public override string GetExplanationFinalizePart(StatRequest req, ToStringNumberSense numberSense, float finalVal)
        {
            var pawn = req.Thing as Pawn;
            if (pawn == null)
                return string.Empty;
            return ResearchRecord.CurrentProject(pawn)?.description ?? "PCR_StatDef_ResearchProjectNoProject".Translate();
        }
    }
}
