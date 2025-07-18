using System;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class RestoreGuidsFromDb
{
#if UNITY_EDITOR
    [MenuItem("Tools/Restore GUIDs From DB")]
    public static void Restore()
    {
        string dbPath = Path.Combine(Application.dataPath, "..", "db.json");
        if (!File.Exists(dbPath))
        {
            Debug.LogError($"db.json not found at {dbPath}");
            return;
        }

        Db db = JsonUtility.FromJson<Db>(File.ReadAllText(dbPath));
        foreach (var bundle in db.bundles)
        {
            foreach (var asset in bundle.assets)
            {
                string metaPath = asset.objectName + ".meta";
                if (!File.Exists(metaPath))
                {
                    Debug.LogWarning($"Meta file not found for {asset.objectName}");
                    continue;
                }

                var lines = File.ReadAllLines(metaPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("guid:"))
                    {
                        lines[i] = "guid: " + asset.guid;
                        break;
                    }
                }
                File.WriteAllLines(metaPath, lines);
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("GUIDs restored from db.json");
    }
#else
    public static void Restore()
    {
        Debug.LogError("RestoreGuidsFromDb can only run inside the Unity Editor.");
    }
#endif

    [Serializable]
    class Db
    {
        public List<BundleInfo> bundles = new List<BundleInfo>();
    }

    [Serializable]
    class BundleInfo
    {
        public List<AssetInfo> assets = new List<AssetInfo>();
    }

    [Serializable]
    class AssetInfo
    {
        public string guid;
        public string objectName;
    }
}
