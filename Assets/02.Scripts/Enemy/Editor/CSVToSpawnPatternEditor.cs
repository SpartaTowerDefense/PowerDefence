using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CSVToSpawnPatternEditor : EditorWindow
{
    private TextAsset csvFile;

    [MenuItem("Tools/Import Spawn Pattern from CSV")]
    public static void ShowWindow()
    {
        GetWindow<CSVToSpawnPatternEditor>("CSV Importer");
    }

    void OnGUI()
    {
        csvFile = (TextAsset)EditorGUILayout.ObjectField("CSV File", csvFile, typeof(TextAsset), false);

        if (GUILayout.Button("Generate SpawnPatternData"))
        {
            if (csvFile != null)
            {
                CreateSpawnPatternFromCSV(csvFile);
            }
        }
    }

    private void CreateSpawnPatternFromCSV(TextAsset csv)
    {
        string[] lines = csv.text.Split('\n');
        List<SpawnInfo> spawnInfos = new();

        for (int i = 1; i < lines.Length; i++) // 첫 줄은 헤더
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] parts = line.Split(',');

            if (parts.Length >= 2 &&
                int.TryParse(parts[0], out int type) &&
                float.TryParse(parts[1], out float delay))
            {
                SpawnInfo info = new SpawnInfo
                {
                    enemyType = type,
                    delay = delay
                };
                spawnInfos.Add(info);
            }
        }

        SpawnPatternData asset = ScriptableObject.CreateInstance<SpawnPatternData>();
        asset.spawnSequence = spawnInfos.ToArray();

        string path = EditorUtility.SaveFilePanelInProject("Save SpawnPatternData", "NewSpawnPatternData", "asset", "Save SO");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            Debug.Log($"SpawnPatternData saved to {path}");
        }
    }
}