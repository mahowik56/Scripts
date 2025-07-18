using System;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class GenerateAssetBundleDB
{
#if UNITY_EDITOR
    [MenuItem("Tools/Generate AssetBundle DB")]
    public static void Generate()
    {
        string outputPath = "AssetBundles";
        Directory.CreateDirectory(outputPath);
        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
        if (manifest == null)
        {
            UnityEngine.Debug.LogError("Failed to build asset bundles");
            return;
        }

        var db = new Db();
        foreach (var bundle in manifest.GetAllAssetBundles())
        {
            var info = new BundleInfo();
            info.bundleName = bundle;
            info.hash = manifest.GetAssetBundleHash(bundle).ToString();
            uint crc;
            BuildPipeline.GetCRCForAssetBundle(Path.Combine(outputPath, bundle), out crc);
            info.crc = crc;
            info.cacheCrc = crc;
            info.size = new FileInfo(Path.Combine(outputPath, bundle)).Length;
            info.dependenciesNames = manifest.GetAllDependencies(bundle);

            var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(bundle);
            foreach (var path in assetPaths)
            {
                var assetInfo = new AssetInfo();
                assetInfo.guid = AssetDatabase.AssetPathToGUID(path);
                assetInfo.objectName = path;
                assetInfo.typeHash = AssetDatabase.GetMainAssetTypeAtPath(path).GetHashCode();
                info.assets.Add(assetInfo);
                db.guids.Add(assetInfo.guid);
            }
            db.bundles.Add(info);
        }

        string json = JsonUtility.ToJson(db, true);
        File.WriteAllText(Path.Combine(outputPath, "db.json"), json);
        UnityEngine.Debug.Log("db.json generated at " + Path.Combine(outputPath, "db.json"));
    }
#endif

    [Serializable]
    class Db
    {
        public List<BundleInfo> bundles = new List<BundleInfo>();
        public List<string> guids = new List<string>();
    }

    [Serializable]
    class BundleInfo
    {
        public string bundleName;
        public string hash;
        public uint crc;
        public uint cacheCrc;
        public long size;
        public string[] dependenciesNames;
        public List<AssetInfo> assets = new List<AssetInfo>();
        public uint modificationHash;
    }

    [Serializable]
    class AssetInfo
    {
        public string guid;
        public string objectName;
        public int typeHash;
    }
}
