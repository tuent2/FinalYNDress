using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using System;
public class MainCharacterController : MonoBehaviour
{   
    public SkeletonAnimation skeletonAnimation;
    Skin characterSkin;

    public CharacterData characterData;

   

    private void Start()
    {
        
        
    }

    public void RandomMainCharacterSkin(int numberPlay)
    {
        if (numberPlay == 0)
        {
            characterData.baseSkin = "DaDen";
            characterData.hairSkin = "Hair/Hair_Base_Den";
            characterData.shirtSkin = "Shirt/Shirt_Base_Den";
            characterData.skirtSkin = "Skirt/Skirt_Base_Den";
            characterData.accSkin = null;
            characterData.shoeSkin = "Shoes/Shoes_Base_Den";
            characterData.faceSkin = null;
            UpdateCharacterSkin();
        }
        else if (numberPlay == 1)
        {
            characterData.baseSkin = "DaTrang";
            characterData.hairSkin = "Hair/Hair_Base_Trang";
            characterData.shirtSkin = "Shirt/Shirt_Base_Trang";
            characterData.skirtSkin = "Skirt/Skirt_Base_Den";
            characterData.accSkin = null;
            characterData.shoeSkin = "Shoes/Shoes_Base_Trang";
            characterData.faceSkin = null;
            UpdateCharacterSkin();
        }
    }
    public void UpdateCharacterSkin()
    {
        var skeleton = skeletonAnimation.Skeleton;
        var skeletonData = skeleton.Data;
        characterSkin = new Skin("character-base");
        // bodyActiveString = BodySkin[index];
        
        characterSkin.AddSkin(skeletonData.FindSkin(characterData.baseSkin));


        if (!(characterData.hairSkin == null || characterData.hairSkin == ""))
        {
            characterSkin.AddSkin(skeletonData.FindSkin(characterData.hairSkin));
        }

        if (!(characterData.shirtSkin == null || characterData.shirtSkin == ""))
        {
            characterSkin.AddSkin(skeletonData.FindSkin(characterData.shirtSkin));
        }

        if (!(characterData.skirtSkin == null || characterData.skirtSkin == ""))
        {
            characterSkin.AddSkin(skeletonData.FindSkin(characterData.skirtSkin));
        }

        if (!(characterData.accSkin == null || characterData.accSkin == ""))
        {
            characterSkin.AddSkin(skeletonData.FindSkin(characterData.accSkin));
        }

        if (!(characterData.shoeSkin == null || characterData.shoeSkin == ""))
        {
            characterSkin.AddSkin(skeletonData.FindSkin(characterData.shoeSkin));
        }

        if (!(characterData.faceSkin == null || characterData.faceSkin == ""))
        {
            characterSkin.AddSkin(skeletonData.FindSkin(characterData.faceSkin));
        }

        var resultCombinedSkin = new Skin("character-combined");
        resultCombinedSkin.AddSkin(characterSkin);
        skeleton.SetSkin(resultCombinedSkin);
        skeleton.SetSlotsToSetupPose();
        
    }

  

    public void UpdateCharacterSkinStringIndex(SkinToChange currentSkin, string SkinChange)
    {
        switch (currentSkin)
        {
            case SkinToChange.Base:
                characterData.baseSkin = SkinChange;
                break;
            case SkinToChange.Hair:
                characterData.hairSkin = SkinChange;
                break;
            case SkinToChange.Shirt:
                characterData.shirtSkin = SkinChange;
                break;
            case SkinToChange.Skirt:
                characterData.skirtSkin = SkinChange;
                break;
            case SkinToChange.Acc:
                characterData.accSkin = SkinChange;
                break;
            case SkinToChange.Shoe:
                characterData.shoeSkin = SkinChange;
                break;
            case SkinToChange.Face:
                characterData.faceSkin = SkinChange;
                break;
            default:
                break;
        }
        
    }

    public void PlayAnimationCharacter(int index, string animation, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, animation, isLoop);
    }

    public void PlayAnimationCharacter(int index, string animation, bool isLoop, float delay)
    {
        skeletonAnimation.AnimationState.AddAnimation(index, animation, isLoop, delay);
    }


    public void SaveMonsterData(int scoreMonster)
    {
        characterData.id = DateTime.Now.ToBinary().ToString();
        characterData.scoreCharacter = scoreMonster;
        
        GameManager.THIS.stageManager.SaveMonsterData(characterData);
    }

    #region Album
    public void HandleShowAlbum(CharacterData characterData)
    {
        this.characterData = characterData;
        
        UpdateCharacterSkin();

        //bodySA.Awake();
        PlayAnimationCharacter(0, "Dance", true);
       
    }

    public void SetStateDraggingInAlbum()
    {
        //var trackEntry = bodySA.state.SetAnimation(0, "Dance3", true);
        var trackEntry = skeletonAnimation.state.SetAnimation(0, "Idle", true);
        trackEntry.TimeScale = 1;
    }

    public void SetStateIdleInAlbum()
    {
        //if (GameManager.Instance.albumManager.characterManagerInAblum.Count > 2)
        //{
        //    GameManager.Instance.albumManager.characterManagerDragging.SetStateIdleInAlbum(GameManager.Instance.albumManager.);
        //}
       
        //var trackEntry = skeletonAnimation.state.SetAnimation(0, "Dance", true);
        PlayAnimationCharacter(0, "Dance", true);
        //List<MainCharacterController> characterManagers = GameManager.THIS.stageManager.mainCharacterManagerInStage;
        //float trackTime = 0;
        //for (int i = 0; i < characterManagers.Count; i++)
        //{
        //    if (gameObject.GetInstanceID() != characterManagers[i].gameObject.GetInstanceID())
        //    {
        //        trackTime = characterManagers[i].GetTrackTimeAnim();
        //        break;
        //    }
        //}
        //trackEntry.TrackTime = trackTime;
    }

    public float GetTrackTimeAnim()
    {
        var trackEntry = skeletonAnimation.state.GetCurrent(0);
        var trackTime = trackEntry.TrackTime;
        return trackTime;
    }


    #endregion


}
