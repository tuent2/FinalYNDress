 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class LoadingController : MonoBehaviour
{
    public static LoadingController THIS;
    public TextMeshProUGUI LoadingText;
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
        StartCoroutine(HanldeShowTextLoading(0.5f));
        LoadingImageObject.fillAmount = 0;
        

            LoadingImageObject.DOFillAmount(0.3f, 0.3f).OnComplete(() =>
            {
                
                onCompleteAction.Invoke();
                LoadingImageObject.DOFillAmount(1f, 0.8f);
                StartCoroutine(WaitToTurnOffLoading());
            }); ;









        }

    IEnumerator WaitToTurnOffLoading()
    {
        yield return  new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
    }

    IEnumerator HanldeShowTextLoading(float timeChangeText)
    {
        while (LoadingText)
        {
            LoadingText.text = "Loading";
            yield return new WaitForSecondsRealtime(timeChangeText);
            LoadingText.text = "Loading" + ".";
            yield return new WaitForSecondsRealtime(timeChangeText);
            LoadingText.text = "Loading" + "..";
            yield return new WaitForSecondsRealtime(timeChangeText);
            LoadingText.text = "Loading" + "...";
            yield return new WaitForSecondsRealtime(timeChangeText);
        }
    }

}

