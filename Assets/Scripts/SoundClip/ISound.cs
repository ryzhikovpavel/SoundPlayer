using UnityEngine;

public interface ISound
{
    float Length { get; }
    string Name { get; }

    AudioClip GetClip();
    bool IsReady { get; }

    float Progress { get; }
}