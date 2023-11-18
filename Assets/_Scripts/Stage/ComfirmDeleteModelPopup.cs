using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ComfirmDeleteModelPopup : MonoBehaviour
{
    [SerializeField] Button ExitButton;
    [SerializeField] Button DeleteButton;
    UnityAction<bool> callbackHided;
    bool isOk;
    void Start()
    {
        ExitButton.onClick.AddListener(() => {
            isOk = false;
            gameObject.SetActive(false);
        });
        DeleteButton.onClick.AddListener(() => {
            isOk = true;
            gameObject.SetActive(false);
        });
    }

    public void ShowWithTextCallback( UnityAction<bool> callbackHided)
    {
        gameObject.SetActive(true);
        this.callbackHided = callbackHided;
       // Debug.Log(122222);
    }

    private void OnDisable()
    {
        callbackHided?.Invoke(isOk);
        callbackHided = null;
    }

    
}
