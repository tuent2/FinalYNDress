using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using DG.Tweening;
using UnityEngine.UI;
public class BehindMonsterController : MonoBehaviour
{
    public static BehindMonsterController THIS;
    public GameObject bongbongPrefab;
    public GameObject itemInSidePrefab;
    private SkeletonAnimation skeletonAnimation;
    private TrackEntry currentAnimation;
    public Transform Item;
    public int index;
    private GameObject spawnedObject;
    private GameObject itemInSideObject;
    private float fadeInDuration = 0.4f;
    private float startAlpha = 0f;
    private float targetAlpha = 1f;
    private bool isMoveToImage =false;

    private GameObject firstEffect;
    private GameObject bongbongBlowEffect;

    public List<Sprite> BodySpriteClone;
    public List<Sprite> HeadSpriteClone;
    public List<Sprite> AccSpriteClone;
    public List<Sprite> EyesSpriteClone;
    public List<Sprite> MounthSpriteClone;

    //public List<Sprite> BodySprite;
    [Space]
    public List<string> BodySkinClone;
    public List<Sprite> HeadSpriteGenClone;
    public List<Sprite> AccSpriteGenClone;
    public List<Sprite> EyesSpriteGenClone;
    public List<Sprite> MounthSpriteGenClone;


    private void Awake()
    {
        THIS = this;
        // RandombehindMonster();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spawnedObject = Instantiate(bongbongPrefab, Item.position, Quaternion.identity);
        itemInSideObject = Instantiate(itemInSidePrefab, Item.position, Quaternion.identity);
        firstEffect = Instantiate(MonsterModeGameManager.THIS.bongbongBlow[0], Item.position, Quaternion.identity);
        bongbongBlowEffect = Instantiate(MonsterModeGameManager.THIS.bongbongBlow[0], Item.position, Quaternion.identity);

        //firstEffect.SetActive(false);

        BodySpriteClone = new List<Sprite>(MainMonterController.THIS.BodySprite); 
        HeadSpriteClone = new List<Sprite>(MainMonterController.THIS.HeadSprite);
        AccSpriteClone = new List<Sprite>(MainMonterController.THIS.AccSprite);
        EyesSpriteClone = new List<Sprite>(MainMonterController.THIS.EyesSprite);
        MounthSpriteClone = new List<Sprite>(MainMonterController.THIS.MounthSprite);

        BodySkinClone = new List<string>(MainMonterController.THIS.BodySkin);
        HeadSpriteGenClone = new List<Sprite>(MainMonterController.THIS.HeadSpriteGen);
        AccSpriteGenClone = new List<Sprite>(MainMonterController.THIS.AccSpriteGen);
        EyesSpriteGenClone = new List<Sprite>(MainMonterController.THIS.EyesSpriteGen);
        MounthSpriteGenClone = new List<Sprite>(MainMonterController.THIS.MounthSpriteGen);

    }
    private void Start()
    {
        
        
        index = Random.Range(0, MainMonterController.THIS.HeadSprite.Count);
        itemInSideObject.GetComponent<SpriteRenderer>().sprite = MainMonterController.THIS.HeadSpriteGen[index];
        ButtonChangeSkeletonYes.THIS.index = index;
        ButtonChangeSkeletonYes.THIS.Type = "Head";
        StartCoroutine(FadeInCoroutine());
        PlayAnimationCharacter(0, "Idle", true);
        MonsterModeGameManager.THIS.HeadSelectedEffect.Play();
        
    }

    //public IEnumerator SetButtonBack()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    GameManager.THIS.YesButton.interactable = true;
    //    GameManager.THIS.NoButton.interactable = true;
    //    GameManager.THIS.ChangeOptionButton.interactable = true;
    //}

    private void Update()
    {   
        if (spawnedObject != null && itemInSideObject != null && isMoveToImage == false)
        {
            spawnedObject.transform.position = Item.position;
            itemInSideObject.transform.position = Item.position;
            bongbongBlowEffect.transform.position = new Vector3 (Item.position.x, Item.position.y+1.2f, Item.position.z);
        }
            
    }

    public void DisableBongBongAndItem()
    {
        Destroy(spawnedObject);
        Destroy(itemInSideObject);
    }



    public void PlayAnimationCharacter(int index, string animation, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, animation, isLoop);
    }

    public void PlayAnimationCharacterHasDelay(int index, string animation, bool isLoop, float delay)
    {
        skeletonAnimation.AnimationState.AddAnimation(index, animation, isLoop, delay);
    }

    private IEnumerator FadeInCoroutine()
    {   

        PlayAnimationCharacter(1,"Lay_Item", false);
        SoundController.THIS.PlayYesOrNoClip();
        //spawnedObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); 
        SetObjectAlpha(0f);
        yield return new WaitForSeconds(0.3f);
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeInDuration);
            SetObjectAlpha(alpha);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo độ trong suốt cuối cùng là độ trong suốt mục tiêu
        SetObjectAlpha(targetAlpha);
        
    }

    private void SetObjectAlpha(float alpha)
    {
        Color objectColor = spawnedObject.GetComponent<Renderer>().material.color;
        Color objectColor1 = itemInSideObject.GetComponent<Renderer>().material.color;

        objectColor.a = alpha;
        spawnedObject.GetComponent<Renderer>().material.color = objectColor;
        objectColor1.a = alpha;
        itemInSideObject.GetComponent<Renderer>().material.color = objectColor1;
    }

    public void RemoveSkin(string type)
    {
        if (type == "Head")
        {
            MainMonterController.THIS.HeadSprite.RemoveAt(index);
            MainMonterController.THIS.HeadSpriteGen.RemoveAt(index);
        }
        else if (type == "Body")
        {
            MainMonterController.THIS.BodySprite.RemoveAt(index);
            MainMonterController.THIS.BodySkin.RemoveAt(index);
        }
        else if (type == "Acc")
        {
            MainMonterController.THIS.AccSprite.RemoveAt(index);
            MainMonterController.THIS.AccSpriteGen.RemoveAt(index);
        }
        else if (type == "Eyes")
        {
            MainMonterController.THIS.EyesSprite.RemoveAt(index);
            MainMonterController.THIS.EyesSpriteGen.RemoveAt(index);
        }
        else if (type == "Mouth")
        {
            MainMonterController.THIS.MounthSprite.RemoveAt(index);
            MainMonterController.THIS.MounthSpriteGen.RemoveAt(index);
        }
    }

    public void RandomHeadSkin()
    {
        if (MainMonterController.THIS.HeadSpriteGen.Count == 0)
        {
            MainMonterController.THIS.HeadSpriteGen = (HeadSpriteGenClone);
            MainMonterController.THIS.HeadSprite = (HeadSpriteClone);

        }
        
        StartCoroutine(FadeInCoroutine());

        index = Random.Range(0, MainMonterController.THIS.HeadSprite.Count);
        itemInSideObject.GetComponent<SpriteRenderer>().sprite = MainMonterController.THIS.HeadSpriteGen[index];
        MonsterModeGameManager.THIS.HeadBg.sprite = MonsterModeGameManager.THIS.ItemIsSelecting;



        ButtonChangeSkeletonYes.THIS.index = index;
        ButtonChangeSkeletonYes.THIS.Type = "Head";
        ButtonChangeSkeletonNo.THIS.Type = "Head";
        //StartCoroutine(SetButtonBack());

    }

    public void RandomBodySkin()
    {
        if (MainMonterController.THIS.BodySprite.Count == 0)
        {
            MainMonterController.THIS.BodySprite.AddRange(HeadSpriteClone);
            MainMonterController.THIS.BodySkin.AddRange(BodySkinClone);
        }
        StartCoroutine(FadeInCoroutine());
        index = Random.Range(0, MainMonterController.THIS.BodySkin.Count);
        itemInSideObject.GetComponent<SpriteRenderer>().sprite = MainMonterController.THIS.BodySprite[index];
        MonsterModeGameManager.THIS.BodyBg.sprite = MonsterModeGameManager.THIS.ItemIsSelecting;
        //MainMonterController.THIS.HeadSprite.RemoveAt(index);
        //MainMonterController.THIS.BodySprite.RemoveAt(index);
        //MainMonterController.THIS.BodySkin.RemoveAt(index);
        ButtonChangeSkeletonYes.THIS.index = index;
        ButtonChangeSkeletonYes.THIS.Type = "Body";
        ButtonChangeSkeletonNo.THIS.Type = "Body";
        //StartCoroutine(SetButtonBack());
    }

    public void RandomAccSkin()
    {
        if (MainMonterController.THIS.AccSpriteGen.Count == 0)
        {
            MainMonterController.THIS.AccSpriteGen.AddRange(AccSpriteGenClone);
            MainMonterController.THIS.AccSprite.AddRange(AccSpriteClone);
        }
        StartCoroutine(FadeInCoroutine());
        index = Random.Range(0, MainMonterController.THIS.AccSprite.Count);
        itemInSideObject.GetComponent<SpriteRenderer>().sprite = MainMonterController.THIS.AccSpriteGen[index];
        //MainMonterController.THIS.HeadSprite.RemoveAt(index);
        MonsterModeGameManager.THIS.AccBg.sprite = MonsterModeGameManager.THIS.ItemIsSelecting;


        //MainMonterController.THIS.AccSprite.RemoveAt(index);
        //MainMonterController.THIS.AccSpriteGen.RemoveAt(index);
        ButtonChangeSkeletonYes.THIS.index = index;
        ButtonChangeSkeletonYes.THIS.Type = "Acc";
        ButtonChangeSkeletonNo.THIS.Type = "Acc";
        //StartCoroutine(SetButtonBack());
    }

    public void RandomEyesSkin()
    {
        if (MainMonterController.THIS.EyesSpriteGen.Count == 0)
        {
            MainMonterController.THIS.EyesSpriteGen.AddRange(EyesSpriteGenClone);
            MainMonterController.THIS.EyesSprite.AddRange(EyesSpriteClone);
        }
        StartCoroutine(FadeInCoroutine());
        index = Random.Range(0, MainMonterController.THIS.EyesSprite.Count);
        itemInSideObject.GetComponent<SpriteRenderer>().sprite = MainMonterController.THIS.EyesSpriteGen[index];
        //MainMonterController.THIS.HeadSprite.RemoveAt(index);
        MonsterModeGameManager.THIS.EyesBg.sprite = MonsterModeGameManager.THIS.ItemIsSelecting;

        //MainMonterController.THIS.EyesSprite.RemoveAt(index);
        //MainMonterController.THIS.EyesSpriteGen.RemoveAt(index);

        ButtonChangeSkeletonYes.THIS.index = index;
        ButtonChangeSkeletonYes.THIS.Type = "Eyes";
        ButtonChangeSkeletonNo.THIS.Type = "Eyes";
        //StartCoroutine(SetButtonBack());
    }


    public void RandomMouthSkin()
    {
        if (MainMonterController.THIS.MounthSpriteGen.Count == 0)
        {
            MainMonterController.THIS.MounthSpriteGen.AddRange(MounthSpriteGenClone);
            MainMonterController.THIS.MounthSprite.AddRange(MounthSpriteClone);
        }
        StartCoroutine(FadeInCoroutine());
        index = Random.Range(0, MainMonterController.THIS.MounthSprite.Count);
        itemInSideObject.GetComponent<SpriteRenderer>().sprite = MainMonterController.THIS.MounthSpriteGen[index];
        //StartCoroutine(FadeInCoroutine());
        MonsterModeGameManager.THIS.MouthBg.sprite = MonsterModeGameManager.THIS.ItemIsSelecting;

        //MainMonterController.THIS.MounthSprite.RemoveAt(index);
        //MainMonterController.THIS.MounthSpriteGen.RemoveAt(index);

        ButtonChangeSkeletonYes.THIS.index = index;
        ButtonChangeSkeletonYes.THIS.Type = "Mouth";
        ButtonChangeSkeletonNo.THIS.Type = "Mouth";
        //StartCoroutine(SetButtonBack());
    }

    public void ScaleImage()
    {
        
        itemInSideObject.GetComponent<Transform>().DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f);
    }

    public void ResetScaleImage()
    {
        itemInSideObject.GetComponent<Transform>().DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f);
        spawnedObject.GetComponent<Transform>().DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f);
    }

    public void MoveImageToTheImage(Transform targetPosition, bool isEnd,string type,int indexItem)
    {
        isMoveToImage = true;
        
        itemInSideObject.transform.DOMove(targetPosition.position, 1f)
            .SetEase(Ease.OutQuad)

            .OnComplete(() =>
            {
                if (isEnd == false)
                {
                    firstEffect.transform.position = targetPosition.position;
                    
                    firstEffect.GetComponent<ParticleSystem>().Play();
                    targetPosition.DOPunchScale(new Vector3(.5f, .5f, 0f), 0.5f).SetEase(Ease.InOutElastic);
                    MonsterModeGameManager.THIS.ChangeCurrentSkin(true);
                    // behindObject.SetActive(true);
                    isMoveToImage = false;
                    //Destroy(imageObject);
                    MonsterModeGameManager.THIS.RandomSkinYesNo();
                    
                    
                    MainMonterController.THIS.PlayAnimationCharacterHasDelay(0,"Idle_Ngoi", true,1f);
                    MainMonterController.THIS.UpdateCharacterSkinIndex(type, indexItem);

                    //firstEffect.SetActive(false);

                    StartCoroutine(SetButtonCanSelect());
                        
                }
                else
                {
                    firstEffect.transform.position = targetPosition.position;
                   // firstEffect.SetActive(true);
                    firstEffect.GetComponent<ParticleSystem>().Play();
                    targetPosition.DOPunchScale(new Vector3(.5f, .5f, 0f), 0.5f).SetEase(Ease.InOutElastic);
                    MonsterModeGameManager.THIS.ChangeCurrentSkin(true);                 
                    isMoveToImage = false;
                   
                    MainMonterController.THIS.UpdateCharacterSkinIndex(type, indexItem);

                    MonsterModeGameManager.THIS.WinGame();
                }
            });
    }

    private IEnumerator SetButtonCanSelect()
    {
        yield return new WaitForSeconds(1.2f);
        MonsterModeGameManager.THIS.YesButton.interactable = true;
        MonsterModeGameManager.THIS.NoButton.interactable = true;
        MonsterModeGameManager.THIS.ChangeOptionButton.interactable = true;
    }

    public void FadeBongBongImage(string Type, int index)
    {
        itemInSideObject.GetComponent<Transform>().DOScale(new Vector3(0.675f, 0.675f, 0.675f), 0.675f);
        spawnedObject.GetComponent<Transform>().DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.3f).OnComplete(()=> {
            
            StartCoroutine(FadeInCoroutineBongBong(Type, index));
        });
        
    }

    private IEnumerator FadeInCoroutineBongBong(string Type, int index)
    {
       
        float elapsedTime = 0f;

        //float startTime = Time.time;
        //float endTime = startTime + duration;
  
        while (elapsedTime < 0.7f)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / 0.7f);
            SetObjectAlphaBongBong(alpha);
            // Thực hiện scale nhỏ lại
            

            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        if (elapsedTime >0.5f)
        {
            bongbongBlowEffect.GetComponent<ParticleSystem>().Play();
            ScaleImage();
        }
            
            

        
        // Đảm bảo độ trong suốt cuối cùng là độ trong suốt mục tiêu
        SetObjectAlphaBongBong(0f);
        //spawnedObject.transform.localScale = Vector3.zero;
        //spawnedObject.transform.localScale = new Vector3(0f, 0f, 0f);
        //imageObject.GetComponent<SettingImageMove>().SettingImage();
        if (Type == "Head")
        {
            MoveImageToTheImage(MonsterModeGameManager.THIS.HeadMove, false, "Head", index);
        }
        else if (Type == "Body")
        {

            MoveImageToTheImage(MonsterModeGameManager.THIS.BodyMove, false, "Body", index);
        }
        else if (Type == "Acc")
        {

            MoveImageToTheImage(MonsterModeGameManager.THIS.AccMove, false, "Acc", index);
        }

        else if (Type == "Eyes")
        {
            MoveImageToTheImage(MonsterModeGameManager.THIS.EyesMove, false, "Eyes", index);
        }

        else if (Type == "Mouth")
        {
            MoveImageToTheImage(MonsterModeGameManager.THIS.MouthMove, true, "Mouth", index);
        }
    }

    private void SetObjectAlphaBongBong(float alpha)
    {
        Color objectColor = spawnedObject.GetComponent<Renderer>().material.color;
        objectColor.a = alpha;
        spawnedObject.GetComponent<Renderer>().material.color = objectColor;
    }
}
