#if (UNITY_EDITOR) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


[InitializeOnLoad]
public class FolderSelectionHelper
{
    public static Object lastSelected;
    public static Object selectThis;

    public static bool inspectorLocked;

    static FolderSelectionHelper()
    {
        Selection.selectionChanged += OnSelectionChanged;
        EditorApplication.update += OnUpdate;

        //Restore folder lock status
        inspectorLocked = EditorPrefs.GetBool("FolderSelectionLocked", false);
    }

    static void OnSelectionChanged()
    {
        if (Selection.activeObject != lastSelected && !IsFolder(Selection.activeObject))
        {
            lastSelected = Selection.activeObject;
            UnLockFolders();
        }

        if (IsFolder(Selection.activeObject))
        {
            selectThis = lastSelected;
        }
    }

    //We have to do selecting in the next editor update because Unity does not allow selecting another object in the same editor update
    static void OnUpdate()
    {
        if (ActiveEditorTracker.sharedTracker.isLocked) return;
        if (selectThis == null) return;

        Selection.activeObject = selectThis;

        LockFolders();

        lastSelected = selectThis;
        selectThis = null;
    }

    private static void LockFolders()
    {
        ActiveEditorTracker.sharedTracker.isLocked = true;
        inspectorLocked = true;
        //We store the state so that if we compile or leave the editor while the folders are locked then the state is kept
        EditorPrefs.SetBool("FolderSelectionLocked", true);
    }

    private static void UnLockFolders()
    {
        if (!inspectorLocked) return;

        ActiveEditorTracker.sharedTracker.isLocked = false;
        inspectorLocked = false;
        EditorPrefs.SetBool("FolderSelectionLocked", false);
    }

    private static bool IsFolder(Object obj)
    {
        if (obj == null)
            return false;

        string path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
        return path.Length > 0 && Directory.Exists(path);
    }

}
#endif