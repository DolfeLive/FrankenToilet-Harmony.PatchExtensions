using System;
using System.IO;
using System.Reflection;
using FrankenToilet.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FrankenToilet.triggeredidiot;

[EntryPoint]
public static class AssetsController
{
    private static AssetBundle _assets;

    // IsSlopTuber errored once so im paranoid now
    public static bool IsSlopSafe
    {
        get
        {
            try
            {
                return SteamHelper.IsSlopTuber;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    [EntryPoint]
    public static void Init()
    {
        LogHelper.LogInfo("[triggeredidiot] Loading assets");
        byte[] data;
        try
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"FrankenToilet.triggeredidiot.assets.bundle";
            var s = assembly.GetManifestResourceStream(resourceName);
            s = s ?? throw new FileNotFoundException($"Could not find embedded resource '{resourceName}'.");
            using var ms = new MemoryStream();
            s.CopyTo(ms);
            data = ms.ToArray();
        }
        catch (Exception ex)
        {
            LogHelper.LogError($"[triggeredidiot] Error loading assets: " + ex.Message);
            return;
        }

        SceneManager.sceneLoaded += (scene, lcm) =>
        {
            if (_assets != null) return;

            _assets = AssetBundle.LoadFromMemory(data);
            LogHelper.LogInfo("[triggeredidiot] Loaded assets");
        };
    }

    public static GameObject? LoadAsset(string assetName)
    {
        if (_assets == null) return null;
        var go = _assets.LoadAsset<GameObject>(assetName);
        if(go == null) return null;
        return Object.Instantiate<GameObject>(go);
    }
}