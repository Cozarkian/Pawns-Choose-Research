using Verse;
using UnityEngine;

namespace VarietyMatters
{
    class Mod_VarietyMatters : Mod
    {
        public Mod_VarietyMatters(ModContentPack content) : base(content)
        {
            GetSettings<ModSettings_VarietyMatters>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect rect = new Rect(260f, 50f, inRect.width * .4f, inRect.height);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(rect);
            if (VarietyRecord.ReplimatsAvailable)
            {
                listingStandard.CheckboxLabeled("Replimat foods are synthetic: ", ref ModSettings_VarietyMatters.replimatSynthetic);
            }
            listingStandard.CheckboxLabeled("Ignore ingredients: ", ref ModSettings_VarietyMatters.ignoreIngredients);
            listingStandard.Label("Max Ingredients When Stacking:".Translate());
            string numIngredientsBuffer = ModSettings_VarietyMatters.numIngredients.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.numIngredients, ref numIngredientsBuffer);
            listingStandard.GapLine();
            listingStandard.GapLine();
            listingStandard.Label("Required Variety per Expectation Level:".Translate());
            listingStandard.GapLine();
            listingStandard.Label("Extremely Low (default 2):".Translate());
            string extremelyLowBuffer = ModSettings_VarietyMatters.extremelyLowVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.extremelyLowVariety, ref extremelyLowBuffer);
            listingStandard.Label("Very Low (default 4):".Translate());
            string veryLowBuffer = ModSettings_VarietyMatters.veryLowVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.veryLowVariety, ref veryLowBuffer);
            listingStandard.Label("Low (default 6):".Translate());
            string lowBuffer = ModSettings_VarietyMatters.lowVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.lowVariety, ref lowBuffer);
            listingStandard.Label("Moderate (default 9):".Translate());
            string moderateBuffer = ModSettings_VarietyMatters.moderateVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.moderateVariety, ref moderateBuffer);
            listingStandard.Label("High (default 12):".Translate());
            string highBuffer = ModSettings_VarietyMatters.highVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.highVariety, ref highBuffer);
            listingStandard.Label("Sky High (default 16):".Translate());
            string skyHighBuffer = ModSettings_VarietyMatters.skyHighVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.skyHighVariety, ref skyHighBuffer);
            listingStandard.Label("Noble and Royal (default 20):".Translate());
            string nobleBuffer = ModSettings_VarietyMatters.nobleVariety.ToString();
            listingStandard.IntEntry(ref ModSettings_VarietyMatters.nobleVariety, ref nobleBuffer);
            listingStandard.GapLine(12);
            listingStandard.GapLine(12);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "VarietyMatters".Translate();
        }
    }
}
