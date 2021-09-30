using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PawnsChooseResearch
{
    public class ResearchCategory : DefModExtension
    {
        public int coreTech = 0;
        public int progressTech = 0;
        public int complexTech = 0;
        public int rangedTech = 0;
        public int meleeTech = 0;
        public int constructionTech = 0;
        public int miningTech = 0;
        public int cookingTech = 0;
        public int plantTech = 0;
        public int animalTech = 0;
        public int craftTech = 0;
        public int artTech = 0;
        public int medTech = 0;
        public int socialTech = 0;

        public int cyberTech = 0;
        public int neverTech = 0;

        public static void CategoryCheck()
        {
            Log.Message("Running Test");
            CECheck();
            /*
            foreach (ResearchProjectDef project in DefDatabase<ResearchProjectDef>.AllDefsListForReading)
            {
                if (!project.HasModExtension<ResearchCategory>())
                {
                    Log.Warning(project.label + " isn't assigned.");
                }
                if (project.GetModExtension<ResearchCategory>().neverTech > 0)
                {
                    Log.Message(project.label + " will  be researched last.");
                }                
                if (project.GetModExtension<ResearchCategory>().cyberTech > 0)
                {
                    Log.Message(project.label + " is abhorred by body purists.");
                }
                if (project.GetModExtension<ResearchCategory>().rangedTech > 0 &&
                    project.GetModExtension<ResearchCategory>().craftTech > 0)
                {
                    //Log.Message(project.label + " is a ranged craft.");
                }
            }

            ResearchProjectDef smithing = ResearchProjectDef.Named("Smithing");
            Log.Message("Smithing's coreTech score is " + smithing.GetModExtension<ResearchCategory>().coreTech);
            Log.Message("Smithing's progressTech score is " + smithing.GetModExtension<ResearchCategory>().progressTech);
            Log.Message("Smithing's complexTech score is " + smithing.GetModExtension<ResearchCategory>().complexTech);
            Log.Message("Smithing's rangedTech score is " + smithing.GetModExtension<ResearchCategory>().rangedTech);
            Log.Message("Smithing's meleeTech score is " + smithing.GetModExtension<ResearchCategory>().meleeTech);
            Log.Message("Smithing's constructionTech score is " + smithing.GetModExtension<ResearchCategory>().constructionTech);
            Log.Message("Smithing's miningTech score is " + smithing.GetModExtension<ResearchCategory>().miningTech);
            Log.Message("Smithing's cookingTech score is " + smithing.GetModExtension<ResearchCategory>().cookingTech);
            Log.Message("Smithing's plantTech score is " + smithing.GetModExtension<ResearchCategory>().plantTech);
            Log.Message("Smithing's animalTech score is " + smithing.GetModExtension<ResearchCategory>().animalTech);
            Log.Message("Smithing's craftTech score is " + smithing.GetModExtension<ResearchCategory>().craftTech);
            Log.Message("Smithing's artTech score is " + smithing.GetModExtension<ResearchCategory>().artTech);
            Log.Message("Smithing's medTech score is " + smithing.GetModExtension<ResearchCategory>().medTech);
            Log.Message("Smithing's socialTech score is " + smithing.GetModExtension<ResearchCategory>().socialTech);
            Log.Message("Smithing's cyberTech score is " + smithing.GetModExtension<ResearchCategory>().cyberTech);

            TraitDef smart = TraitDef.Named("TooSmart");
            Log.Message("Too smart's complexTech score is " + smart.GetModExtension<ResearchCategory>().complexTech);
            */
        }

        private static void CECheck()
        {
            List<ResearchProjectDef> projects = new List<ResearchProjectDef>()
            {
                ResearchProjectDef.Named("CE_TurretHeavyWeapons"),
                ResearchProjectDef.Named("CE_ChargeTurret"),
                ResearchProjectDef.Named("CE_HeavyTurret"),
                ResearchProjectDef.Named("CE_Launchers"),
                ResearchProjectDef.Named("CE_AdvancedLaunchers"),
                ResearchProjectDef.Named("CE_AdvancedAmmo"),
                ResearchProjectDef.Named("VFES_Artillery_Debug"),
            };
            for (int i = 0; i < projects.Count; i++)
            {
                Log.Message(projects[i].label + " ranged tech is " + projects[i].GetModExtension<ResearchCategory>().rangedTech);
                Log.Message(projects[i].label + " never tech is " + projects[i].GetModExtension<ResearchCategory>().neverTech);
            }
        }
    }
}


