using UnityEngine;

[CreateAssetMenu(fileName = "SoundPlaylist", menuName = "Tools/Create playlist")]
public class SoundPlaylist: ScriptableObject
{
    public SoundSettings[] Sounds;
}


[System.Serializable]
public class SoundSettings
{
    public enum SoundFileType
    {
        Recources,
        File,
        Remote
    }

    public string Name;
    public SoundFileType Type;
}