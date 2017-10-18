using UnityEngine;
using UnityEngine.UI;

public class PlayerControls: MonoBehaviour
{
    public Button BtnPlay;
    public Button BtnStop;
    public Button BtnPlayAll;

    public void PlaySound()
    {
        BtnPlay.gameObject.SetActive(false);
        BtnStop.gameObject.SetActive(true);
        BtnPlayAll.interactable = false;
    }

    public void StopSound()
    {
        BtnPlay.gameObject.SetActive(true);
        BtnStop.gameObject.SetActive(false);
        BtnPlayAll.interactable = true;
    }
}