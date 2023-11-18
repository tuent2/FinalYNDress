using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverlayCanvasController : MonoBehaviour
{
    public GameObject buttonPanel;
    [SerializeField] Button settingButton;
    public  Button iapButton;
    [SerializeField] SettingPanelController settingPanel;
    void Start()
    {
        settingButton.onClick.AddListener(()=> {
            settingPanel.gameObject.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
