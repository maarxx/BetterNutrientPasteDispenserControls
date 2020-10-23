using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace BetterNutrientPasteDispenserControls
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            //Log.Message("Hello from Harmony in scope: com.github.harmony.rimworld.maarx.betternutrientpastedispensercontrols");
            var harmony = new Harmony("com.github.harmony.rimworld.maarx.betternutrientpastedispensercontrols");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //public override IEnumerable<Gizmo> GetGizmos()
    [HarmonyPatch(typeof(Building))]
    [HarmonyPatch("GetGizmos")]
    class Patch_Building_GetGizmos
    {
        static void Postfix(Building __instance, ref IEnumerable<Gizmo> __result)
        {
            if (__instance is Building_NutrientPasteDispenser)
            {
                //Log.Message("Hello from Harmony Postfix Patch_Building_GetGizmos " + __result.Count());

                //Building_NutrientPasteDispenser myThis = (Building_NutrientPasteDispenser)__instance;
                //MethodInfo CanDispenseNow = myThis.GetType().GetMethod("CanDispenseNow", BindingFlags.Instance | BindingFlags.Public);
                //MethodInfo TryDispenseFood = myThis.GetType().GetMethod("TryDispenseFood", BindingFlags.Instance | BindingFlags.Public);

                //int[] numbers = { 1, 5, 25 };
                //foreach (int i in numbers)
                //{
                //    Gizmo newGizmo = new Command_Action
                //    {
                //        defaultLabel = "Dispense " + i,
                //        action = delegate
                //        {
                //            int k = i;
                //            for (int j = 0; j < k && (bool) CanDispenseNow.Invoke(myThis, null); j++)
                //            {
                //                TryDispenseFood.Invoke(myThis, null);
                //            }
                //        }
                //    };
                //    __result.Add(newGizmo);
                //}

                Building_NutrientPasteDispenser myThis = (Building_NutrientPasteDispenser)__instance;

                int[] numbers = { 1, 5, 25 };
                foreach (int i in numbers)
                {
                    Gizmo newGizmo = new Command_Action
                    {
                        defaultLabel = "Dispense " + i,
                        icon = ContentFinder<Texture2D>.Get("UI/Designators/Open"),
                        action = delegate
                        {
                            for (int j = 0; j < i && myThis.CanDispenseNow; j++)
                            {
                                Thing meal = myThis.TryDispenseFood();
                                GenPlace.TryPlaceThing(meal, myThis.InteractionCell, myThis.Map, ThingPlaceMode.Near);
                            }
                        }
                    };
                    __result = __result.AddItem(newGizmo);
                }
                //Log.Message("Goodbye from Harmony Postfix Patch_Building_GetGizmos " + __result.Count());
            }
        }
    }

}
