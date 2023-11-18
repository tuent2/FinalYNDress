using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
public class AlbumItem : MonoBehaviour
{
    [SerializeField] Image screenShotImage;
    [SerializeField] Button button;
    [SerializeField] AlbumForcusItem AlbumForcusItem;
    private Sprite sprite;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .2f);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        DOTween.Kill(transform);
    }

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            AlbumForcusItem.SetData(sprite);
            AlbumForcusItem.gameObject.SetActive(true);
            
        });
    }

    public void SetData(Sprite sprite)
    {
        this.sprite = sprite;
        screenShotImage.sprite = this.sprite;
    }

    
}
