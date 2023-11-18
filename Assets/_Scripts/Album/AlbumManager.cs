using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AlbumManager : MonoBehaviour
{
    public string screenshotFolder = "Screenshots";
    public string screenshotName = "screenshot.png";

    //private string screenshotPath;
    public int NumberOfScreenShot;
    private void Start()
    {
        NumberOfScreenShot = PlayerPrefs.GetInt(DataGame.screenShotNumber, 0);
        //screenshotPath = Path.Combine(Application.dataPath, screenshotFolder, screenshotName);
        
    }

    private void Update()
    {
        
    }

    private IEnumerator CaptureScreenAndSetImage()
    {
        yield return new WaitForEndOfFrame();

        int halfWidth = Screen.width / 2;
        int halfHeight = Screen.height / 2;

        int startX = halfWidth - 380;
        int startY = halfHeight - 150;

        Texture2D screenshotTexture = new Texture2D(760, 680, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(startX, startY, 760, 680), 0, 0);
        screenshotTexture.Apply();

        byte[] bytes = screenshotTexture.EncodeToPNG();
        File.WriteAllBytes(DataGame.pathScreenShotDataSave + NumberOfScreenShot, bytes);
        NumberOfScreenShot++;
        PlayerPrefs.SetInt(DataGame.screenShotNumber, NumberOfScreenShot);
        //Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, 760, 680), new Vector2(0.5f, 0.5f));
        //ScreenShotImage.sprite = screenshotSprite;
        //FinalScreenShotImage.sprite = screenshotSprite;
        Destroy(screenshotTexture);
    }

   
}
