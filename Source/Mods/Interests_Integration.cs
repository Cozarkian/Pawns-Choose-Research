using RimWorld;
using Verse;
using DInterests;

namespace PawnsChooseResearch
{
    public class Interests_Integration
    {
        public static float GetInterestScore(Pawn pawn, ResearchProjectDef researchProject)
        {
            float interestScore = 0f;
            bool interestTech = false;
            //Shooting
            if (researchProject.GetModExtension<ResearchCategory>().rangedTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Shooting).passion) / 100;
                interestTech = true;
            }

            //Melee
            if (researchProject.GetModExtension<ResearchCategory>().meleeTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Melee).passion) / 100;
                interestTech = true;
            }
            //Construction
            if (researchProject.GetModExtension<ResearchCategory>().constructionTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Construction).passion) / 100;
                interestTech = true;
            }
            //Mining
            if (researchProject.GetModExtension<ResearchCategory>().miningTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Mining).passion) / 100;
                interestTech = true;
            }
            //Cooking
            if (researchProject.GetModExtension<ResearchCategory>().cookingTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Cooking).passion) / 100;
                interestTech = true;
            }
            //Growing
            if (researchProject.GetModExtension<ResearchCategory>().plantTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Plants).passion) / 100;
                interestTech = true;
            }
            //Animals
            if (researchProject.GetModExtension<ResearchCategory>().animalTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Animals).passion) / 100;
                interestTech = true;
            }
            //Crafting
            if (researchProject.GetModExtension<ResearchCategory>().craftTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Crafting).passion) / 100;
                interestTech = true;
            }
            //Artistic
            if (researchProject.GetModExtension<ResearchCategory>().artTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Artistic).passion) / 100;
                interestTech = true;
            }
            //Medical
            if (researchProject.GetModExtension<ResearchCategory>().medTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Medicine).passion) / 100;
                interestTech = true;
            }
            //Social
            if (researchProject.GetModExtension<ResearchCategory>().socialTech > 0)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Social).passion) / 100;
                interestTech = true;
            }
            //Intellectual
            if (!interestTech)
            {
                interestScore += InterestBase.GetValue((int)pawn.skills.GetSkill(SkillDefOf.Intellectual).passion) / 100;
            }
            return interestScore;
        }
    }
}
