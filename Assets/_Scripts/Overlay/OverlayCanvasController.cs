using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverlayCanvasController : MonoBehaviour
{
   // public static OverlayCanvasController THIS;

    public GameObject buttonPanel;
    [SerializeField] Button settingButton;
    public  Button iapButton;
    [SerializeField] SettingPanelController settingPanel;
    public GameObject RateUsCanvas;
    //public ComonPopUpController ComonPopUpController;
    private void Awake()
    {
        
    }

    void Start()
    {
        settingButton.onClick.AddListener(()=> {
            settingPanel.gameObject.SetActive(true);
            settingPanel.UISetting();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
