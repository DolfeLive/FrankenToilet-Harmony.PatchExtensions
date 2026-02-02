namespace FrankenToilet.Bryan.Patches;

using FrankenToilet.Core;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[PatchOnEntry]
[HarmonyPatch(typeof(TimeController))]
public static class ParryPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("ParryFlash")]
    public static void rghsrhgnfgrf(TimeController __instance) =>
        __instance.parryFlash?.GetComponent<Image>().sprite = BundleLoader.DoomahImg;
}