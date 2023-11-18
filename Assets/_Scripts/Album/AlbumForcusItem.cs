using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
//using DeadMosquito.AndroidGoodies;
public class AlbumForcusItem : MonoBehaviour
{
    [SerializeField] Image focusImage;
    [SerializeField] Button shareButton;
    [SerializeField] Button deleteButton;
    [SerializeField] Button downloadButton;
    [SerializeField] Button ChangeLockScreenButton;
    [SerializeField] Button ChangeHomeScreenButton;
    [SerializeField] Button ChangeBothScreenButton;
    Texture2D wallpaperTexture;
    bool isProcessingSetWallpaper;
    bool isProcessingSetWallpaperWaiting;
    bool isBackButtonClickSetWallpaper;

    private Sprite sprite;


    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .2f);
    }

    private void OnDisable()
    {
        shareButton.onClick.AddListener(
            delegate
            {   
                ClickShareButtonOfShareCanvas();
            });
        deleteButton.onClick.AddListener(
           delegate
           {

           });
        downloadButton.onClick.AddListener(
           delegate
           {

           });
        ChangeLockScreenButton.onClick.AddListener(
           delegate
           {
               //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);

               isProcessingSetWallpaperWaiting = false;
               //FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbWALLPAPER_NameBG, GameManager.Instance.completeUI.bgCurrentObject.name.Replace("(Clone)", "")));

               CoroutineEverywhere.Instance.WaitingWithSecond(.1f, () =>
               {
                   //AGWallpaperManager.SetWallpaper(wallpaperTexture, which: AGWallpaperManager.WallpaperType.Lock);
                   //SetWallpaper(OptionSetWallPaper.LockScreen);
               });
           });
        ChangeHomeScreenButton.onClick.AddListener(
           delegate
           {
               //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);

               isProcessingSetWallpaperWaiting = false;
               //FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbWALLPAPER_NameBG, GameManager.Instance.completeUI.bgCurrentObject.name.Replace("(Clone)", "")));
               CoroutineEverywhere.Instance.WaitingWithSecond(.1f, () =>
               {
                   //AGWallpaperManager.SetWallpaper(wallpaperTexture, which: AGWallpaperManager.WallpaperType.System);
                   //SetWallpaper(OptionSetWallPaper.HomeScreen);
               });
           });
        ChangeBothScreenButton.onClick.AddListener(
           delegate
           {
               //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);

               isProcessingSetWallpaperWaiting = false;
               //FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbWALLPAPER_NameBG, GameManager.Instance.completeUI.bgCurrentObject.name.Replace("(Clone)", "")));
               CoroutineEverywhere.Instance.WaitingWithSecond(.1f, () =>
               {
                   //AGWallpaperManager.Clear();
                   //AGWallpaperManager.SetWallpaper(wallpaperTexture, which: AGWallpaperManager.WallpaperType.Lock);
                   //AGWallpaperManager.SetWallpaper(wallpaperTexture, which: AGWallpaperManager.WallpaperType.System);
                   //SetWallpaper(OptionSetWallPaper.Both);
               });
           });
    }

    void Start()
    {
        
    }

    public void SetData(Sprite sprite)
    {
        this.sprite = sprite;
        focusImage.sprite = this.sprite;
    }

    public void ClickShareButtonOfShareCanvas()
    {
        //ShareButtonFinal.gameObject.SetActive(false);
        //BackButton.gameObject.SetActive(false);
        StartCoroutine(TakeScreenshotAndShare());
    }
    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("See My Character").SetText("Game is so fun").SetUrl(DataGame.appPackageName)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    public void ClickBackButton()
    {
        gameObject.SetActive(false);
        //FinalCanvas.SetActive(true);
    }
}
