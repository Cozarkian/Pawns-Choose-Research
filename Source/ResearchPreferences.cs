using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PawnsChooseResearch
{
    public class ResearchPreferences
    {
        public static float GetPreferenceScore(Pawn pawn, ResearchProjectDef researchProject)
        {
            //Log.Message("Getting score for " + researchProject.label + ", base score is " + researchProject.ProgressPercent);
            float score = researchProject.ProgressPercent;
            if (researchProject.HasModExtension<ResearchCategory>())
            {
                GetPassionScore(pawn, researchProject, ref score);
                GetSpecialTraitScore(pawn, researchProject, ref score);
                if (ModSettings_PawnsChooseResearch.checkTraits)
                {
                    GetTraitScore(pawn, researchProject, ref score);
                }
                //Core Techs
                if (researchProject.GetModExtension<ResearchCategory>().coreTech > 0)
                {
                    score += .76f;
                }
                if (ModSettings_PawnsChooseResearch.preferSimple)
                {
                    if (researchProject.techLevel > pawn.Faction.def.techLevel)
                    {
                        //Log.Message("This idea makes my head hurt, maybe I should pick something easier");
                        score += .5f * (researchProject.techLevel - pawn.Faction.def.techLevel - 1);
                    }
                }
            }
            return score;
        }
        private static void GetPassionScore(Pawn pawn, ResearchProjectDef researchProject, ref float score)
        {
            if (!ModSettings_PawnsChooseResearch.checkPassions || ModSettings_PawnsChooseResearch.interestsActivated)
            {
                return;
            }
            //Log.Message("Checking passions without interests");
            bool skillTrait = false;
            //Shooting
            if (researchProject.GetModExtension<ResearchCategory>().rangedTech > 0)
            {
                //Log.Message("Shooting score is " + (int)pawn.skills.GetSkill(SkillDefOf.Shooting).passion);
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Shooting).passion;
            }

            //Melee
            if (researchProject.GetModExtension<ResearchCategory>().meleeTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Melee).passion;
            }
            //Construction
            if (researchProject.GetModExtension<ResearchCategory>().constructionTech > 0)
            {
                //Log.Message("Construction score is " + (int)pawn.skills.GetSkill(SkillDefOf.Construction).passion);
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Construction).passion;
            }
            //Mining
            if (researchProject.GetModExtension<ResearchCategory>().miningTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Mining).passion;
            }
            //Cooking
            if (researchProject.GetModExtension<ResearchCategory>().cookingTech > 0)
            {
                //Log.Message("Cooking score is " + (int)pawn.skills.GetSkill(SkillDefOf.Cooking).passion);
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Cooking).passion;
            }
            //Growing
            if (researchProject.GetModExtension<ResearchCategory>().plantTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Plants).passion;
            }
            //Animals
            if (researchProject.GetModExtension<ResearchCategory>().animalTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Animals).passion;
            }
            //Crafting
            if (researchProject.GetModExtension<ResearchCategory>().craftTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Crafting).passion;
            }
            //Artistic
            if (researchProject.GetModExtension<ResearchCategory>().artTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Artistic).passion;
            }
            //Medical
            if (researchProject.GetModExtension<ResearchCategory>().medTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Medicine).passion;
            }
            //Social
            if (researchProject.GetModExtension<ResearchCategory>().socialTech > 0)
            {
                skillTrait = true;
                score += (int)pawn.skills.GetSkill(SkillDefOf.Social).passion;
            }
            //Intellectual (Uncategorized)
            if (!skillTrait)
            {
                score += (int)pawn.skills.GetSkill(SkillDefOf.Intellectual).passion;
            }
            //Log.Message("After passions, score is " + score);
        }

        private static void GetTraitScore(Pawn pawn, ResearchProjectDef researchProject, ref float score)
        {
            List<Trait> traits = pawn.story.traits.allTraits;
            for (int i = 0; i < traits.Count; i++)
            {
                TraitDef curTrait = traits[i].def;
                //Likes to Coordinate
                if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree <= 0) ||
                    curTrait.GetModExtension<ResearchCategory>().progressTech > 0)
                {
                    score += researchProject.ProgressPercent * 2;
                }
                //Lone Researcher
                if (curTrait.GetModExtension<ResearchCategory>().progressTech < 0)
                {
                    score -= researchProject.ProgressPercent * 2;
                }
                //Low Tech
                if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree > 0) ||
                    (curTrait == TraitDefOf.Nerves && traits[i].Degree > 0) ||
                    curTrait.GetModExtension<ResearchCategory>().complexTech < 0)
                {
                    score -= researchProject.baseCost / 1000;
                }
                //High Tech
                if (curTrait.GetModExtension<ResearchCategory>().complexTech > 0)
                {
                    score += researchProject.baseCost / 1000;
                }
                //Ranged
                if (researchProject.GetModExtension<ResearchCategory>().rangedTech > 0)
                {
                    if ((curTrait == TraitDefOf.NaturalMood && traits[i].Degree < 0) ||
                        curTrait.GetModExtension<ResearchCategory>().rangedTech > 0)
                    {
                        score += 1;
                    }
                    else if ((traits[i].def == TraitDefOf.NaturalMood && traits[i].Degree > 0) ||
                            traits[i].def.GetModExtension<ResearchCategory>().rangedTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Melee
                if (researchProject.GetModExtension<ResearchCategory>().meleeTech > 0)
                {
                    if ((curTrait == TraitDefOf.NaturalMood && traits[i].Degree < 0) ||
                        curTrait.GetModExtension<ResearchCategory>().meleeTech > 0)
                    {
                        score += 1;
                    }
                    else if ((traits[i].def == TraitDefOf.NaturalMood && traits[i].Degree > 0) ||
                            traits[i].def.GetModExtension<ResearchCategory>().meleeTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Construction
                if (researchProject.GetModExtension<ResearchCategory>().constructionTech > 0)
                {
                    if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree > 0) ||
                        curTrait.GetModExtension<ResearchCategory>().constructionTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().constructionTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Mining
                if (researchProject.GetModExtension<ResearchCategory>().miningTech > 0)
                {
                    if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree > 0) ||
                                curTrait.GetModExtension<ResearchCategory>().miningTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().miningTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Cooking
                if (researchProject.GetModExtension<ResearchCategory>().cookingTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().cookingTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().cookingTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Planting
                if (researchProject.GetModExtension<ResearchCategory>().plantTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().plantTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().plantTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Animals
                if (researchProject.GetModExtension<ResearchCategory>().animalTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().animalTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().animalTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Crafting
                if (researchProject.GetModExtension<ResearchCategory>().craftTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().craftTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().craftTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Artistic
                if (researchProject.GetModExtension<ResearchCategory>().artTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().artTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().artTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Medical
                if (researchProject.GetModExtension<ResearchCategory>().medTech > 0)
                {
                    if ((curTrait == TraitDefOf.NaturalMood && traits[i].Degree < 0) ||
                        (curTrait == TraitDef.Named("Immunity") && traits[i].Degree < 0) ||
                         curTrait.GetModExtension<ResearchCategory>().medTech > 0)
                    {
                        score += 1;
                    }
                    else if ((traits[i].def == TraitDef.Named("Immunity") && traits[i].Degree > 0) ||
                            traits[i].def.GetModExtension<ResearchCategory>().medTech < 0)
                    {
                        score -= 2;
                    }
                }
                //Social
                if (researchProject.GetModExtension<ResearchCategory>().socialTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().socialTech > 0)
                    {
                        score += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().socialTech < 0)
                    {
                        score -= 2;
                    }
                }
            }
            //Log.Message("After traits, score is " + score);
        }

        private static void GetSpecialTraitScore(Pawn pawn, ResearchProjectDef researchProject, ref float score)
        {
            List<Trait> traits = pawn.story.traits.allTraits;
            for (int i = 0; i < traits.Count; i++)
            {
                TraitDef curTrait = traits[i].def;
                if (curTrait == TraitDefOf.Transhumanist)
                {
                    if (researchProject.GetModExtension<ResearchCategory>().cyberTech > 0 ||
                        researchProject.defName == "Machining")
                    {
                        score += 3;
                    }
                    else if (researchProject.prerequisites != null)
                    {
                        foreach (ResearchProjectDef prerequisite in researchProject.prerequisites)
                        {
                            if (prerequisite.GetModExtension<ResearchCategory>().cyberTech > 0)
                            {
                                score += 3;
                                break;
                            }
                        }
                    }
                }

                if (curTrait == TraitDefOf.DrugDesire && traits[i].Degree > 0)
                {
                    if (researchProject.defName == "PsychoidBrewing" 
                        || researchProject.defName == "DrugProduction")
                    {
                        score += 3;
                    }
                    if (researchProject.prerequisites != null && researchProject.prerequisites.Contains(ResearchProjectDef.Named("DrugProduction")))
                    {
                        score += 3;
                    }
                }
                if (curTrait == TraitDef.Named("Ascetic"))
                {
                    if (researchProject == ResearchProjectDef.Named("NutrientPaste"))
                    {
                        score += 2;
                    }
                }
                if (curTrait == TraitDefOf.Pyromaniac)
                {
                    if (researchProject.defName == "Electricity" ||
                        researchProject.defName == "Batteries" ||
                        researchProject.defName == "IEDs" ||
                        researchProject.defName == "Mortars" ||
                        researchProject.defName == "BiofuelRefining" ||
                        researchProject.defName == "SmokepopBelt" ||
                        researchProject.defName == "ChargedShot" ||
                        researchProject.defName == "ShipReactor")
                    {
                        score += 1;
                    }
                }
                if (curTrait == TraitDefOf.Nudist)
                {
                    ResearchProjectDef complexClothing = ResearchProjectDef.Named("ComplexClothing");
                    if (researchProject.defName == "ComplexClothing" 
                        || (researchProject.prerequisites != null && researchProject.prerequisites.Contains(complexClothing)))
                    {
                        score -= 2;
                    }
                }

                // Vanilla Traits Expanded Special Preferences
                if (ModSettings_PawnsChooseResearch.vanillaTraitsActivated)
                {
                    if (curTrait == TraitDef.Named("VTE_Clumsy"))
                    {
                        if (researchProject == ResearchProjectDef.Named("Autodoors"))
                        {
                            score += 1;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_ColdInclined"))
                    {
                        if (researchProject == ResearchProjectDef.Named("PassiveCooler") ||
                            researchProject == ResearchProjectDef.Named("AirConditioning"))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_Neat"))
                    {
                        if (researchProject == ResearchProjectDef.Named("SterileMaterials"))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_CouchPotato"))
                    {
                        if (researchProject == ResearchProjectDef.Named("TubeTelevision") ||
                            researchProject == ResearchProjectDef.Named("FlatscreenTelevision"))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_ChildOfSea"))
                    {
                        if (researchProject == ResearchProjectDef.Named("WatermillGenerator"))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_ChildOfMountain"))
                    {
                        if (researchProject == ResearchProjectDef.Named("GeothermalPower") ||
                              researchProject == ResearchProjectDef.Named("DeepDrilling") ||
                              researchProject == ResearchProjectDef.Named("GroundPenetratingScanner"))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_DrunkenMaster") ||
                        curTrait == TraitDef.Named("VTE_Lush"))
                    {
                        if (researchProject == ResearchProjectDef.Named("Brewing") ||
                            researchProject.prerequisites.Contains(ResearchProjectDef.Named("Brewing")))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_Ecologist"))
                    {
                        if (researchProject == ResearchProjectDef.Named("SolarPanels") ||
                            researchProject == ResearchProjectDef.Named("WatermillGenerator") ||
                            researchProject == ResearchProjectDef.Named("GeothermalPower"))
                        {
                            score += 3;
                        }
                    }
                    if (curTrait == TraitDef.Named("VTE_Wanderlust"))
                    {
                        if (researchProject == ResearchProjectDef.Named("PackagedSurvivalMeal") ||
                            researchProject == ResearchProjectDef.Named("TransportPod"))
                        {
                            score += 3;
                        }
                    }
                    if (traits[i].def == TraitDef.Named("VTE_Technophobe"))
                    {
                        score -= (int)researchProject.techLevel;
                    }
                }
            }
            //Log.Message("After special traits, score is " + score);
        }
    }
}