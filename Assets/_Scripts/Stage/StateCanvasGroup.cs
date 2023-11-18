using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCanvasGroup : MonoBehaviour
{
    [SerializeField] bool hasFade = true;
    [SerializeField] float fadeDisable = .4f;
    [SerializeField] Ease easeFade;
    [SerializeField] bool updateMode = false;
    CanvasGroup canvasGroup;
    Animator animator;
    public bool isState;
    Vector2 scaleStart;
    bool isGetRefsed = false;
    float fadeDisableSet;
    private void Awake()
    {
        GetRefs();
    }
    public void GetRefs()
    {
        if (isGetRefsed)
        {
            return;
        }
        isGetRefsed = true;
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
        scaleStart = transform.localScale;
        isState = canvasGroup.alpha == 1f ? true : false;
    }
    public void SetStateWithFadeDefault(bool isEnable, float durationFade = .13f)
    {
        fadeDisableSet = this.fadeDisable;
        SetState(isEnable, durationFade);
    }
    public void SetStateWithFadeDisable(bool isEnable, float durationFade = .13f, float fadeDisable = 0f)
    {
        fadeDisableSet = fadeDisable;
        SetState(isEnable, durationFade);
    }
    public void SetState(bool isEnable, float durationFade = .13f)
    {
        GetRefs();
        if (isEnable)
        {
            if (hasFade)
            {
                canvasGroup.blocksRaycasts = true;
                DOTween.Complete(canvasGroup);
                canvasGroup.DOFade(1, durationFade).SetUpdate(updateMode).SetEase(easeFade).OnComplete(() =>
                {

                });
            }
            else
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            if (animator)
            {
                animator.enabled = true;
            }
        }
        else
        {
            if (hasFade)
            {
                canvasGroup.blocksRaycasts = false;
                DOTween.Complete(canvasGroup);
                canvasGroup.DOFade(fadeDisableSet, durationFade).SetUpdate(updateMode).SetEase(easeFade).OnComplete(() =>
                {

                });
            }
            else
            {
                canvasGroup.alpha = fadeDisableSet;
                canvasGroup.blocksRaycasts = false;
            }
            if (animator)
            {
                animator.enabled = false;
                transform.localScale = scaleStart;
            }
        }
        isState = isEnable;
    }
}
