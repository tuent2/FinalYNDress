using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDancingOnStage : MonoBehaviour
{
    [ConditionalHide] public bool isMonsterOnStage;
    [ConditionalHide] public bool isActiveEffectDancing;
    [SerializeField] GameObject effectObject;
    [SerializeField] Collider2D colliderDetect;
    [SerializeField] ContactFilter2D contactFilterDetectStage;
    Collider2D[] colliderDetectStages = new Collider2D[1];
    StageInLandAlbum stageInLandAlbum;
    public void SetActive()
    {
        effectObject.SetActive(true);
    }
    public void SetInActive()
    {
        effectObject.SetActive(false);
    }
    private void OnEnable()
    {
        //TimingManager.Instance.callbackNextSecond += NextSecond;
        InvokeRepeating(nameof(DetectStage), .1f, .1f);
    }

    private void OnDisable()
    {
        SetInActive();

        //TimingManager.Instance.callbackNextSecond -= NextSecond;
        CancelInvoke(nameof(DetectStage));
    }
    void DetectStage()
    {
        bool isActiveEffectDancing = false;
        int countMonsterOnStage = Physics2D.OverlapCollider(colliderDetect, contactFilterDetectStage, colliderDetectStages);
        if (countMonsterOnStage >= 1)
        {
            isMonsterOnStage = true;
            stageInLandAlbum = colliderDetectStages[0].GetComponentInParent<StageInLandAlbum>();
            if (stageInLandAlbum)
                isActiveEffectDancing = stageInLandAlbum.isMonstersFullStage;
        }
        else
        {
            isMonsterOnStage = false;

            stageInLandAlbum = null;
            isActiveEffectDancing = false;
        }
        if (isActiveEffectDancing)
        {
            SetActive();
        }
        else
        {
            SetInActive();
        }
    }
}
