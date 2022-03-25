using System.Reflection;
using HarmonyLib;
using Verse;

namespace BetterNutrientPasteDispenserControls;

[StaticConstructorOnStartup]
internal class Main
{
    static Main()
    {
        //Log.Message("Hello from Harmony in scope: com.github.harmony.rimworld.maarx.betternutrientpastedispensercontrols");
        var harmony = new Harmony("com.github.harmony.rimworld.maarx.betternutrientpastedispensercontrols");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}

//public override IEnumerable<Gizmo> GetGizmos()