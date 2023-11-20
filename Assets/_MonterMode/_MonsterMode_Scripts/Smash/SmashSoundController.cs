using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashSoundController : MonoBehaviour
{
    public static SmashSoundController THIS { set; get; }
    public AudioSource audioSource;
    [Space]
    public AudioClip[] TouchClip;
    [SerializeField][Range(0f, 1f)] float TouchClipValume = 1f;
    [Space]
    public AudioClip SawClip;
    [SerializeField][Range(0f, 1f)] float SawClipValume = 1f;
    [Space]
    public AudioClip LazeClip;
    [SerializeField][Range(0f, 1f)] float LazeClipValume = 1f;
    [Space]
    public AudioClip KOClip;
    [SerializeField] [Range(0f, 1f)] float KOClipValume = 1f;

    private void Awake()
    {
        THIS = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShutDownTheCurrentBGM()
    {
        audioSource.Stop();
    }
    public void PlayTouchClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            int currentTrackIndex = Random.Range(0, TouchClip.Length);
            playClip(TouchClip[currentTrackIndex], TouchClipValume);
        }
    }

    public void PlaySawClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            audioSource.clip = SawClip;
            audioSource.volume = SawClipValume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayLazerClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            audioSource.Stop();
            audioSource.clip = LazeClip;
            audioSource.volume = LazeClipValume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayKOClip()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            
            playClip(KOClip, KOClipValume);
        }
    }

    public void playClip(AudioClip clip, float valume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, valume);

        }
    }
}
