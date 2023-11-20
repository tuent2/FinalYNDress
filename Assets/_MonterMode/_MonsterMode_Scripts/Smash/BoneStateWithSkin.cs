using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class BoneStateWithSkin : MonoBehaviour
{
     SkeletonAnimation skeletonAnimation;
    public bool ReloadShowBone()
    {
        skeletonAnimation = SmashCharacterManager.THIS.GetSkeletonActive();
        string nameBone = name;
        string nameSkinGetted = GetSecondPartAfterUnderscore(nameBone.ToLower());
        string nameofbone = GetFirstPartAfterUnderscore(skeletonAnimation.initialSkinName.ToLower());
        //Debug.LogError(nameSkinGetted);
        //Debug.LogError(nameofbone);
        if (nameSkinGetted.Contains(nameofbone))    
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        return gameObject.activeSelf;
    }


    string GetSecondPartAfterUnderscore(string input)
    {
       
        int firstUnderscoreIndex = input.IndexOf('_');

        if (firstUnderscoreIndex != -1)
        {
            
            int secondUnderscoreIndex = input.IndexOf('_', firstUnderscoreIndex + 1);

            if (secondUnderscoreIndex != -1)
            {
                
                return input.Substring(secondUnderscoreIndex + 1);
            }
        }

        return null; 
    }

    string GetFirstPartAfterUnderscore(string input)
    {
        
        int firstUnderscoreIndex = input.IndexOf('_');

        if (firstUnderscoreIndex != -1)
        {
           
            return input.Substring(firstUnderscoreIndex + 1);
        }

        return null; 
    }
}
