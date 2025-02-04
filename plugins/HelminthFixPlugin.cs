using BepInEx;
using System.Security.Permissions;
using System.Security;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

[module: UnverifiableCode]
#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[assembly: HG.Reflection.SearchableAttribute.OptIn]

namespace HelminthFix
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class HelminthFixPlugin : BaseUnityPlugin
    {
        public const string
            GUID = "groovesalad." + NAME,
            NAME = "HelminthFix",
            VERSION = "1.0.0";

        private void Awake()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "helminthroost")
            {
                GameObject[] rootObjects = scene.GetRootGameObjects();
                GameObject lighting = Array.Find(rootObjects, x => x.name == "HOLDER: Lighting");
                if (lighting)
                {
                    Transform pp = lighting.transform.Find("Weather, Helminthroost/PP + Amb");
                    if (pp && pp.TryGetComponent(out PostProcessVolume ppVolume))
                    {
                        ppVolume.priority = 5f;
                    }
                }
            }
        }
    }
}
