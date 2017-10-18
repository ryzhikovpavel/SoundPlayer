using UnityEngine;

public class ResourceSound: BaseSound
{
    public ResourceSound(string path)
    {
        clip = Resources.Load(path, typeof (AudioClip)) as AudioClip;
    }

    public override float Progress
    {
        get { return IsReady ? 1 : 0; }
    }
}