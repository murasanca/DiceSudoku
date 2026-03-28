#if UNITY_EDITOR
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor Unused
/// </summary>
public class EU:EditorWindow
{
    private DefaultAsset defaultAsset;
    private bool includeSubfolders=true;
    private Vector2 scroll;
    private readonly List<string>
        folderPaths=new(),
        unusedPaths=new();

    [MenuItem("Tools/Unused Asset(s)")]
    private static void Open()=>GetWindow<EU>("Unused Asset(s)");

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Unused Asset(s)",EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Folder",GUILayout.Width(64));
        defaultAsset=(DefaultAsset)EditorGUILayout.ObjectField(defaultAsset,typeof(DefaultAsset),false);
        Color previousBackground=GUI.backgroundColor;
        GUI.backgroundColor=Color.yellow;
        if(GUILayout.Button("Clear",GUILayout.Width(64)))
        {
            defaultAsset=null;
            unusedPaths.Clear();
            folderPaths.Clear();
        }
        GUI.backgroundColor=previousBackground;
        EditorGUILayout.EndHorizontal();
        includeSubfolders=EditorGUILayout.ToggleLeft("Include Subfolders",includeSubfolders);
        EditorGUILayout.Space();
        previousBackground=GUI.backgroundColor;
        GUI.backgroundColor=Color.green;
        if(GUILayout.Button("Scan Project for Unused Assets",GUILayout.Height(32)))
            ScanProject();
        GUI.backgroundColor=previousBackground;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(string.Concat("Unused Asset(s): ",unusedPaths.Count.ToString()),EditorStyles.boldLabel);
        scroll=EditorGUILayout.BeginScrollView(scroll);
        for(int i=0;i<unusedPaths.Count;++i)
        {
            int deleteIndex=-1;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(unusedPaths[i]);
            if(GUILayout.Button("Ping",GUILayout.Width(50)))
                EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(unusedPaths[i]));
            if(GUILayout.Button("Select",GUILayout.Width(60)))
                Selection.activeObject=AssetDatabase.LoadMainAssetAtPath(unusedPaths[i]);
            GUI.backgroundColor=Color.red;
            if(GUILayout.Button("Delete",GUILayout.Width(64))&&EditorUtility.DisplayDialog("Confirm Delete",string.Concat("Delete asset:\n",unusedPaths[i],"?\nThis cannot be undone."),"Delete","Cancel"))
                if(AssetDatabase.DeleteAsset(unusedPaths[i]))
                {
                    AssetDatabase.Refresh();
                    deleteIndex=i;
                }
                else
                    EditorUtility.DisplayDialog("Failed","Could not delete asset. It may be locked or in use.","OK");
            GUI.backgroundColor=previousBackground;
            EditorGUILayout.EndHorizontal();
            if(-1!=deleteIndex)
            {
                unusedPaths.RemoveAt(deleteIndex);
                --i;
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Select a Folder in Project. The tool scans project assets and lists selected-folder assets with no references.",MessageType.Info);
    }

    private void ScanProject()
    {
        unusedPaths.Clear();
        folderPaths.Clear();
        if(defaultAsset==null)
        {
            EditorUtility.DisplayDialog("Error","Please assign a Folder from the Project window.","OK");
            return;
        }
        string folderPath=AssetDatabase.GetAssetPath(defaultAsset);
        if(!AssetDatabase.IsValidFolder(folderPath))
        {
            EditorUtility.DisplayDialog("Error","Selected object is not a folder. Drag a folder from the Project window.","OK");
            return;
        }
        string[]folderGuids=AssetDatabase.FindAssets("",new[]{folderPath});
        foreach(string g in folderGuids)
        {
            string p=AssetDatabase.GUIDToAssetPath(g);
            if(AssetDatabase.IsValidFolder(p))
                continue;
            folderPaths.Add(p);
        }
        if(0==folderPaths.Count)
        {
            EditorUtility.DisplayDialog("Info","No assets found in the selected folder.","OK");
            return;
        }
        HashSet<string>used=new();
        HashSet<string>folderSet=new(folderPaths);
        string[]allGuids=AssetDatabase.FindAssets("");
        int total=allGuids.Length;
        try
        {
            for(int i=0;i<total;++i)
            {
                if(EditorUtility.DisplayCancelableProgressBar("Scanning Project",string.Concat("Checking asset ",(1+i).ToString(),"/",total.ToString()),(float)i/total))
                {
                    EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("Canceled","Scan was canceled.","OK");
                    return;
                }
                string refPath=AssetDatabase.GUIDToAssetPath(allGuids[i]);
                if(refPath.EndsWith(".meta"))
                    continue;
                string[]deps;
                try
                {
                    deps=AssetDatabase.GetDependencies(refPath,true);
                }
                catch
                {
                    deps=new string[0];
                }
                foreach(string d in deps)
                    if(folderSet.Contains(d)&&d!=refPath)
                        used.Add(d);
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
        foreach(string p in folderPaths)
            if(!used.Contains(p))
                unusedPaths.Add(p);
        unusedPaths.Sort();
        AssetDatabase.Refresh();
    }
}
#endif