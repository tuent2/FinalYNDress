using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController THIS { set; get; }
    public AudioSource audioSource;
    [Space]
    public AudioClip BackGroundCompletedClip;
    [SerializeField] [Range(0f, 1f)] float BackGroundCompletedVolume = 1f;
    [Space]
    public AudioClip InGameBGClip;
    [SerializeField] [Range(0f, 1f)] float InGameBGVolume = 1f;
    [Space]
    public AudioClip YesClip;
    [SerializeField] [Range(0f, 1f)] float YesVolume = 1f;
    [Space]
    public AudioClip NoClip;
    [SerializeField] [Range(0f, 1f)] float NoVolume = 1f;
    [Space]
    public AudioClip YesOrNoClip;
    [SerializeField] [Range(0f, 1f)] float YesOrNoValume = 1f;
    [Space]
    public AudioClip TapClip;
    [SerializeField] [Range(0f, 1f)] float TapValume = 1f;
    [Space]
    public AudioClip DressClip;
    [SerializeField] [Range(0f, 1f)] float DressValume = 1f;
    //[Space]
    //public AudioClip[] NotSelectedClip;
    //[SerializeField] [Range(0f, 1f)] float NotSelectedValume = 1f;

    private void Awake()
    {
        if (THIS != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShutDownTheCurrentBGM()
    {
        audioSource.Stop();
    }

    public void PlayBackGroundCompletedClip()
    {
        if (PlayerPrefs.GetInt("BGM", 1) == 1)
        {
            //int currentTrackIndex = Random.Range(0, BackGroundCompletedClip.Length);
            //audioSource.Stop();
            //audioSource.clip = BackGroundCompletedClip[currentTrackIndex];
            //audioSource.volume = BackGroundCompletedVolume;
            //audioSource.loop = true;
            //audioSource.Play();

            audioSource.Stop();
            audioSource.clip = BackGroundCompletedClip;
            audioSource.volume = BackGroundCompletedVolume;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    public void PlayInGameBGClip()
    {
        if (PlayerPrefs.GetInt("BGM", 1) == 1)
        {
            audioSource.Stop();
            audioSource.clip = InGameBGClip;
            audioSource.volume = InGameBGVolume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }


    public void PlayYesClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            playClip(YesClip, YesVolume);
        }
    }


    public void PlayNoClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            playClip(NoClip, NoVolume);
        }
    }



    public void PlayYesOrNoClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            playClip(YesOrNoClip, YesOrNoValume);
        }
    }

    public void PlayTabClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            playClip(TapClip, TapValume);


        }
    }

    public void PlayDressClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            playClip(DressClip, DressValume);
        }
    }

    //public void PlayNotSelectedClip()
    //{
    //    if (PlayerPrefs.GetInt("SFX", 1) == 1)
    //    {
    //        int currentTrackIndex = Random.Range(0, NotSelectedClip.Length);
    //        playClip(NotSelectedClip[currentTrackIndex], NotSelectedValume);
    //    }
    //}



    public void playClip(AudioClip clip, float valume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, valume);

        }
    }
}
