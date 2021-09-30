using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;


namespace PawnsChooseResearch
{
    public class ResearchRecord : GameComponent
    {
        public static List<ResearchProjectDef> currentProjects = new List<ResearchProjectDef>();
        private static List<Pawn> trackedPawns = new List<Pawn>();
        private static Dictionary<Pawn, ResearchProjectDef> researchPlan;
        public static ResearchProjectDef groupProject;

        public ResearchRecord(Game game)
        {
            researchPlan = new Dictionary<Pawn, ResearchProjectDef>();
        }

        public static ResearchProjectDef CurrentProject(Pawn trackedPawn, bool showGroupProj = true)
        {
            if (showGroupProj && ModSettings_PawnsChooseResearch.groupResearch)
            {
                return groupProject;
            }
            if (researchPlan.TryGetValue(trackedPawn, out ResearchProjectDef currentProject))
            {
                //Log.Message("Current project for " + trackedPawn.Name.ToString() + " is " + currentProject.defName.ToString());
                if (currentProject != null && !currentProject.IsFinished)
                {
                    return researchPlan[trackedPawn];
                }
            }
            return null;
        }

        public static void SetResearchPlan(Pawn trackedPawn, ResearchProjectDef myProject)
        {
            if (researchPlan.ContainsKey(trackedPawn))
            {
                researchPlan[trackedPawn] = myProject;
                //Log.Message(trackedPawn.Name + " has started researching " + myProject.defName);
            }
            else 
            {
                researchPlan.Add(trackedPawn, myProject);
            }
        }
        //Stop Tracking for Dead Pawns
        public static void UpdateResearchRecord()
        {
            if (!ModSettings_PawnsChooseResearch.groupResearch && !ModSettings_PawnsChooseResearch.restoreControl)
            {
                foreach (Pawn pawn in researchPlan.Keys)
                {
                    if (pawn.Dead || pawn.Destroyed)
                    {
                        researchPlan.Remove(pawn);
                    }
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref researchPlan, "VarietyRecord", LookMode.Reference, LookMode.Def, ref trackedPawns, ref currentProjects);
            Scribe_Defs.Look(ref groupProject, "GroupProject");

        }
        public override void FinalizeInit()
        {
            if (researchPlan != null && researchPlan.Count > 0)
            {
                UpdateResearchRecord();
                foreach (Pawn key in researchPlan.Keys)
                {
                    Log.Message(key.Name + " is researching " + CurrentProject(key));
                }
            }
            ModSettings_PawnsChooseResearch.CheckMods();
            //Log.Message("Interests Activated is " + InterestsActivated);
            //ResearchCategory.CategoryCheck();
            base.FinalizeInit();
        }
    }
}