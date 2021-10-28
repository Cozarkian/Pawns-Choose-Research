using Verse;
using UnityEngine;

namespace PawnsChooseResearch
{
    class Mod_PawnsChooseResearch : Mod
    {
        public Mod_PawnsChooseResearch(ModContentPack content) : base(content)
        {
            GetSettings<ModSettings_PawnsChooseResearch>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect rect = new Rect(260f, 50f, inRect.width * .4f, inRect.height);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(rect);
            listingStandard.CheckboxLabeled("Restore Player Control: ", ref ModSettings_PawnsChooseResearch.restoreControl);
            if (!ModSettings_PawnsChooseResearch.restoreControl)
            {
                listingStandard.CheckboxLabeled("Pawns Coordinate Research: ", ref ModSettings_PawnsChooseResearch.groupResearch);
                if (!ModSettings_PawnsChooseResearch.groupResearch)
                {
                    listingStandard.CheckboxLabeled("Must Have Skill Capability: ", ref ModSettings_PawnsChooseResearch.mustHaveSkill);
                }
                listingStandard.CheckboxLabeled("Traits Affect Research: ", ref ModSettings_PawnsChooseResearch.checkTraits);
                if (ModSettings_PawnsChooseResearch.interestsActivated)
                {
                    listingStandard.CheckboxLabeled("Interests Affect Research: ", ref ModSettings_PawnsChooseResearch.checkPassions);
                }
                else
                {
                    listingStandard.CheckboxLabeled("Passions Affect Research: ", ref ModSettings_PawnsChooseResearch.checkPassions);
                }
                listingStandard.CheckboxLabeled("Avoid too advanced research: ", ref ModSettings_PawnsChooseResearch.preferSimple);
                if (ModLister.GetActiveModWithIdentifier("GwinnBleidd.ResearchTweaks") != null)
                {
                    listingStandard.CheckboxLabeled("Reverse Engineering Assigns Research", ref ModSettings_PawnsChooseResearch.TBnRE_Activated);
                }
                /*
                if (ModLister.GetActiveModWithIdentifier("VanillaExpanded.VBooksE") != null)
                {
                    listingStandard.CheckboxLabeled("Technology Blueprints Assign Research", ref ModSettings_PawnsChooseResearch.VBE_Activated);
                }
                */
            }
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "PawnsChooseResearch".Translate();
        }
    }
}
