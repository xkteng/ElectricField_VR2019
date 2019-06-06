using System.IO;
using UnityEditor;
using UnityEngine;


namespace KaiTool.AssetBundleManipulation
{
    public class KaiTool_AssetBundleEditor : Editor
    {
        [MenuItem("Tools/KaiTool/AssetBundle/CreateAssetBundle")]
        private static void CreateAssetBundle()
        {
            var path = Application.dataPath + "/AssetBundles";
            if (!Directory.Exists(path))
            {
                BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
                AssetDatabase.Refresh();
            }
        }
        [MenuItem("Tools/KaiTool/AssetBundle/UnloadAllAssetBundles")]
        private static void UnloadAllAssetBundles()
        {
            AssetBundle.UnloadAllAssetBundles(true);
        }

    }
}
