using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class SharePanelController : MonoBehaviour
{
    public Button ShareButtonFinal;
    public Button BackButton;
    public GameObject FinalCanvas;
    private Sprite shareSprite;
    [SerializeField] Image FinalShareImage;
    public void UpdateImageShareAndSpirte(Sprite sprite)
    {
        shareSprite = sprite;
        FinalShareImage.sprite = shareSprite;
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
            .SetSubject("See My Monster").SetText("Game is so fun").SetUrl("https://play.google.com/store/apps/details?id=com.dressup.makeup.leftright.monstermakeover.monsterdressup.yesorno")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    public void ClickBackButton()
    {
        gameObject.SetActive(false);
        FinalCanvas.SetActive(true);
    }
}
