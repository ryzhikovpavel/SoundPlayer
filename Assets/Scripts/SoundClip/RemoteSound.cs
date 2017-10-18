using UnityEngine;

public class RemoteSound: BaseSoundLoader
{
    public RemoteSound(string url)
    {
        if (url.Substring(0, 7).ToLower() == "http://" || url.Substring(0, 8).ToLower() == "https://")
            PlayerController.MonoBehaviour.StartCoroutine(LoadSound(url));
        else 
            Debug.Log("Invalid URL");
    }       
}