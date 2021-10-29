using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PawnsChooseResearch
{
    public class ResearchPreferences
    {
        public static float GetPreferenceScore(Pawn pawn, ResearchProjectDef researchProject)
        {
            float score = 1 + researchProject.ProgressPercent / 2;
            //Log.Message("Getting score for " + researchProject.label + ", base score is " + score);

            if (researchProject.HasModExtension<ResearchCategory>())
            {
                if (ModSettings_PawnsChooseResearch.checkPassions)
                {
                    if (ModSettings_PawnsChooseResearch.interestsActivated)
                    {
                        score += Interests_Integration.GetInterestScore(pawn, researchProject);
                        //Log.Message("Score after interests is " + score);
                    }
                    else
                    {
                        score += GetPassionScore(pawn, researchProject);
                        //Log.Message("Score after passions is " + score);
                    }
                }
                if (ModSettings_PawnsChooseResearch.checkTraits)
                {
                    score += GetTraitScore(pawn, researchProject);
                    //Log.Message("Score after traits is " + score);
                }
                score += GetSpecialTraitScore(pawn, researchProject);
                //Log.Message("Score after special traits is " + score);
                //Core Techs
                if (researchProject.GetModExtension<ResearchCategory>().coreTech > 0)
                {
                    score += pawn.skills.GetSkill(SkillDefOf.Intellectual).Level * .05f;
                    //Log.Message("Score for core tech is " + score);
                }
            }
            if (ModSettings_PawnsChooseResearch.preferSimple)
            {
                if (researchProject.techLevel > Faction.OfPlayer.def.techLevel)
                {
                    score -= ((int)Faction.OfPlayerSilentFail.def.techLevel - (int)researchProject.techLevel) / (pawn.skills.GetSkill(SkillDefOf.Intellectual).Level + 1f);
                    //Log.Message("Score after tech level is " + score);
                }
            }
            score *= Rand.Range(.5f, 1f);
            //Log.Message("Final score is " + score);
            return score;
        }
        private static float GetPassionScore(Pawn pawn, ResearchProjectDef researchProject)
        {
            //Log.Message("Checking passions without interests");
            float passionScore = 0f;
            bool passionTech = false;
            //Shooting
            if (researchProject.GetModExtension<ResearchCategory>().rangedTech > 0)
            {
                //Log.Message("Shooting score is " + (int)pawn.skills.GetSkill(SkillDefOf.Shooting).passion);
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Shooting).passion;
                passionTech = true;
            }

            //Melee
            if (researchProject.GetModExtension<ResearchCategory>().meleeTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Melee).passion;
                passionTech = true;
            }
            //Construction
            if (researchProject.GetModExtension<ResearchCategory>().constructionTech > 0)
            {
                //Log.Message("Construction score is " + (int)pawn.skills.GetSkill(SkillDefOf.Construction).passion);
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Construction).passion;
                passionTech = true;
            }
            //Mining
            if (researchProject.GetModExtension<ResearchCategory>().miningTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Mining).passion;
                passionTech = true;
            }
            //Cooking
            if (researchProject.GetModExtension<ResearchCategory>().cookingTech > 0)
            {
                //Log.Message("Cooking score is " + (int)pawn.skills.GetSkill(SkillDefOf.Cooking).passion);
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Cooking).passion;
                passionTech = true;
            }
            //Growing
            if (researchProject.GetModExtension<ResearchCategory>().plantTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Plants).passion;
                passionTech = true;
            }
            //Animals
            if (researchProject.GetModExtension<ResearchCategory>().animalTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Animals).passion;
                passionTech = true;
            }
            //Crafting
            if (researchProject.GetModExtension<ResearchCategory>().craftTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Crafting).passion;
                passionTech = true;
            }
            //Artistic
            if (researchProject.GetModExtension<ResearchCategory>().artTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Artistic).passion;
                passionTech = true;
            }
            //Medical
            if (researchProject.GetModExtension<ResearchCategory>().medTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Medicine).passion;
                passionTech = true;
            }
            //Social
            if (researchProject.GetModExtension<ResearchCategory>().socialTech > 0)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Social).passion;
                passionTech = true;
            }
            //Intellectual (Uncategorized)
            if (!passionTech)
            {
                passionScore += (int)pawn.skills.GetSkill(SkillDefOf.Intellectual).passion;
            }
            //Log.Message("Passion multiplier is " + passionScore);
            return passionScore;
        }

        //Uses Interests Mod
        private static float GetTraitScore(Pawn pawn, ResearchProjectDef researchProject)
        {
            float traitScore = 0f;
            List<Trait> traits = pawn.story.traits.allTraits;
            for (int i = 0; i < traits.Count; i++)
            {
                TraitDef curTrait = traits[i].def;
                //Likes to Coordinate
                if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree <= 0) ||
                    curTrait.GetModExtension<ResearchCategory>().progressTech > 0)
                {
                    traitScore += researchProject.ProgressPercent - 1f;
                }
                //Lone Researcher
                if (curTrait.GetModExtension<ResearchCategory>().progressTech < 0)
                {
                    traitScore += 1f - researchProject.ProgressPercent;
                }
                //Low Tech
                if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree > 0) ||
                    (curTrait == TraitDefOf.Nerves && traits[i].Degree > 0) ||
                    curTrait.GetModExtension<ResearchCategory>().complexTech < 0)
                {
                    traitScore -= researchProject.baseCost / (int)researchProject.techLevel / 1000;
                }
                //High Tech
                if (curTrait.GetModExtension<ResearchCategory>().complexTech > 0)
                {
                    traitScore += researchProject.baseCost / (int)researchProject.techLevel / 1000;
                }
                //Ranged
                if (researchProject.GetModExtension<ResearchCategory>().rangedTech > 0)
                {
                    if ((curTrait == TraitDefOf.NaturalMood && traits[i].Degree < 0) ||
                        curTrait.GetModExtension<ResearchCategory>().rangedTech > 0)
                    {
                        traitScore += 1f;
                    }
                    else if ((traits[i].def == TraitDefOf.NaturalMood && traits[i].Degree > 0) ||
                            traits[i].def.GetModExtension<ResearchCategory>().rangedTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Melee
                if (researchProject.GetModExtension<ResearchCategory>().meleeTech > 0)
                {
                    if ((curTrait == TraitDefOf.NaturalMood && traits[i].Degree < 0) ||
                        curTrait.GetModExtension<ResearchCategory>().meleeTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if ((traits[i].def == TraitDefOf.NaturalMood && traits[i].Degree > 0) ||
                            traits[i].def.GetModExtension<ResearchCategory>().meleeTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Construction
                if (researchProject.GetModExtension<ResearchCategory>().constructionTech > 0)
                {
                    if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree > 0) ||
                        curTrait.GetModExtension<ResearchCategory>().constructionTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().constructionTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Mining
                if (researchProject.GetModExtension<ResearchCategory>().miningTech > 0)
                {
                    if ((curTrait == TraitDefOf.Industriousness && traits[i].Degree > 0) ||
                                curTrait.GetModExtension<ResearchCategory>().miningTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().miningTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Cooking
                if (researchProject.GetModExtension<ResearchCategory>().cookingTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().cookingTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().cookingTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Planting
                if (researchProject.GetModExtension<ResearchCategory>().plantTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().plantTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().plantTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Animals
                if (researchProject.GetModExtension<ResearchCategory>().animalTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().animalTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().animalTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Crafting
                if (researchProject.GetModExtension<ResearchCategory>().craftTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().craftTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().craftTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Artistic
                if (researchProject.GetModExtension<ResearchCategory>().artTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().artTech > 0)
                    {
                        traitScore += 1;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().artTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Medical
                if (researchProject.GetModExtension<ResearchCategory>().medTech > 0)
                {
                    if ((curTrait == TraitDefOf.NaturalMood && traits[i].Degree < 0) ||
                        (curTrait == TraitDef.Named("Immunity") && traits[i].Degree < 0) ||
                         curTrait.GetModExtension<ResearchCategory>().medTech > 0)
                    {
                        traitScore += 1f;
                    }
                    else if ((traits[i].def == TraitDef.Named("Immunity") && traits[i].Degree > 0) ||
                            traits[i].def.GetModExtension<ResearchCategory>().medTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
                //Social
                if (researchProject.GetModExtension<ResearchCategory>().socialTech > 0)
                {
                    if (curTrait.GetModExtension<ResearchCategory>().socialTech > 0)
                    {
                        traitScore += 1f;
                    }
                    else if (traits[i].def.GetModExtension<ResearchCategory>().socialTech < 0)
                    {
                        traitScore -= 1f;
                    }
                }
            }
            return traitScore;
        }

        private static float GetSpecialTraitScore(Pawn pawn, ResearchProjectDef researchProject)
        {
            float spTraitScore = 0f;
            List<Trait> traits = pawn.story.traits.allTraits;
            for (int i = 0; i < traits.Count; i++)
            {
                TraitDef curTrait = traits[i].def;
                if (curTrait == TraitDefOf.Transhumanist)
                {
                    if (researchProject.GetModExtension<ResearchCategory>().cyberTech > 0 ||
                        researchProject.defName == "Machining")
                    {
                        spTraitScore += 3;
                    }
                    else if (researchProject.prerequisites != null)
                    {
                        foreach (ResearchProjectDef prerequisite in researchProject.prerequisites)
                        {
                            if (prerequisite.GetModExtension<ResearchCategory>().cyberTech > 0)
                            {
                                spTraitScore += 3;
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
                        spTraitScore += 3;
                    }
                    if (researchProject.prerequisites != null && researchProject.prerequisites.Contains(ResearchProjectDef.Named("DrugProduction")))
                    {
                        spTraitScore += 3;
                    }
                }
                if (curTrait == TraitDef.Named("Ascetic"))
                {
                    if (researchProject == ResearchProjectDef.Named("NutrientPaste"))
                    {
                        spTraitScore += 2;
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
                        spTraitScore += 1f;
                    }
                }
                if (curTrait == TraitDefOf.Nudist)
                {
                    ResearchProjectDef complexClothing = ResearchProjectDef.Named("ComplexClothing");
                    if (researchProject.defName == "ComplexClothing" 
                        || (researchProject.prerequisites != null && researchProject.prerequisites.Contains(complexClothing)))
                    {
                        spTraitScore -= 1f;
                    }
                }
                // Vanilla Traits Expanded Special Preferences
                if (ModSettings_PawnsChooseResearch.vanillaTraitsActivated)
                {
                    //Log.Message("Checking " + curTrait.label + " in VTE");
                    spTraitScore += VTE_Integration.GetSpecialTraitScore_VTE(researchProject, curTrait);    
                }
            }
            return spTraitScore;
        }
    }
}