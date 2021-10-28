using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace PawnsChooseResearch
{
    public class SetResearch
    {
        public static void SetRandomResearch(Pawn pawn)
        {
            List<ResearchProjectDef> possibleProjects = new List<ResearchProjectDef>();
            for (int i = 3; i >= 0; i--)
            {
                if (DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where((ResearchProjectDef x) => x.CanStartNow).TryRandomElement(out ResearchProjectDef research))
                {
                    if (!possibleProjects.Contains(research))
                    {
                        if ((i < 1 && possibleProjects.Count == 0) ||
                           (!ResearchCapability.IsIncapable(pawn, research) && !ResearchCapability.IsAbhorrent(pawn, research)))
                        {
                            possibleProjects.Add(research);
                        }
                    }
                }
            }
            //Log.Message(pawn.Name.ToString() + " chose " + possibleProjects.Count + " projects");
            if (possibleProjects.Count == 1)
            {
                //Log.Message("The only possible project is " + possibleProjects[0].label);
                ResearchRecord.SetResearchPlan(pawn, possibleProjects[0]);
                return;
            }

            ResearchProjectDef myProject = new ResearchProjectDef();
            float myProjectScore = -100;
            float projectScore = 0;
            foreach (ResearchProjectDef project in possibleProjects)
            {
                projectScore = ResearchPreferences.GetPreferenceScore(pawn, project);//Get Score of Project
                if (projectScore > myProjectScore || myProject == null)
                {
                    myProject = project;
                    myProjectScore = projectScore;
                }
            }
            Log.Message("Choosing " + myProject.label);
            ResearchRecord.SetResearchPlan(pawn, myProject);
        }

        public static void SetRandomGroupResearch()
        {
            List<ResearchProjectDef> possibleProjects = new List<ResearchProjectDef>();
            List<Pawn> researchers = new List<Pawn>();
            foreach (Map map in Find.Maps)
            {
                if (map.IsPlayerHome)
                {
                    foreach (Pawn pawn in map.mapPawns.FreeColonistsSpawned)
                    {
                        if (pawn.workSettings.WorkIsActive(WorkTypeDefOf.Research))
                        {
                            researchers.Add(pawn);
                        }
                    }
                }
            }
            if (researchers.Count == 0)
            {
                if (DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where((ResearchProjectDef x) => x.CanStartNow).TryRandomElement(out ResearchProjectDef research))
                {
                    Find.ResearchManager.currentProj = research;
                    return;
                }
            }
            int numProjects = 1 + researchers.Count;
            for (int i = 0; i < numProjects; i++)
            {
                if (DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where((ResearchProjectDef x) => x.CanStartNow).TryRandomElement(out ResearchProjectDef research))
                {
                    if (!possibleProjects.Contains(research))
                    {
                        possibleProjects.Add(research);
                    }
                }
            }
            ResearchProjectDef myProject = new ResearchProjectDef();
            float myProjectScore = -100;
            float projectScore = 0;
            foreach (ResearchProjectDef project in possibleProjects)
            {
                foreach(Pawn researcher in researchers)
                {
                    if (ResearchCapability.IsAbhorrent(researcher, project))
                    {
                        projectScore -= 3;
                    }
                    else if (ResearchCapability.IsIncapable(researcher, project))
                    {
                        projectScore -= 1;
                    }
                    else
                    {
                        projectScore += ResearchPreferences.GetPreferenceScore(researcher, project);
                    }
                }
                if (projectScore > myProjectScore || myProject == null)
                {
                    myProject = project;
                    myProjectScore = projectScore;
                }
            }
            Find.ResearchManager.currentProj = myProject;
            ResearchRecord.groupProject = myProject;
        }
    }
}
