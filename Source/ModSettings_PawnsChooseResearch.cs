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
        public static bool checkInterests = true;
        public static bool checkTraits = true;
        public static bool preferSimple = true;

        public static bool interestsActivated = false;
        public static bool vanillaTraitsActivated = false;

        public static void CheckMods()
        {
            interestsActivated = false;
            vanillaTraitsActivated = false;
            if (ModLister.GetActiveModWithIdentifier("Cozarkian.PawnsChooseInterests") != null)
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
            Scribe_Values.Look(ref checkInterests, "checkInterests", true);
            Scribe_Values.Look(ref checkTraits, "checkTraits", true);
            Scribe_Values.Look(ref preferSimple, "preferSimple", true);

            base.ExposeData();
        }
    }
}