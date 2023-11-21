using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;


public class MainMonterController : MonoBehaviour
{
    public static MainMonterController THIS;
    public GameObject SpawnObject;
    private SkeletonAnimation skeletonAnimation;
    private TrackEntry currentAnimation;
    public Transform headTransform;
    Skin characterSkin;
    [SpineSkin]
    public List<string> BodySkin;

    public List<Sprite> BodySprite;
    public List<Sprite> HeadSprite;
    public List<Sprite> AccSprite;
    //public List<Sprite> EyesSprite;
    public List<Sprite> EyesSprite;
    public List<Sprite> MounthSprite;
    
    [Space]
    public List<Sprite> HeadSpriteGen;
    public List<Sprite> AccSpriteGen;
    public List<Sprite> EyesSpriteGen;
    public List<Sprite> MounthSpriteGen;
    
    string bodyActiveString;

    

    public GameObject headSpawnedObject;
    public GameObject AccSpawnedObject;
    public GameObject EyesSpawnedObject;
    public GameObject MounthSpawnedObject;
    public GameObject EffectChangeItem;


    public GameObject headSpawnedObjectSmash;
    public GameObject AccSpawnedObjectSmash;
    public GameObject EyesSpawnedObjectSmash;
    public GameObject MounthSpawnedObjectSmash;
    public SkeletonAnimation skeletonAnimationSmash;
    Skin characterSkinSmash;
    string bodyActiveStringSmash;
    private void Awake()
    {
        THIS = this;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        

    }

    private void Start()
    {
        
    }

    public void RandomCharacter()
    {
        int index1 = Random.Range(0, BodySkin.Count);

        var skeleton = skeletonAnimation.Skeleton;

        Debug.Log(skeletonAnimation);

        var skeletonData = skeleton.Data;
        characterSkin = new Skin("character-base1");
        bodyActiveString = BodySkin[index1];
        characterSkin.AddSkin(skeletonData.FindSkin(bodyActiveString));
        var resultCombinedSkin = new Skin("character-combined");

        resultCombinedSkin.AddSkin(characterSkin);
        skeleton.SetSkin(resultCombinedSkin);
        skeleton.SetSlotsToSetupPose();


        int index2 = Random.Range(0, HeadSprite.Count);
        int index3 = Random.Range(0, AccSprite.Count);
        int index4 = Random.Range(0, EyesSprite.Count);
        int index5 = Random.Range(0, MounthSprite.Count);

        Vector3 newPosition = new Vector3(headTransform.position.x, headTransform.position.y - 0.3f, headTransform.position.z); 

        Transform head123 = new GameObject().transform ;
        head123.position = newPosition;
        //headSpawnedObject = Instantiate(SpawnObject, head123.position, Quaternion.identity, headTransform);
        headSpawnedObject.GetComponent<SpriteRenderer>().sprite = HeadSprite[index2];
        //headSpawnedObject.GetComponent<SpriteRenderer>().sortingOrder = -4;

        
        //AccSpawnedObject = Instantiate(SpawnObject, head123.position, Quaternion.identity, headTransform);
        AccSpawnedObject.GetComponent<SpriteRenderer>().sprite = AccSprite[index3];
        //AccSpawnedObject.GetComponent<SpriteRenderer>().sortingOrder = -1;



        //EyesSpawnedObject = Instantiate(SpawnObject, head123.position, Quaternion.identity, headTransform);
        //EyesSpawnedObject.GetComponent<SpriteRenderer>().sprite = EyesSprite[index4];
        //EyesSpawnedObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
        EyesSpawnedObject.GetComponent<SpriteRenderer>().sprite = EyesSprite[index4];

        //MounthSpawnedObject = Instantiate(SpawnObject, head123.position, Quaternion.identity, headTransform);
        MounthSpawnedObject.GetComponent<SpriteRenderer>().sprite = MounthSprite[index5];
        //MounthSpawnedObject.GetComponent<SpriteRenderer>().sortingOrder = -3;


        HeadSprite.RemoveAt(index2);
        HeadSpriteGen.RemoveAt(index2);
        AccSprite.RemoveAt(index3);
        AccSpriteGen.RemoveAt(index3);
        //EyesSprite.RemoveAt(index4);
        EyesSpriteGen.RemoveAt(index4);
        EyesSprite.RemoveAt(index4);
        MounthSprite.RemoveAt(index5);
        MounthSpriteGen.RemoveAt(index5);
        BodySkin.RemoveAt(index1);
        BodySprite.RemoveAt(index1);
    }

    public void SmashCharacterRandom()
    {
        int index1 = Random.Range(0, BodySkin.Count);

        var skeleton = skeletonAnimationSmash.Skeleton;

        var skeletonData = skeleton.Data;
        characterSkin = new Skin("character-base1");
        bodyActiveString = BodySkin[index1];

        characterSkin.AddSkin(skeletonData.FindSkin(bodyActiveString));
        var resultCombinedSkin = new Skin("character-combined");

        resultCombinedSkin.AddSkin(characterSkin);
        skeleton.SetSkin(resultCombinedSkin);
        skeleton.SetSlotsToSetupPose();


        int index2 = Random.Range(0, HeadSprite.Count);
        int index3 = Random.Range(0, AccSprite.Count);
        int index4 = Random.Range(0, EyesSprite.Count);
        int index5 = Random.Range(0, MounthSprite.Count);

        //Vector3 newPosition = new Vector3(headTransform.position.x, headTransform.position.y - 0.3f, headTransform.position.z);

        //Transform head123 = new GameObject().transform;
        //head123.position = newPosition;
        headSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = HeadSprite[index2];
        AccSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = AccSprite[index3];
        EyesSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = EyesSprite[index4];
        MounthSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = MounthSprite[index5];

    }

    public void PlayAnimationCharacter(int index, string animation, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, animation, isLoop);
    }

    public void PlayAnimationCharacterHasDelay(int index, string animation, bool isLoop, float delay)
    {
        skeletonAnimation.AnimationState.AddAnimation(index, animation, isLoop,delay);
    }

    public void UpdateCharacterSkinIndex(string type, int index)
    {
        switch (type)
        {
            case "Head":
                
                headSpawnedObject.GetComponent<SpriteRenderer>().sprite = HeadSprite[index];
                SoundController.THIS.PlayDressClip();
                EffectChangeItem.GetComponent<ParticleSystem>().Play();
                headSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = HeadSprite[index];
                break;
            case "Body":

                var skeleton = skeletonAnimation.Skeleton;
                var skeletonData = skeleton.Data;
                characterSkin = new Skin("character-base1");
                bodyActiveString = BodySkin[index];
                characterSkin.AddSkin(skeletonData.FindSkin(bodyActiveString));
                var resultCombinedSkin = new Skin("character-combined");
                resultCombinedSkin.AddSkin(characterSkin);
                skeleton.SetSkin(resultCombinedSkin);
                skeleton.SetSlotsToSetupPose();
                SoundController.THIS.PlayDressClip();
                //BodyDanceController.THIS.UpdateBody(bodyActiveString);
                
                EffectChangeItem.GetComponent<ParticleSystem>().Play();

                var skeletonSmash = skeletonAnimationSmash.Skeleton;
                var skeletonDataSmash = skeletonSmash.Data;
                characterSkinSmash = new Skin("character-base1");
                bodyActiveStringSmash = BodySkin[index];
                characterSkin.AddSkin(skeletonDataSmash.FindSkin(bodyActiveString));
                var resultCombinedSkinSmash = new Skin("character-combined");
                resultCombinedSkinSmash.AddSkin(characterSkin);
                skeletonSmash.SetSkin(resultCombinedSkinSmash);
                skeletonSmash.SetSlotsToSetupPose();
             

                break;
            case "Acc":
                
                AccSpawnedObject.GetComponent<SpriteRenderer>().sprite = AccSprite[index];
                SoundController.THIS.PlayDressClip();
                //BodyDanceController.THIS.UpdateAccSprite(AccSprite[index]);
                
                EffectChangeItem.GetComponent<ParticleSystem>().Play();
                AccSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = AccSprite[index];
                break;
            case "Eyes":
                
                //EyesSpawnedObject.GetComponent<SpriteRenderer>().sprite = EyesSprite[index];
                EyesSpawnedObject.GetComponent<SpriteRenderer>().sprite = EyesSprite[index];

                SoundController.THIS.PlayDressClip();
                //BodyDanceController.THIS.UpdateEyesSprite(EyesSprite[index]);
                
                EffectChangeItem.GetComponent<ParticleSystem>().Play();
                EyesSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = EyesSprite[index];
                break;
            case "Mouth":
                
                MounthSpawnedObject.GetComponent<SpriteRenderer>().sprite = MounthSprite[index];
                SoundController.THIS.PlayDressClip();
                //BodyDanceController.THIS.UpdateMouthSprite(MounthSprite[index]);
                
                //EffectChangeItem.GetComponent<ParticleSystem>().Play();
                MounthSpawnedObjectSmash.GetComponent<SpriteRenderer>().sprite = MounthSprite[index];
                break;
            default:
                break;
        }
    }
}
