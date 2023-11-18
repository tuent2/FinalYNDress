using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class UIAlbum : MonoBehaviour
{
    string TestScreenshotFolder = "_Textures/AlbumScreenShot/";
    [SerializeField] AlbumItem albumItemPrefab;
    Coroutine handleUpdateItemCoroutine;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (handleUpdateItemCoroutine != null)
        {
            StopCoroutine(handleUpdateItemCoroutine);
        }
        handleUpdateItemCoroutine = StartCoroutine(IEnumHandleLoadAndDisplayAllScreenShot());
    }

    IEnumerator IEnumHandleLoadAndDisplayAllScreenShot()
    {
        albumItemPrefab.gameObject.SetActive(false);
        string[] screenshotPaths = Directory.GetFiles(Path.Combine(Application.persistentDataPath, DataGame.pathScreenShotDataSave), "*.png");
        //string[] screenshotPaths = Directory.GetFiles(Path.Combine(Application.persistentDataPath, TestScreenshotFolder), "*.png");
        if (screenshotPaths == null)
        {
            yield return new WaitForSeconds(0f);
        }
        else
        {
            Debug.Log(screenshotPaths);
            foreach (string path in screenshotPaths)
            {
                Texture2D texture = LoadTextureFromFile(path);
                CreateScreenshotImage(texture);
                yield return new WaitForSeconds(.2f);
            }
        }
        

        
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
    }

    private void CreateScreenshotImage(Texture2D texture)
    {
        AlbumItem albumItem = null;
        //GameObject imageObject = new GameObject("ScreenshotImage");
        //UnityEngine.UI.Image imageComponent = imageObject.AddComponent<UnityEngine.UI.Image>();
        //imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //imageComponent.rectTransform.sizeDelta = new Vector2(100, 100); 
        
        albumItem = Instantiate(albumItemPrefab, albumItemPrefab.transform.parent);
        albumItem.gameObject.SetActive(true);
        albumItem.SetData(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
        
    }
}
