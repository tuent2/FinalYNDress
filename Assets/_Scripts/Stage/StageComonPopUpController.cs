using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageComonPopUpController : MonoBehaviour
{
    [SerializeField] Button CloseButton;
    [SerializeField] Button OkButton;
    void Start()
    {
        CloseButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
        OkButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
