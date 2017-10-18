using System.IO;
using UnityEngine;

public class FileSound: BaseSoundLoader
{
    public FileSound(string file)
    {
        if (!File.Exists(file))
        {
            Debug.Log("File [" + file + "] not exists");
            return;
        }
            
        file = Path.GetFullPath(file);
        PlayerController.MonoBehaviour.StartCoroutine(LoadSound("file://" + file));
    }
}