using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using DG.Tweening;
using NaughtyAttributes;
public class BehindCharacterController : MonoBehaviour
{
    [SerializeField] SpriteRenderer bongbongPrefab;
    public SpriteRenderer itemInSidePrefab;
    [SerializeField] Transform Item;
    [SerializeField] ParticleSystem bongbongBlow;
    [SerializeField] bool isSheep;
    private bool isImageMoving = false;
    
    private SkeletonAnimation skeletonAnimation;
    Skin characterSkin;
    
    private int indexSkinRandom;
    private Transform targetPosition;
    bool isEnd;
    Color bongbongColor;
    Color itemColor;

    
    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
       
    }
    private void Start()
    {
        CheckAndChangeSkin();
        
        //PlayAniGetTheItemTesst();
    }

    private void Update()
    {
        if (bongbongPrefab != null && itemInSidePrefab != null && isImageMoving == false)
        {
            bongbongPrefab.transform.position = new Vector3(Item.position.x, Item.position.y + 0.6f, Item.position.z);
            itemInSidePrefab.transform.position = new Vector3(Item.position.x, Item.position.y + 0.52f, Item.position.z);
            //bongbongBlowEffect.transform.position = new Vector3(Item.position.x, Item.position.y + 1.2f, Item.position.z);
        }
    }

    private void CheckAndChangeSkin()
    {
        if (isSheep)
        {
            var skeleton = skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            characterSkin = new Skin("character-base1");

            float randomValue = Random.Range(0f, 1f);

            if (randomValue > 0.5)
            {
                characterSkin.AddSkin(skeletonData.FindSkin("Sheep_Den"));
            }
            else
            {
                characterSkin.AddSkin(skeletonData.FindSkin("Sheep_Trang"));
            }

            var resultCombinedSkin = new Skin("character-combined");
            resultCombinedSkin.AddSkin(characterSkin);
            skeleton.SetSkin(resultCombinedSkin);
            skeleton.SetSlotsToSetupPose();
        }
        //PlayAnimationCharacter(1, "Lay_Item", false);
        PlayAnimationCharacter(0, "Idle", true);
    }

    public void PlayAnimationCharacter(int index, string animation, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, animation, isLoop);
    }

    public void PlayAnimationCharacter(int index, string animation, bool isLoop, float delay)
    {
        skeletonAnimation.AnimationState.AddAnimation(index, animation, isLoop, delay);
    }


    private float fadeInDuration = 0.4f;
    private float startAlpha = 0f;
    private float targetAlpha = 1f;

   
    [Button("LayItem")]
    private IEnumerator PlayAniGetTheItem()
    {
        //yield return null;
        //PlayAnimationCharacter(1, "Lay_Item", false);
        if (!isSheep)
        {
            skeletonAnimation.state.SetAnimation(1, "Lay_Item", false).Complete += delegate {
                skeletonAnimation.state.AddAnimation(0, "Idle", true, 0);
                skeletonAnimation.Initialize(true);
            };
        
            
        }
        else
        {
            PlayAnimationCharacter(1, "Lay_Item", false);
        }
        SetObjectAlpha(0f);

        //Debug.Log("1");
        //Debug.unityLogger.logEnabled = true;
        yield return new WaitForSeconds(0.3f);

        //Debug.Log("2");
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeInDuration);
            SetObjectAlpha(alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetObjectAlpha(targetAlpha);
        SoundController.THIS.PlayYesOrNoClip();
    }

    public void SetObjectAlpha(float alpha)
    {
        bongbongColor = bongbongPrefab.color;
        bongbongColor.a = alpha;
        bongbongPrefab.color = bongbongColor;

        itemColor = itemInSidePrefab.color;
        itemColor.a = alpha;
        itemInSidePrefab.color = itemColor;
        
    }


    
    public void PlayAniAndChangeSprite(List<Sprite> spriteToGen)
    {
        StartCoroutine(PlayAniGetTheItem());
        indexSkinRandom = Random.Range(0, spriteToGen.Count);
        itemInSidePrefab.sprite = spriteToGen[indexSkinRandom];
        YesNoPlayMananger.THIS.skinIndex = indexSkinRandom;
    }

    public void ResetScaleItemAndBuble()
    {
        itemInSidePrefab.GetComponent<Transform>().DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);
        bongbongPrefab.GetComponent<Transform>().DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.3f);
    }

    [Button("bongbongno")]
    public void BlowBuble()
    {

        itemInSidePrefab.transform.DOScale(new Vector3(0.35f, 0.35f, 0.35f), 0.7f).OnComplete(() => {

        });
        bongbongPrefab.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.7f).OnComplete(() => {
            bongbongPrefab.DOFade(0, 0.25f);
            itemInSidePrefab.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f).OnComplete(() =>
            {

            });
            bongbongPrefab.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f).OnComplete(() =>
            {
                bongbongPrefab.transform.DOScale(Vector3.zero, 0f);
                bongbongBlow.Play();
                switch (YesNoPlayMananger.THIS.currentSkin)
                {
                    case SkinToChange.Hair:
                        targetPosition = YesNoPlayMananger.THIS.hairBg.transform;
                        isEnd = false;
                        break;
                    case SkinToChange.Shirt:
                        targetPosition = YesNoPlayMananger.THIS.shirtBg.transform;
                        isEnd = false;
                        break;
                    case SkinToChange.Skirt:
                        targetPosition = YesNoPlayMananger.THIS.skirtBg.transform;
                        isEnd = false;
                        break;
                    case SkinToChange.Acc:
                        targetPosition = YesNoPlayMananger.THIS.accBg.transform;
                        isEnd = false;
                        break;
                    case SkinToChange.Shoe:
                        targetPosition = YesNoPlayMananger.THIS.shoeBg.transform;
                        isEnd = false;
                        break;
                    case SkinToChange.Face:
                        targetPosition = YesNoPlayMananger.THIS.faceBg.transform;
                        isEnd = true;
                        break;
                }
                MoveItemToTarget(targetPosition,isEnd);
            });
        });
    }

    public void MoveItemToTarget(Transform targetPosition,bool isFinalChoice)
    {
        isImageMoving = true;
        
        itemInSidePrefab.transform.DOMove(targetPosition.position, 1f)
            .SetEase(Ease.OutQuad)

            .OnComplete(() =>
            {
                
                if (isFinalChoice == false)
                {
                    
                    YesNoPlayMananger.THIS.SelectedEffect.transform.position = targetPosition.position;
                    YesNoPlayMananger.THIS.SelectedEffect.Play();

                    YesNoPlayMananger.THIS.SkinChangeEffect.transform.position = targetPosition.position;
                    YesNoPlayMananger.THIS.SkinChangeEffect.Play();
                    
                    targetPosition.DOPunchScale(new Vector3(.5f, .5f, 0f), 0.5f).SetEase(Ease.InOutElastic);
                    
                    YesNoPlayMananger.THIS.ChangeIndexOfSkinItem(true);
                    // behindObject.SetActive(true);
                    isImageMoving = false;
                    //Destroy(imageObject);
                    ResetScaleItemAndBuble();
                    YesNoPlayMananger.THIS.RandomSkinInSideBuble();
                    
                    
                    YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0,"Idle", true,1f);
                    PlayAnimationCharacter(0, "Idle", true, 1f);

                    //firstEffect.SetActive(false);

                    StartCoroutine(SetButtonCanSelect());
                        
                }
                else
                {
                    
                    //YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Idle", true, 1f);

                    YesNoPlayMananger.THIS.SelectedEffect.transform.position = targetPosition.position;
                    YesNoPlayMananger.THIS.SelectedEffect.Play();
                    YesNoPlayMananger.THIS.SkinChangeEffect.transform.position = targetPosition.position;
                    YesNoPlayMananger.THIS.SkinChangeEffect.Play();
                    targetPosition.DOPunchScale(new Vector3(.5f, .5f, 0f), 0.5f).SetEase(Ease.InOutElastic);

                    YesNoPlayMananger.THIS.ChangeIndexOfSkinItem(true);
                    isImageMoving = false;



                    YesNoPlayMananger.THIS.FinishChoiceItem();
                }
            });
    }

   
    public void MoveLimitedItemToTarget (Sprite itemSprite)
    {
        PlayAnimationCharacter(1, "Yes", true);
        SetObjectAlpha(0f);

        itemInSidePrefab.sprite = itemSprite;
        itemInSidePrefab.transform.position = UIYesNoPlay.THIS.itemInpopUp.position;
        UIYesNoPlay.THIS.ChangeStateOfButton(false);

        StartCoroutine(IEnumMoveLimitedItemToTarget());
    }

    IEnumerator IEnumMoveLimitedItemToTarget()
    {
        SetObjectAlpha(0f);
        //Debug.Log("2");
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / 1f);
            itemColor = itemInSidePrefab.color;
            itemColor.a = alpha;
            itemInSidePrefab.color = itemColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        itemColor = itemInSidePrefab.color;
        itemColor.a = 1f;
        itemInSidePrefab.color = itemColor;
        //yield return new WaitForSeconds(2f);
        
        itemColor = itemInSidePrefab.color;
        itemColor.a = 1f;
        itemInSidePrefab.color = itemColor;
        UIYesNoPlay.THIS.ChangeStateOfButton(false);
        switch (YesNoPlayMananger.THIS.currentSkin)
        {
            case SkinToChange.Hair:
                targetPosition = YesNoPlayMananger.THIS.hairBg.transform;
                isEnd = false;
                break;
            case SkinToChange.Shirt:
                targetPosition = YesNoPlayMananger.THIS.shirtBg.transform;
                isEnd = false;
                break;
            case SkinToChange.Skirt:
                targetPosition = YesNoPlayMananger.THIS.skirtBg.transform;
                isEnd = false;
                break;
            case SkinToChange.Acc:
                targetPosition = YesNoPlayMananger.THIS.accBg.transform;
                isEnd = false;
                break;
            case SkinToChange.Shoe:
                targetPosition = YesNoPlayMananger.THIS.shoeBg.transform;
                isEnd = false;
                break;
            case SkinToChange.Face:
                targetPosition = YesNoPlayMananger.THIS.faceBg.transform;
                isEnd = true;
                break;
        }
        MoveItemToTarget(targetPosition, isEnd);
        //yield return new WaitForSeconds(2f);
        //PlayAnimationCharacter(1, "Yes", true);
    }

    private IEnumerator SetButtonCanSelect()
    {
        yield return new WaitForSeconds(1.2f);
        UIYesNoPlay.THIS.ChangeStateOfButton(true);
    }
}


