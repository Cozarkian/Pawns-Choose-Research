using Verse;

namespace VarietyMatters
{
    class ModSettings_VarietyMatters : ModSettings
    {
        //variables to change 
        public static bool ignoreIngredients;
        public static bool replimatSynthetic = false;
        public static int extremelyLowVariety = 2;
        public static int veryLowVariety = 4;
        public static int lowVariety = 6;
        public static int moderateVariety = 9;
        public static int highVariety = 12;
        public static int skyHighVariety = 16;
        public static int nobleVariety = 20;
        public static int numIngredients = 3;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ignoreIngredients, "ignoreIngredients", false);
            Scribe_Values.Look(ref replimatSynthetic, "replimatSynthetic", false);
            Scribe_Values.Look(ref extremelyLowVariety, "extremelyLowVariety", 2);
            Scribe_Values.Look(ref veryLowVariety, "veryLowVariety", 4);
            Scribe_Values.Look(ref lowVariety, "lowVariety", 6);
            Scribe_Values.Look(ref moderateVariety, "moderateVariety", 9);
            Scribe_Values.Look(ref highVariety, "highVariety", 12);
            Scribe_Values.Look(ref skyHighVariety, "skyHighVariety", 16);
            Scribe_Values.Look(ref nobleVariety, "nobleVariety", 20);
            Scribe_Values.Look(ref numIngredients, "numIngredients", 3);

            base.ExposeData();
        }
    }
}
