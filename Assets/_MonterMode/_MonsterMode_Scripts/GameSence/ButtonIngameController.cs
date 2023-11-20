using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ButtonIngameController : MonoBehaviour
{
    public GameObject SettingPanel;
    public Image BgSetting;
    public bool isHomeScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSettingButton()
    {
        Debug.Log(Screen.width);
        SettingPanel.SetActive(true);
        Vector3 originalPosition = BgSetting.transform.position;
        BgSetting.transform.position = new Vector3(originalPosition.x - Screen.width, originalPosition.y, originalPosition.z);
        BgSetting.transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
}
