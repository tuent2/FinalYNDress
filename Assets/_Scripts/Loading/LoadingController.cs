 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class LoadingController : MonoBehaviour
{
    public static LoadingController THIS;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image LoadingImageObject;

    private void Awake()
    {

        if (THIS != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        
    }



    public void LoadingAction(System.Action onCompleteAction)
    {
        gameObject.SetActive(true);
        LoadingImageObject.fillAmount = 0;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(0.8f, 1f).OnComplete(() =>
        {
            canvasGroup.DOFade(1.0f, 0.5f);
            LoadingImageObject.DOFillAmount(1f, 1f).OnComplete(() =>
            {
                
                onCompleteAction.Invoke();

                StartCoroutine(WaitToTurnOffLoading());
            }); ;

        });







        }

    IEnumerator WaitToTurnOffLoading()
    {
        yield return  new WaitForSeconds(0.75f);
        gameObject.SetActive(false);
    }

    
   
}

