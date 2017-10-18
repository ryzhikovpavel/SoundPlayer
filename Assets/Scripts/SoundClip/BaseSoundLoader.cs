using System.Collections;
using System.IO;
using UnityEngine;

public abstract class BaseSoundLoader: BaseSound
{
    private float _progress;

    protected IEnumerator LoadSound(string url)
    {
        using (WWW www = new WWW(url))
        {
            _progress = 0f;
            yield return new WaitForFixedUpdate();
            while (!www.isDone)
            {
                _progress = www.progress;
                yield return new WaitForFixedUpdate();
            }                

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                _progress = -1;
                yield break;
            }
                                
            clip = www.GetAudioClip();
            clip.name = Path.GetFileNameWithoutExtension(url);
            _progress = 1;
            Debug.Log("Sound loaded: " + clip.name);
        }
    }

    public override float Progress
    {
        get { return _progress; }
    }
}