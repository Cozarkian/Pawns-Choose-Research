using Verse;

namespace PawnsChooseResearch
{
    public class ModSettings_PawnsChooseResearch : ModSettings
    {
        //variables to change 
        public static bool groupResearch = false;
        public static bool restoreControl = false;
        public static bool mustHaveSkill = true;
        public static bool checkPassions = true;
        public static bool checkTraits = true;
        public static bool preferSimple = true;

        public static bool interestsActivated = false;
        public static bool vanillaTraitsActivated = false;
        //public static bool VBE_Activated = false;
        public static bool TBnRE_Activated = true;

        public static void CheckMods()
        {
            interestsActivated = false;
            vanillaTraitsActivated = false;
            if (ModLister.GetActiveModWithIdentifier("Mlie.DInterestsFramework") != null || ModLister.GetActiveModWithIdentifier("dame.interestsframework") != null)
            {
                interestsActivated = true; 
            }
            if (ModLister.GetActiveModWithIdentifier("VanillaExpanded.VanillaTraitsExpanded") != null)
            {
                vanillaTraitsActivated = true;
            }
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref groupResearch, "groupResearch", false);
            Scribe_Values.Look(ref restoreControl, "restoreControl", false);
            Scribe_Values.Look(ref mustHaveSkill, "musthaveSkill", true);
            Scribe_Values.Look(ref checkPassions, "checkPassions", true);
            Scribe_Values.Look(ref checkTraits, "checkTraits", true);
            Scribe_Values.Look(ref preferSimple, "preferSimple", true);

            //Scribe_Values.Look(ref VBE_Activated, "VBE_Activated", true);
            Scribe_Values.Look(ref TBnRE_Activated, "TBnRE_Activated", true);
            base.ExposeData();
        }
    }
}