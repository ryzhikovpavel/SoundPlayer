using UnityEngine;
using UnityEngine.UI;

public class SoundItem: MonoBehaviour, ISoundEvents
{
    public delegate void SoundRefEvent(ISound sound);
    public delegate void SoundEventsEvent(ISound sound, ISoundEvents events);
    public event SoundEventsEvent OnPlay;
    public event SoundRefEvent OnPause;

    public ISound Sound { get; private set; }

    [SerializeField]
    private Text Name;
    [SerializeField]
    private Slider Progress;
    [SerializeField]
    private Button BtnPlay;
    [SerializeField]
    private Button BtnPause;
    [SerializeField]
    private GameObject IcoLoading;
    [SerializeField]
    private GameObject IcoError;
           
    public bool IsPlaying { get; private set; }

    public void Start()
    {
        BtnPause.onClick.AddListener(PausePlaying);
        BtnPlay.onClick.AddListener(ResumePlaying);
    }

    public void Init(ISound sound)
    { 
        Sound = sound;        
        Name.text = Sound.Name;
        if (Sound.IsReady)
        {
            HideAll();
            BtnPlay.gameObject.SetActive(true);
            Progress.gameObject.SetActive(false);
        }
        else
        {
            HideAll();
            IcoLoading.SetActive(true);
            Progress.gameObject.SetActive(true);
        }
    }

    public void ItemProgressUpdate()
    {
        if (Sound.IsReady)
        {
            if (IcoLoading.activeSelf)
            {
                HideAll();
                BtnPlay.gameObject.SetActive(true);
                Progress.gameObject.SetActive(false);
                Name.text = Sound.Name;
            }                
            return;
        }

        if (Sound.Progress < 0)
        {
            enabled = false;
            HideAll();
            IcoError.SetActive(true);
            Progress.gameObject.SetActive(false);
            return;
        }


        if (!IcoLoading.activeSelf)
        {
            HideAll();
            IcoLoading.SetActive(true);
        }

        Progress.value = Sound.Progress;
    }

    private void HideAll()
    {
        IcoLoading.SetActive(false);
        IcoError.SetActive(false);
        BtnPlay.gameObject.SetActive(false);
        BtnPause.gameObject.SetActive(false);            
    }

    public void PlayingProgress(float value)
    {
        Progress.value = value;
    }

    public void StopPlaying()
    {
        PausePlaying();
        Progress.gameObject.SetActive(false);
    }
            
    public void PausePlaying()
    {
        if (OnPause != null) OnPause.Invoke(Sound);
        HideAll();
        BtnPlay.gameObject.SetActive(true);            
    }

    public void ResumePlaying()
    {
        if (OnPlay != null) OnPlay.Invoke(Sound, this);
        HideAll();
        BtnPause.gameObject.SetActive(true);
        Progress.gameObject.SetActive(true);
        Progress.value = 0;
    }
}