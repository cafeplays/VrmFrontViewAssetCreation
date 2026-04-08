using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class BundleExportPanel : EditorWindow
{
    public enum BundleType
    {
        Backdrop,
        Avatar
    }

    private GameObject targetPrefab;
    private BundleType exportType = BundleType.Backdrop;

    [MenuItem("VrmFrontView/Bundle Exporter Panel")]
    public static void ShowWindow()
    {
        BundleExportPanel window = GetWindow<BundleExportPanel>("Bundle Exporter");
        window.minSize = new Vector2(350, 200);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Streamable Asset Exporter", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 1. Prefab Selection Field
        targetPrefab = (GameObject)EditorGUILayout.ObjectField(
            "Target Prefab", 
            targetPrefab, 
            typeof(GameObject), 
            false
        );

        EditorGUILayout.Space();

        // 2. Export Type Dropdown
        exportType = (BundleType)EditorGUILayout.EnumPopup("Export Type", exportType);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // 3. Export Button
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Export Bundle", GUILayout.Height(40)))
        {
            ValidateAndExport();
        }
        GUI.backgroundColor = Color.white;

        // Helpful tips area updated with new extension
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            ".backdrop files: Internal key 'stageprop'\n" +
            ".vavatar files: Internal key 'avatar' (Requires VRM)", 
            MessageType.Info
        );
    }

    private void ValidateAndExport()
    {
        if (targetPrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a Prefab to export.", "OK");
            return;
        }

        if (PrefabUtility.GetPrefabAssetType(targetPrefab) == PrefabAssetType.NotAPrefab)
        {
            EditorUtility.DisplayDialog("Error", "The assigned object is not a valid Prefab Asset.", "OK");
            return;
        }

        var components = targetPrefab.GetComponentsInChildren<Component>(true);
        foreach (var c in components)
        {
            if (c == null)
            {
                EditorUtility.DisplayDialog("Build Aborted", 
                    "The Prefab has a 'Missing Script' component!\n" +
                    "Unity cannot build AssetBundles with missing scripts.", "OK");
                return;
            }
        }

        if (exportType == BundleType.Avatar)
        {
            bool hasVRM = false;
            foreach (var c in components)
            {
                if (c != null && (c.GetType().Name.Contains("VRM") || c.GetType().Name.Contains("Vrm")))
                {
                    hasVRM = true;
                    break;
                }
            }

            if (!hasVRM)
            {
                EditorUtility.DisplayDialog("Invalid Avatar", "No VRM/Vrm component found in hierarchy.", "OK");
                return;
            }
        }

        // --- UPDATED EXPORT EXTENSION LOGIC ---
        string extension = exportType == BundleType.Avatar ? "vavatar" : "backdrop";
        string internalKey = exportType == BundleType.Avatar ? "avatar" : "stageprop";
        
        string defaultFileName = targetPrefab.name.ToLower() + "." + extension;
        string savePath = EditorUtility.SaveFilePanel($"Export {exportType}", "", defaultFileName, extension);

        if (string.IsNullOrEmpty(savePath)) return;

        ExecuteBuild(savePath, internalKey);
    }

    private void ExecuteBuild(string savePath, string internalKey)
    {
        string directory = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        string assetPath = AssetDatabase.GetAssetPath(targetPrefab);

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = Path.GetFileName(savePath);
        buildMap[0].assetNames = new string[] { assetPath };
        buildMap[0].addressableNames = new string[] { internalKey };

        try 
        {
            Debug.Log($"Starting {exportType} export...");
            
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(
                directory, 
                buildMap, 
                BuildAssetBundleOptions.None, 
                EditorUserBuildSettings.activeBuildTarget
            );

            if (manifest != null)
            {
                string manifestPath = savePath + ".manifest";
                if (File.Exists(manifestPath)) File.Delete(manifestPath);
                
                string folderManifest = Path.Combine(directory, Path.GetFileName(directory) + ".manifest");
                if (File.Exists(folderManifest)) File.Delete(folderManifest);

                AssetDatabase.Refresh();
                
                if (File.Exists(savePath))
                {
                    EditorUtility.RevealInFinder(savePath);
                    Debug.Log($"SUCCESS: {exportType} exported to {savePath}");
                }
            }
            else
            {
                Debug.LogError("BuildPipeline failed. Check Unity Console for internal errors.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Build Crashed: {e.Message}");
        }
    }
}