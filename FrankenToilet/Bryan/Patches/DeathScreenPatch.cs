namespace FrankenToilet.Bryan.Patches;

using FrankenToilet.Core;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

[PatchOnEntry]
[HarmonyPatch(typeof(LaughingSkull))]
public static class DeathScreenPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    public static void oiweoufjds(LaughingSkull __instance)
    {
        __instance.GetComponent<Animator>().runtimeAnimatorController = BundleLoader.BadLaughingSkullAnim;
        __instance.gameObject.AddComponent<LaughLaugh>();

        var blackScreen = __instance.transform.parent.GetComponent<Image>();
        blackScreen.sprite = BundleLoader.Trans;
        blackScreen.color = new(1f, 1f, 1f, 1f);
    }
}

public class LaughLaugh : MonoBehaviour
{
    public void OnEnable()
    {
        var src = GetComponent<AudioSource>();
        src.clip = BundleLoader.BadLaughing;
        src.Play();
        src.loop = true;
    }
}