using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor
{
    public class GameTools
    {
        [MenuItem("Tools/MoveSceneSpritesToTmpFolder")]
        public static void MoveSceneSpritesToTmpFolder()
        {
            var sprites = Resources.FindObjectsOfTypeAll(typeof(Sprite)).Cast<Sprite>().ToArray();

            var path = Application.dataPath + "/tmp";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            HashSet<Sprite> hash = new HashSet<Sprite>();

            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] != null && !hash.Contains(sprites[i]))
                {
                    hash.Add(sprites[i]);                    
                    var file = AssetDatabase.GetAssetPath(sprites[i]);
                    var dot = file.LastIndexOf(".");
                    var slath = file.LastIndexOf("/");
                    var ext = file.Substring(dot, file.Length - dot);                    
                    var name = file.Substring(slath, file.Length - slath - ext.Length);  
                    var newpath = "Assets/tmp" + name + ext;
                    int x = 1;

                    if (file == newpath) continue;
                    if (file.IndexOf("/Resources/", StringComparison.Ordinal) >= 0) continue;

                    while (File.Exists(newpath))
                    {
                        newpath = "Assets/tmp" + name + "_" + x + ext;
                        x++;
                    }

                    Debug.Log("MoveFile: " + file + " To: " + newpath);
                    AssetDatabase.MoveAsset(file, newpath);
                }
            }
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/MoveSceneTmpSpritesToSelectFolder")]
        public static void MoveSceneTmpSpritesToSelectFolder()
        {
            if (Selection.activeObject == null)
            {
                Debug.LogError("Folder not select");
                return;
            }           

            var root = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (!Directory.Exists(root))
            {
                Debug.LogError("Directory ["+ root+"] not found");
                return;
            }

            Debug.Log("Selection folder: " + root);

            var sprites = Resources.FindObjectsOfTypeAll(typeof(Sprite)).Cast<Sprite>().ToArray();

            var path = Application.dataPath + "/tmp/common";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string common = "Assets/tmp/common";

            HashSet<Sprite> hash = new HashSet<Sprite>();

            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] != null && !hash.Contains(sprites[i]))
                {
                    hash.Add(sprites[i]);
                    var file = AssetDatabase.GetAssetPath(sprites[i]);
                    if (file.IndexOf("/Resources/", StringComparison.Ordinal) >= 0) continue;

                    var dot = file.LastIndexOf(".");
                    var slath = file.LastIndexOf("/");
                    var ext = file.Substring(dot, file.Length - dot);
                    var name = file.Substring(slath, file.Length - slath - ext.Length);
                    var newpath = root;
                    var newpathfile = newpath + name + ext;

                    if (file == newpathfile) continue;
                    if (file != "Assets/tmp" + name + ext)
                    {                        
                        newpath = common;
                        newpathfile = newpath + name + ext;

                        if (file == newpathfile) continue;
                        Debug.LogError("File [" + file + "] is Common");
                    }                    

                    int x = 1;
                    
                    while (File.Exists(newpathfile))
                    {
                        newpathfile = newpath + name + "_" + x + ext;
                        x++;
                    }

                    Debug.Log("MoveFile: " + file + " To: " + newpathfile);
                    AssetDatabase.MoveAsset(file, newpathfile);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}