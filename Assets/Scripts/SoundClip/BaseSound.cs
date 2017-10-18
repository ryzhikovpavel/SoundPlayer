using UnityEngine;

public abstract class BaseSound: ISound
{
    protected AudioClip clip;

    public float Length
    {
        get
        {
            if (IsReady) return  clip.length;
            return 0f;
        }            
    }

    public string Name
    {
        get
        {
            if (IsReady) return clip.name;
            return "Unknown";
        }
    }

    public AudioClip GetClip()
    {
        return clip;
    }

    public bool IsReady { get { return clip != null; } }
    public abstract float Progress { get; }
}