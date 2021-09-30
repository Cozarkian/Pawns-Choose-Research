using System.Collections.Generic;
using RimWorld;
using Verse;


namespace PawnsChooseResearch
{
    public class ResearchCapability
    {
        public static bool IsIncapable(Pawn pawn, ResearchProjectDef researchProject)
        {
            if (researchProject.HasModExtension<ResearchCategory>() && ModSettings_PawnsChooseResearch.mustHaveSkill)
            {
                //Log.Message("Checking for skill incapabilities");
                //Shooting
                if (researchProject.GetModExtension<ResearchCategory>().rangedTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Shooting).TotallyDisabled)
                    {
                        return true;
                    }
                }

                //Melee
                if (researchProject.GetModExtension<ResearchCategory>().meleeTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Melee).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Construction
                if (researchProject.GetModExtension<ResearchCategory>().constructionTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Construction).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Mining
                if (researchProject.GetModExtension<ResearchCategory>().miningTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Mining).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Cooking
                if (researchProject.GetModExtension<ResearchCategory>().cookingTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Cooking).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Growing
                if (researchProject.GetModExtension<ResearchCategory>().plantTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Plants).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Animals
                if (researchProject.GetModExtension<ResearchCategory>().animalTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Animals).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Crafting
                if (researchProject.GetModExtension<ResearchCategory>().craftTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Crafting).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Artistic
                if (researchProject.GetModExtension<ResearchCategory>().artTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Artistic).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Medical
                if (researchProject.GetModExtension<ResearchCategory>().medTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Medicine).TotallyDisabled)
                    {
                        return true;
                    }
                }
                //Social
                if (researchProject.GetModExtension<ResearchCategory>().socialTech > 0)
                {
                    if (pawn.skills.GetSkill(SkillDefOf.Social).TotallyDisabled)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsAbhorrent(Pawn pawn, ResearchProjectDef researchProject)
        {
            if ((researchProject.HasModExtension<ResearchCategory>() && researchProject.GetModExtension<ResearchCategory>().neverTech > 0) || researchProject.baseCost > 90000)
            {
                //Log.Message(researchProject.label + " is abhorred by all");
                return true;
            }

            List<Trait> traits = pawn.story.traits.allTraits;
            for (int i = 0; i < traits.Count; i++)
            {
                if (traits[i].def == TraitDefOf.Pyromaniac)
                {
                    if (researchProject.defName == "Firefoam" || researchProject.prerequisites.Contains(ResearchProjectDef.Named("Firefoam")))
                    {
                        return true;
                    }
                }
                if (traits[i].def == TraitDefOf.BodyPurist)
                {
                    if (researchProject.GetModExtension<ResearchCategory>().cyberTech > 0)
                    {
                        return true;
                    }
                    foreach (ResearchProjectDef prerequisite in researchProject.prerequisites)
                    {
                        if (prerequisite.GetModExtension<ResearchCategory>().cyberTech > 0) return true;
                    }
                }
                if (traits[i].def == TraitDefOf.DrugDesire && traits[i].Degree < 0)
                {
                    if (researchProject.defName == "PsychoidBrewing" ||
                        researchProject.defName == "DrugProduction" ||
                        researchProject.defName == "PsychiteRefining" ||
                        researchProject.defName == "WakeUpProduction" ||
                        researchProject.defName == "GoJuiceProduction")
                    {
                        return true;
                    }
                }
                if (traits[i].def == TraitDefOf.Ascetic)
                {
                    if (researchProject.defName == "TubeTelevision" ||
                        researchProject.defName == "FlatscreenTelevision")
                    {
                        return true;
                    }
                }
                if (ModSettings_PawnsChooseResearch.vanillaTraitsActivated)
                {
                    if (traits[i].def == TraitDef.Named("VTE_RefinedPalate") ||
                        traits[i].def == TraitDef.Named("VTE_Gastronomist"))
                    {
                        if (researchProject.defName == "NutrientPaste" ||
                            researchProject.defName == "PackagedSurvivalMeal")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}




