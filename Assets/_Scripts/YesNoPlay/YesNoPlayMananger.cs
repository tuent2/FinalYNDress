using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;
using NaughtyAttributes;
using DG.Tweening;
public enum SkinToChange
{   
    Base,
    Hair,
    Shirt,
    Skirt,
    Acc,
    Shoe,
    Face
}
public class YesNoPlayMananger : MonoBehaviour
{
    public static YesNoPlayMananger THIS;

    public SkinToChange currentSkin;

    [Header("SpineString")]
    [SpineSkin]
    public List<string> hairSkin;
    [SpineSkin]
    public List<string> shirtSkin;
    [SpineSkin]
    public List<string> skirtSkin;
    [SpineSkin]
    public List<string> accSkin;
    [SpineSkin]
    public List<string> shoeSkin;
    [SpineSkin]
    public List<string> faceSkin;
    [Header("Sprite")]
    public List<Sprite> hairSprite;
    public List<Sprite> shirtSprite;
    public List<Sprite> skirtSprite;
    public List<Sprite> accSprite;
    public List<Sprite> shoeSprite;
    public List<Sprite> faceSprite;
    public AllDataSprite allDataSprite;

    public MainCharacterController mainCharacterController;
   // private Transform mainCharacterTransform;
    public GameObject behindCharacterSheep;
    public GameObject behindCharacterGrimace;
    public BehindCharacterController behindCharacterController;
    //public UIYesNoPlay uiYesNoPlay;
    [Space]
    public Image hairSelectedImage;
    public Image shirtSelectedImage;
    public Image skirtSelectedImage;
    public Image accSelectedImage;
    public Image shoeSelectedImage;
    public Image faceSelectedImage;

    [Space]
    public ParticleSystem SelectingEffect;
    public ParticleSystem SelectedEffect;
    public ParticleSystem SkinChangeEffect;
    public ParticleSystem SenceChangeEffect;
    public ParticleSystem EmojiEffect;
    
    public GameObject ItemInSideGame;
    [Space]
    public Transform hairPositonEffectPlay;
    public Transform shirtPositonEffectPlay;
    public Transform skirtPositonEffectPlay;
    public Transform accPositonEffectPlay;
    public Transform shoePositonEffectPlay;
    public Transform facePositonEffectPlay;

    [Space]
    public Image hairBg;
    public Image shirtBg;
    public Image skirtBg;
    public Image accBg;
    public Image shoeBg;
    public Image faceBg;
    [Space]
    [SerializeField] Sprite ItemNonSelect;
    public Sprite ItemSelected;
    public Sprite ItemIsSelecting;
     
   // private string SkinChange;
    private int CountYN = 1;
    public int skinIndex;

    private int numberPlay;
    Camera cameraMain;
    public SkinToChange firstSkin;
    public SkinToChange secondSkin;
   

    private void Awake()
    {
        THIS = this;
        numberPlay = PlayerPrefs.GetInt(DataGame.NumberPlay, 0);
        
        
    }

    private void ImportDataSprite()
    {
        hairSprite = new List<Sprite>(allDataSprite.hairSprite) ;
        shirtSprite = new List<Sprite>(allDataSprite.shirtSprite);
        skirtSprite = new List<Sprite>(allDataSprite.skirtSprite);
        accSprite = new List<Sprite>(allDataSprite.accSprite);
        shoeSprite = new List<Sprite>(allDataSprite.shoeSprite);
        faceSprite = new List<Sprite>(allDataSprite.faceSprite);
    }
    private void ImportSpineSkinName()
    {
        hairSkin = new List<string>();
        shirtSkin = new List<string>();
        skirtSkin = new List<string>();
        accSkin = new List<string>();
        shoeSkin = new List<string>();
        faceSkin = new List<string>();

        var defaultSkin = mainCharacterController.skeletonAnimation.Skeleton.Data;
        foreach (var skin in defaultSkin.Skins)
        {
            if (skin.Name.StartsWith("Hair/"))
            {
                hairSkin.Add(skin.Name);
            }
            if (skin.Name.StartsWith("Shirt/"))
            {
                shirtSkin.Add(skin.Name);
            }
            if (skin.Name.StartsWith("Skirt/"))
            {
                skirtSkin.Add(skin.Name);
            }
            if (skin.Name.StartsWith("Accessory/"))
            {
                accSkin.Add(skin.Name);
            }
            if (skin.Name.StartsWith("Shoes/"))
            {
                shoeSkin.Add(skin.Name);
            }
            if (skin.Name.StartsWith("Face/"))
            {
                faceSkin.Add(skin.Name);
            }

        }
    }



    public void RandomItemLimited()
    {

        SkinToChange[] skins = { SkinToChange.Hair, SkinToChange.Shirt, SkinToChange.Skirt, SkinToChange.Acc, SkinToChange.Shoe, SkinToChange.Face };

         firstSkin = skins[UnityEngine.Random.Range(0, skins.Length)];

        
        do
        {
            secondSkin = skins[UnityEngine.Random.Range(0, skins.Length)];
        } while (Mathf.Abs((int)secondSkin - (int)firstSkin) < 2);

        Debug.Log("Phần tử thứ nhất: " + firstSkin);
        Debug.Log("Phần tử thứ hai: " + secondSkin);
    }

    public void ClearOneItemShowPopup()
    {
        if(firstSkin == currentSkin)
        {
            firstSkin = SkinToChange.Base;
        }
        if (secondSkin == currentSkin)
        {
            secondSkin = SkinToChange.Base;
        }
    }
    [Button("TestSkin")]
    public void TestSkinCharacter()
    {

        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Base, "DaTrang");
        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Hair, "Hair/Hair_Base_Trang");
        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shirt, "Shirt/Shirt_Base_Trang");
        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Skirt, "Skirt/Skirt_Base_Den");
        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Acc, null);
        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shoe, "Shoes/Shoes_Base_Trang");
        mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Face, null);

        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Base, "DaDen");
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Hair, "Hair/Hair_Base_Den");
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shirt, "Shirt/Shirt_Base_Den");
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Skirt, "Skirt/Skirt_Base_Den");
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Acc, null);
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shoe, "Shoes/Shoes_Base_Den");
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Face, null);




        //int BaseSkin = UnityEngine.Random.Range(0, 2);
        //int hairIndex = UnityEngine.Random.Range(0, hairSprite.Count);
        //int shirtIndex = UnityEngine.Random.Range(0, shirtSprite.Count);
        //int skirtIndex = UnityEngine.Random.Range(0, skirtSprite.Count);
        //int accIndenx = UnityEngine.Random.Range(0, accSprite.Count);
        //int shoeIndex = UnityEngine.Random.Range(0, shoeSprite.Count);
        //int faceIndex = UnityEngine.Random.Range(0, faceSprite.Count);





        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Base, (BaseSkin == 0) ? "DaDen" : "DaTrang");
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Hair, hairSkin[hairIndex]);
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shirt, shirtSkin[shirtIndex]);
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Skirt, skirtSkin[skirtIndex]);
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Acc, accSkin[accIndenx]);
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shoe, shoeSkin[shoeIndex]);
        //mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Face, faceSkin[faceIndex]);
        mainCharacterController.UpdateCharacterSkin();


       
    }

    public void SetUpToPlay()
    {
        currentSkin = SkinToChange.Hair;
        GameManager.THIS.uIYesNoPlay.SetupForYNPlay();
        ImportSpineSkinName();
        ImportDataSprite();
        if (cameraMain == null)
        {
            cameraMain = Camera.main;
        }
        RandomItemLimited();
       // ImportSpineSkinName();
       // ImportDataSprite();
        //SelectingEffect.transform.position = shirtBg.transform.position;

        if (numberPlay == 0 || numberPlay == 1) 
        {
            mainCharacterController.RandomMainCharacterSkin(numberPlay);
            if (numberPlay == 1)
            {
                GameManager.THIS.overlayCanvasController.RateUsCanvas.SetActive(true);
            }
        }
        else
        {
            
            int abc = PlayerPrefs.GetInt(DataGame.isRating, 0);
            numberPlay = PlayerPrefs.GetInt(DataGame.NumberPlay, 0);
            if (abc == 0 && numberPlay % 15 == 0)
            {
                GameManager.THIS.overlayCanvasController.RateUsCanvas.SetActive(true);
            }


            int BaseSkin = UnityEngine.Random.Range(0, 2);
            int hairIndex = UnityEngine.Random.Range(0, hairSprite.Count);
            int shirtIndex = UnityEngine.Random.Range(0, shirtSprite.Count);
            int skirtIndex = UnityEngine.Random.Range(0, skirtSprite.Count);
            int accIndenx = UnityEngine.Random.Range(0, accSprite.Count);
            int shoeIndex = UnityEngine.Random.Range(0, shoeSprite.Count);
            //int faceIndex = UnityEngine.Random.Range(0, faceSprite.Count);

           

;

            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Base, (BaseSkin == 0) ? "DaDen" : "DaTrang");
            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Hair, hairSkin[hairIndex]);
            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shirt, shirtSkin[shirtIndex]);
            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Skirt, skirtSkin[skirtIndex]);
            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Acc, accSkin[accIndenx]);
            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Shoe, shoeSkin[shoeIndex]);
            mainCharacterController.UpdateCharacterSkinStringIndex(SkinToChange.Face, null);
            mainCharacterController.UpdateCharacterSkin();


            hairSkin.RemoveAt(hairIndex);
            hairSprite.RemoveAt(hairIndex);

            shirtSkin.RemoveAt(shirtIndex);
            shirtSprite.RemoveAt(shirtIndex);

            skirtSkin.RemoveAt(skirtIndex);
            skirtSprite.RemoveAt(skirtIndex);

            accSkin.RemoveAt(accIndenx);
            accSprite.RemoveAt(accIndenx);

            shoeSkin.RemoveAt(shoeIndex);
            shoeSprite.RemoveAt(shoeIndex);

            //faceSkin.RemoveAt(faceIndex);
            //faceSprite.RemoveAt(faceIndex);

        }
        if (!ItemInSideGame.activeSelf)
        {
            ItemInSideGame.SetActive(true);
        }

        numberPlay ++;
        PlayerPrefs.SetInt(DataGame.NumberPlay, numberPlay);


        mainCharacterController.PlayAnimationCharacter(0, "Idle", true);
        mainCharacterController.transform.localScale = new Vector3(1f, 1f, 1f);
        mainCharacterController.transform.position = new Vector2(0f, (-6.42f-2.72f));
        
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (randomValue >= 0.5f)
        {
            behindCharacterSheep.SetActive(true);
            behindCharacterGrimace.SetActive(false);
            behindCharacterController = behindCharacterSheep.GetComponent<BehindCharacterController>();
        }
        else
        {
            behindCharacterSheep.SetActive(false);
            behindCharacterGrimace.SetActive(true);
            behindCharacterController = behindCharacterGrimace.GetComponent<BehindCharacterController>();
        }
        behindCharacterController.PlayAnimationCharacter(0, "Idle", true);
        behindCharacterController.ResetScaleItemAndBuble();
        behindCharacterController.SetObjectAlpha(1f);
        RandomSkinInSideBuble();
        SelectedEffect.gameObject.SetActive(true);
        SelectingEffect.gameObject.SetActive(true);
        SkinChangeEffect.gameObject.SetActive(true);

        hairSelectedImage.gameObject.SetActive(false);
        shirtSelectedImage.gameObject.SetActive(false);
        skirtSelectedImage.gameObject.SetActive(false);
        accSelectedImage.gameObject.SetActive(false);
        shoeSelectedImage.gameObject.SetActive(false);
        faceSelectedImage.gameObject.SetActive(false);

        hairBg.sprite = ItemNonSelect;
        shirtBg.sprite = ItemNonSelect;
        skirtBg.sprite = ItemNonSelect;
        accBg.sprite = ItemNonSelect;
        shoeBg.sprite = ItemNonSelect;
        faceBg.sprite = ItemNonSelect;
       // Debug.Log(hairBg.transform.position);
        SelectingEffect.transform.position = hairBg.transform.position;
       // Debug.Log(SelectingEffect.transform.position);

    }

    public int RandomLastFiveItemOfList(List<Sprite> listSprite)
    {
        int listLength = listSprite.Count;

        List<Sprite> selectedSprites = new List<Sprite>();

        for (int i = listLength - 5; i < listLength; i++)
        {
            selectedSprites.Add(listSprite[i]);
        }

        int randomIndex = UnityEngine.Random.Range(listLength - 5, listLength);
        //Sprite selectedSprite = listSprite[randomIndex];

      
        //foreach (Sprite sprite in selectedSprites)
        //{
        //    Debug.Log(sprite.name);
        //}
        Debug.Log("Vị trí của sprite được chọn: " + randomIndex);
        return randomIndex;
        
    }

    public  void RandomSkinInSideBuble()
    {


        switch (currentSkin)
        {
            case SkinToChange.Hair:
                {   
                    if (currentSkin == firstSkin || currentSkin == secondSkin)
                    {
                        UIYesNoPlay.THIS.limitedItemCanvas.gameObject.SetActive(true);
                        UIYesNoPlay.THIS.limitedItemCanvas.UpdateSpriteInItemImage(RandomLastFiveItemOfList(hairSprite), hairSprite);
                        
                        return;
                    }
                    behindCharacterController.PlayAniAndChangeSprite(hairSprite);
                    
                    break;
                }
            case SkinToChange.Shirt:
                {
                    if (currentSkin == firstSkin || currentSkin == secondSkin)
                    {
                        UIYesNoPlay.THIS.limitedItemCanvas.gameObject.SetActive(true);
                        UIYesNoPlay.THIS.limitedItemCanvas.UpdateSpriteInItemImage(RandomLastFiveItemOfList(shirtSprite), shirtSprite);
                        
                        return;
                    }
                    behindCharacterController.PlayAniAndChangeSprite(shirtSprite);
                    
                    break;
                }
            case SkinToChange.Skirt:
                {
                    if (currentSkin == firstSkin || currentSkin == secondSkin)
                    {
                        UIYesNoPlay.THIS.limitedItemCanvas.gameObject.SetActive(true);
                        UIYesNoPlay.THIS.limitedItemCanvas.UpdateSpriteInItemImage(RandomLastFiveItemOfList(skirtSprite), skirtSprite);
                        
                        return;
                    }
                    behindCharacterController.PlayAniAndChangeSprite(skirtSprite);
                   
                    break;
                }
            case SkinToChange.Acc:
                {
                    if (currentSkin == firstSkin || currentSkin == secondSkin)
                    {
                        UIYesNoPlay.THIS.limitedItemCanvas.gameObject.SetActive(true);
                        UIYesNoPlay.THIS.limitedItemCanvas.UpdateSpriteInItemImage(RandomLastFiveItemOfList(accSprite), accSprite);
                        
                        return;
                    }
                    behindCharacterController.PlayAniAndChangeSprite(accSprite);
                   
                    break;
                }
            case SkinToChange.Shoe:
                {
                    if (currentSkin == firstSkin || currentSkin == secondSkin)
                    {
                        UIYesNoPlay.THIS.limitedItemCanvas.gameObject.SetActive(true);
                        UIYesNoPlay.THIS.limitedItemCanvas.UpdateSpriteInItemImage(RandomLastFiveItemOfList(shoeSprite), shoeSprite);
                        
                        return;
                    }
                    behindCharacterController.PlayAniAndChangeSprite(shoeSprite);
                    
                    break;
                }
            case SkinToChange.Face:
                {
                    if (currentSkin == firstSkin || currentSkin == secondSkin)
                    {
                        UIYesNoPlay.THIS.limitedItemCanvas.gameObject.SetActive(true);
                        UIYesNoPlay.THIS.limitedItemCanvas.UpdateSpriteInItemImage(RandomLastFiveItemOfList(faceSprite), faceSprite);
                        
                        return;
                    }
                    behindCharacterController.PlayAniAndChangeSprite(faceSprite);
                    
                    break;
                }
            default:
                break;
        }
    }

    public void ChangeIndexOfSkinItem(bool isYes)
    {
        switch (currentSkin)
        {
            case SkinToChange.Hair:
                {
                    if (isYes == true)
                    {
                        
                        hairSelectedImage.gameObject.SetActive(true);
                        hairBg.sprite = ItemSelected;

                        mainCharacterController.UpdateCharacterSkinStringIndex(currentSkin, hairSkin[skinIndex]);
                        mainCharacterController.UpdateCharacterSkin();
                        currentSkin = SkinToChange.Shirt;
                        shirtBg.sprite = ItemIsSelecting;
                        SelectingEffect.transform.position = shirtBg.transform.position;
                        SkinChangeEffect.transform.position = hairPositonEffectPlay.position;
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            
                            hairSelectedImage.gameObject.SetActive(true);
                            hairBg.sprite = ItemSelected;

                            
                            currentSkin = SkinToChange.Shirt;
                            shirtBg.sprite = ItemIsSelecting;
                            SelectingEffect.transform.position = shirtBg.transform.position;
                            SkinChangeEffect.transform.position = hairPositonEffectPlay.position;
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }

            case SkinToChange.Shirt:
                {
                    if (isYes == true)
                    {
                        
                        shirtSelectedImage.gameObject.SetActive(true);
                        shirtBg.sprite = ItemSelected;

                        mainCharacterController.UpdateCharacterSkinStringIndex(currentSkin, shirtSkin[skinIndex]);
                        mainCharacterController.UpdateCharacterSkin();
                        currentSkin = SkinToChange.Skirt;
                        skirtBg.sprite = ItemIsSelecting;
                        SelectingEffect.transform.position = skirtBg.transform.position;
                        SkinChangeEffect.transform.position = shirtPositonEffectPlay.position;
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            
                            shirtSelectedImage.gameObject.SetActive(true);
                            shirtBg.sprite = ItemSelected;

                            
                            currentSkin = SkinToChange.Skirt;
                            skirtBg.sprite = ItemIsSelecting;
                            SelectingEffect.transform.position = skirtBg.transform.position;
                            SkinChangeEffect.transform.position = shirtPositonEffectPlay.position;
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }

            case SkinToChange.Skirt:
                {

                    if (isYes == true)
                    {
                        
                        skirtSelectedImage.gameObject.SetActive(true);
                        skirtBg.sprite = ItemSelected;

                        mainCharacterController.UpdateCharacterSkinStringIndex(currentSkin, skirtSkin[skinIndex]);
                        mainCharacterController.UpdateCharacterSkin();
                        currentSkin = SkinToChange.Acc;
                        accBg.sprite = ItemIsSelecting;
                        SelectingEffect.transform.position = accBg.transform.position;
                        SkinChangeEffect.transform.position = skirtPositonEffectPlay.position;
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            
                            skirtSelectedImage.gameObject.SetActive(true);
                            skirtBg.sprite = ItemSelected;

                            
                            currentSkin = SkinToChange.Acc;
                            accBg.sprite = ItemIsSelecting;
                            SelectingEffect.transform.position = accBg.transform.position;
                            SkinChangeEffect.transform.position = skirtPositonEffectPlay.position;
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }
            case SkinToChange.Acc:
                {

                    if (isYes == true)
                    {
                        
                        accSelectedImage.gameObject.SetActive(true);
                        accBg.sprite = ItemSelected;

                        mainCharacterController.UpdateCharacterSkinStringIndex(currentSkin, accSkin[skinIndex]);
                        mainCharacterController.UpdateCharacterSkin();
                        currentSkin = SkinToChange.Shoe;
                        shoeBg.sprite = ItemIsSelecting;
                        SelectingEffect.transform.position = shoeBg.transform.position;
                        SkinChangeEffect.transform.position = accPositonEffectPlay.position;
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                       // StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            //accSelectedEffect.Stop();
                            accSelectedImage.gameObject.SetActive(true);
                            accBg.sprite = ItemSelected;
                            
                            currentSkin = SkinToChange.Shoe;
                            shoeBg.sprite = ItemIsSelecting;
                            SelectingEffect.transform.position = shoeBg.transform.position;
                            SkinChangeEffect.transform.position = accPositonEffectPlay.position;
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }

            case SkinToChange.Shoe:
                {

                    if (isYes == true)
                    {
                       
                        shoeSelectedImage.gameObject.SetActive(true);
                        shoeBg.sprite = ItemSelected;

                        mainCharacterController.UpdateCharacterSkinStringIndex(currentSkin, shoeSkin[skinIndex]);
                        mainCharacterController.UpdateCharacterSkin();
                        currentSkin = SkinToChange.Face;
                        faceBg.sprite = ItemIsSelecting;
                        SelectingEffect.transform.position = faceBg.transform.position;
                        SkinChangeEffect.transform.position = facePositonEffectPlay.position;
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            SelectingEffect.Stop();
                            shoeSelectedImage.gameObject.SetActive(true);
                            shoeBg.sprite = ItemSelected;

                            
                            currentSkin = SkinToChange.Face;
                            faceBg.sprite = ItemIsSelecting;
                            SelectingEffect.transform.position = faceBg.transform.position;
                            SkinChangeEffect.transform.position = facePositonEffectPlay.position;
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }

            case SkinToChange.Face:
                {

                    if (isYes == true)
                    {
                        SelectingEffect.Stop();
                        faceSelectedImage.gameObject.SetActive(true);
                        faceBg.sprite = ItemSelected;

                        mainCharacterController.UpdateCharacterSkinStringIndex(currentSkin, faceSkin[skinIndex]);
                        mainCharacterController.UpdateCharacterSkin();


                        //isFinalChoice = true;
                        currentSkin = SkinToChange.Hair;
                        //faceBg.sprite = ItemIsSelecting;
                        SelectingEffect.transform.position = faceBg.transform.position;
                        SkinChangeEffect.transform.position = facePositonEffectPlay.position;
                        CountYN = 1;
                        //GameManager.THIS.ChangePhaseShow();
                        //GameManager.THIS.WinGame();
                        break;
                    }
                    else
                    {
                        if (CountYN == 2)
                        {
                            SelectingEffect.Stop();
                            faceSelectedImage.gameObject.SetActive(true);
                            faceBg.sprite = ItemSelected;

                           
                            //isFinalChoice = true;
                            //currentSkin = SkinToChange.Head;
                            SelectingEffect.transform.position = hairBg.transform.position;
                            SkinChangeEffect.transform.position = hairPositonEffectPlay.position;
                            CountYN = 1;
                            //GameManager.THIS.ChangePhaseShow();
                            //WinGame();
                            FinishChoiceItem();
                            break;
                        }
                        else
                        {
                            //StartCoroutine(WaitToBeHindMonsterShow());
                            CountYN++;
                            break;
                        }

                    }
                }
            default:
                break;
        }
    }

    private IEnumerator WaitToBeHindMonsterShow()
    {
        yield return new WaitForSeconds(0.7f);
        behindCharacterController.PlayAnimationCharacter(0, "No", true);
        //SoundController.THIS.PlayNotSelectedClip();
    }

    [Button("WinGame")]
    public void FinishChoiceItem()
    {
        GameManager.THIS.overlayCanvasController.buttonPanel.SetActive(false);
        SoundController.THIS.PlayBackGroundCompletedClip();
        UIYesNoPlay.THIS.ActionChangeWhenFinishChoiceItem();
        behindCharacterController.gameObject.SetActive(false);
        ItemInSideGame.SetActive(false);
        SelectingEffect.gameObject.SetActive(false);
        SelectedEffect.gameObject.SetActive(false);
       
        cameraMain.DOOrthoSize(cameraMain.orthographicSize / 1.2f, 1f);
        mainCharacterController.gameObject.transform.DOScale(mainCharacterController.gameObject.transform.localScale *1.2f,1f)
            .OnComplete(() =>
            {
                //mainCharacterController.PlayAnimationCharacter(0, "dance", true);
                SkinChangeEffect.gameObject.SetActive(false);
                StartCoroutine(WaitTheFlashToContinues());
            });
    }

    public IEnumerator PlayCompleteSound()
    {
        yield return new WaitForSeconds(0.4f);
        SoundController.THIS.PlayBackGroundCompletedClip();
    }

    public IEnumerator WaitTheFlashToContinues()
    {
        yield return new WaitForSeconds(1f);

        mainCharacterController.PlayAnimationCharacter(0, "Dance", true);
        mainCharacterController.gameObject.transform.position = new Vector3(0f, -4.7f, 0f);
        mainCharacterController.gameObject.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0f);
        yield return new WaitForSeconds(0.35f);
        cameraMain.DOOrthoSize(cameraMain.orthographicSize * 1.2f, 0.5f);
        yield return new WaitForSeconds(1f);
        SenceChangeEffect.Play();
       
        UIYesNoPlay.THIS.PlayIEnumPlayTabToGet();
        yield return new WaitForSeconds(1f);
        EmojiEffect.Play();
    }

    

    public void ClickYesGenImageToTheIcon()
    {
        behindCharacterController.BlowBuble();
    }


    public void RemoveCurrentSkin()
    {
        switch(currentSkin)
        {
            case SkinToChange.Hair:
                hairSkin.RemoveAt(skinIndex);
                hairSprite.RemoveAt(skinIndex);
                break;
            case SkinToChange.Shirt:
                shirtSkin.RemoveAt(skinIndex);
                shirtSprite.RemoveAt(skinIndex);
                break;
            case SkinToChange.Skirt:
                skirtSkin.RemoveAt(skinIndex);
                skirtSprite.RemoveAt(skinIndex);
                break;
            case SkinToChange.Acc:
                accSkin.RemoveAt(skinIndex);
                accSprite.RemoveAt(skinIndex);
                break;
            case SkinToChange.Shoe:
                shoeSkin.RemoveAt(skinIndex);
                shoeSprite.RemoveAt(skinIndex);
                break;
            case SkinToChange.Face:
                faceSkin.RemoveAt(skinIndex);
                faceSprite.RemoveAt(skinIndex);
                break;
            default:
                break;
        }
    }

}
