/*
using System;
using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using Verse;
using Verse.AI;
using HarmonyLib;
using VanillaBooksExpanded;

namespace PawnsChooseResearch
{
    [HarmonyPatch]
    public class VBE_Integration
    {
         static bool Prepare(MethodBase original)
         {
         Log.Message("Preparing");
         Log.Message("VBE is loaded = " + (ModLister.GetActiveModWithIdentifier("VanillaExpanded.VBooksE") != null).ToString());
         return ModLister.GetActiveModWithIdentifier("VanillaExpanded.VBooksE") != null;
         }

         static IEnumerable<MethodBase> TargetMethods()
         {
            yield return AccessTools.Method(Verse.GenTypes.GetTypeInAnyAssembly("TechBlueprint", null), "GetFloatMenuOptions");
            yield return AccessTools.Method(Verse.GenTypes.GetTypeInAnyAssembly("TechBlueprint", null), "UnlockResearch");
         }

         [HarmonyPrefix]
         [HarmonyPatch(typeof(TechBlueprint), "GetFloatMenuOptions")]
         public static bool Prefix_FloatMenu(ref IEnumerable<FloatMenuOption> __result, Pawn myPawn, TechBlueprint __instance)
         {
             if (!ModSettings_PawnsChooseResearch.restoreControl && myPawn.CanReach(__instance, PathEndMode.InteractionCell, Danger.Deadly, false, false, TraverseMode.ByPawn))
             {
                 Action action = delegate ()
                 {
                     Job job = JobMaker.MakeJob(VBE_DefOf.VBE_ReadBook, null, __instance);
                     job.count = 1;
                     myPawn.jobs.TryTakeOrderedJob(job, new JobTag?(JobTag.Misc), false);
                 };
                 __result = NewMenu(myPawn, action, __instance.researchProject, __instance);
                 return false;
             }
             return true;
         }
  
        [HarmonyPrefix]
        [HarmonyPatch(typeof(TechBlueprint), "UnlockResearch")]
        public static bool Prefix_UnlockResearch(Pawn pawn, TechBlueprint __instance)
        {
            if (ModSettings_PawnsChooseResearch.restoreControl)
            {
                return true;
            }

            if (__instance.researchProject.CanStartNow)
            {
                if (ModSettings_PawnsChooseResearch.groupResearch)
                {
                    ResearchRecord.groupProject = __instance.researchProject;
                }
                else
                {
                    ResearchRecord.SetResearchPlan(pawn, __instance.researchProject);
                }
            }
            else if (__instance.researchProject.prerequisites != null && !__instance.researchProject.PrerequisitesCompleted)
            {
                ResearchProjectDef underlyingTheory;
                List<ResearchProjectDef> posProjects = __instance.researchProject.prerequisites;
                int i = posProjects.Count;
                while (i > 0)
                {
                    int j = Rand.Range(0, posProjects.Count - 1);
                    underlyingTheory = posProjects[j];
                    if (underlyingTheory.CanStartNow)
                    {
                        if (!ModSettings_PawnsChooseResearch.groupResearch)
                        {
                            ResearchRecord.SetResearchPlan(pawn, underlyingTheory);
                        }
                        else
                        {
                            ResearchRecord.groupProject = underlyingTheory;
                        }
                        break;
                    }
                    posProjects.RemoveAt(j);
                    i--;
                }
            }
            else if (ModSettings_PawnsChooseResearch.groupResearch)
            {
                SetResearch.SetRandomGroupResearch();
            }
            else
            {
                SetResearch.SetRandomResearch(pawn);
            }
            GainKnowledge(pawn, __instance.Props.readingTicks);
            __instance.used = true;
            return false;
        }
        
        public static IEnumerable<FloatMenuOption> NewMenu(Pawn myPawn, Action action, ResearchProjectDef project, LocalTargetInfo techPrint)
        {
            string label = "VBE.CantReadBlueprintTooAdvanced".Translate();
            if (myPawn.skills.GetSkill(SkillDefOf.Intellectual).TotallyDisabled)
            {
                label = myPawn.LabelShort + " " + "PCR.VBE.Disabled".Translate();
                yield return new FloatMenuOption(label, null, MenuOptionPriority.Default, null, null, 0f, null, null, true, 0);
                yield break;
            }
            else if (project.CanStartNow)
            {
                label = "VBE.ReadBlueprint".Translate() + ": " + project.label;
                yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption(label, action, MenuOptionPriority.Default, null, null, 0f, null, null, true, 0), myPawn, techPrint, "ReservedBy");
                yield break;
            }
            else if (project.prerequisites != null && !project.PrerequisitesCompleted)
            {
                //Log.Message("Check for underlying theory");
                for (int i = 0; i < project.prerequisites.Count; i++)
                {
                    ResearchProjectDef underlyingTheory = project.prerequisites[i];
                    if (underlyingTheory.CanStartNow)
                    {
                        label = "PCR.VBE.Underlying".Translate().CapitalizeFirst() + " " + project.label;
                        yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption(label, action, MenuOptionPriority.Default, null, null, 0f, null, null, true, 0), myPawn, techPrint, "ReservedBy");
                        yield break;
                    }
                }
            }
            else if (project.IsFinished)
            {
                label = "PCR.VBE.Idea".Translate().CapitalizeFirst();
                yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption(label, action, MenuOptionPriority.Default, null, null, 0f, null, null, true, 0), myPawn, techPrint, "ReservedBy");
                yield break;
            }
            yield return new FloatMenuOption(label, null, MenuOptionPriority.Default, null, null, 0f, null, null, true, 0);
            yield break;
        }
        
        private static void GainKnowledge(Pawn pawn, float num)
        {
            ResearchProjectDef curProject = ResearchRecord.CurrentProject(pawn);
            Find.ResearchManager.currentProj = curProject;
            num *= pawn.GetStatValue(StatDefOf.ResearchSpeed, true) * .6f;
            num *= (int)Faction.OfPlayer.def.techLevel / (int)curProject.techLevel;
            Find.ResearchManager.ResearchPerformed(num, pawn);
        }
    }
}
*/