using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour, IPlayerEvents
{
    private static PlayerController instance;
    public static MonoBehaviour MonoBehaviour { get { return instance; } }
    public SoundPlaylist SoundPlaylist;

    private Playlist Playlist;
    private PlayerControls Controls;

    private List<ISound> soundList = new List<ISound>();
    private List<ISound> soundsavelist = new List<ISound>();
    private AudioSource source;
    private ISoundEvents soundEvents;
    private int index = -1;

    void Awake()
    {
        instance = this;

        var obj = FindObjectOfType(typeof (Playlist));
        if (obj == null) Debug.LogError("Playlist not found");
        else Playlist = (obj as Playlist);

        obj = FindObjectOfType(typeof(PlayerControls));
        if (obj == null) Debug.LogError("PlayerControls not found");
        else Controls = (obj as PlayerControls);

        Controls.BtnPlay.onClick.AddListener(SoundPlay);
        Controls.BtnStop.onClick.AddListener(SoundPause);
        Controls.BtnPlayAll.onClick.AddListener(AllSoundPlay);

        source = gameObject.AddComponent<AudioSource>();
    }

    public void Start()
    {
        foreach (var sound in SoundPlaylist.Sounds)
        {
            var bs = CreateSound(sound);
            soundList.Add(bs);
        }

        if (Playlist != null) Playlist.DisplayList(soundList, this);            
    }



    private BaseSound CreateSound(SoundSettings sound)
    {
        switch (sound.Type)
        {
            case SoundSettings.SoundFileType.Recources:
                return new ResourceSound(sound.Name);
            case SoundSettings.SoundFileType.File:
                return new FileSound(sound.Name);
            case SoundSettings.SoundFileType.Remote:
                return new RemoteSound(sound.Name);                
        }
        return null;
    }

    public void OnPlaySound(ISound sound, ISoundEvents evetns)
    {
        Playlist.StopAllPlaying();
        soundsavelist.Add(sound);
        if (source.clip != null && source.clip == sound.GetClip() && source.time < source.clip.length)
        {
            source.UnPause();
        }
        else
        {
            source.clip = sound.GetClip();
            source.Stop();
            source.Play();
        }
        soundEvents = evetns;
        soundEvents.PlayingProgress(source.time / source.clip.length);
        Controls.PlaySound();
    }

    public void OnPauseSound(ISound sound)
    {
        source.Pause();
        Controls.StopSound();
    }

    private void SoundPause()
    {
        if (soundEvents != null)
        {
            soundEvents.PausePlaying();
        }
    }

    private void SoundPlay()
    {
        if (soundEvents != null)
        {
            soundEvents.ResumePlaying();                
        }
    }

    private void AllSoundPlay()
    {
        if (soundsavelist.Count != 0)
        {
            index = 0;
            PlaySound(soundsavelist[index]);
        }
    }

    private void PlaySound(ISound sound)
    {
        var se = Playlist.GetSoundEvents(sound);
        se.ResumePlaying();
    }

    void Update()
    {
        if (soundEvents != null && Application.isFocused)
        {
            if (source.isPlaying)
            {
                soundEvents.PlayingProgress(source.time/source.clip.length);
            }
            else
            {
                Controls.StopSound();

                if (source.time <= 0)
                {
                    soundEvents.StopPlaying();
                    if (index >= 0)
                    {
                        index++;
                        if (index >= soundsavelist.Count) index = 0;
                        PlaySound(soundsavelist[index]);
                    }
                } else soundEvents.PausePlaying();
            }
        }

    }
}   