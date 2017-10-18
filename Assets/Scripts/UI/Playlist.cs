using System.Collections.Generic;
using UnityEngine;

public class Playlist: MonoBehaviour
{
    public GameObjectPool SoundItem;
    public float ItemSize;
    List<SoundItem> items = new List<SoundItem>();

    public void DisplayList(List<ISound> sounds, IPlayerEvents events)
    {            
        float last = (transform as RectTransform).rect.height / 2f - ItemSize / 2f;
        foreach (ISound sound in sounds)
        {
            var obj = SoundItem.Get();
            PushItem(obj.transform as RectTransform, ref last);
            var item = obj.GetComponent<SoundItem>();
            item.Init(sound);
            item.OnPlay += events.OnPlaySound;
            item.OnPause += events.OnPauseSound;
            items.Add(item);
        }
    }

    public void StopAllPlaying()
    {
        foreach (var item in items)
        {
            item.StopPlaying();
        }
    }

    public ISoundEvents GetSoundEvents(ISound sound)
    {
        foreach (var item in items)
        {
            if (item.Sound == sound) return item;                
        }
        return null;
    }

    void Update()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].enabled)
            {
                items[i].ItemProgressUpdate();    
            }
        }    
    }

    private void PushItem(RectTransform item, ref float last)
    {                        
        item.anchoredPosition = new Vector2(0, last);
        last = last - ItemSize;
    }
}