using RimWorld;
using Verse;


namespace PawnsChooseResearch
{
    public class VTE_Integration
    {
        public static float GetSpecialTraitScore_VTE(ResearchProjectDef researchProject, TraitDef curTrait)
        {
            float spTraitScore = 0f;
            if (curTrait == TraitDef.Named("VTE_Clumsy"))
            {
                if (researchProject == ResearchProjectDef.Named("Autodoors"))
                {
                    spTraitScore += 1f;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_ColdInclined"))
            {
                if (researchProject == ResearchProjectDef.Named("PassiveCooler") ||
                    researchProject == ResearchProjectDef.Named("AirConditioning"))
                {
                    spTraitScore += 3f;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_Neat"))
            {
                if (researchProject == ResearchProjectDef.Named("SterileMaterials"))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_CouchPotato"))
            {
                if (researchProject == ResearchProjectDef.Named("TubeTelevision") ||
                    researchProject == ResearchProjectDef.Named("FlatscreenTelevision"))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_ChildOfSea"))
            {
                if (researchProject == ResearchProjectDef.Named("WatermillGenerator"))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_ChildOfMountain"))
            {
                if (researchProject == ResearchProjectDef.Named("GeothermalPower") ||
                      researchProject == ResearchProjectDef.Named("DeepDrilling") ||
                      researchProject == ResearchProjectDef.Named("GroundPenetratingScanner"))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_DrunkenMaster") ||
                curTrait == TraitDef.Named("VTE_Lush"))
            {
                if (researchProject == ResearchProjectDef.Named("Brewing") || 
                    (researchProject.prerequisites != null && researchProject.prerequisites.Contains(ResearchProjectDef.Named("Brewing"))))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_Ecologist"))
            {
                if (researchProject == ResearchProjectDef.Named("SolarPanels") ||
                    researchProject == ResearchProjectDef.Named("WatermillGenerator") ||
                    researchProject == ResearchProjectDef.Named("GeothermalPower"))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_Wanderlust"))
            {
                if (researchProject == ResearchProjectDef.Named("PackagedSurvivalMeal") ||
                    researchProject == ResearchProjectDef.Named("TransportPod"))
                {
                    spTraitScore += 3;
                }
            }
            else if (curTrait == TraitDef.Named("VTE_Technophobe"))
            {
                spTraitScore -= ((int)researchProject.techLevel - 2);
            }
            return spTraitScore;
        }
    }
}
