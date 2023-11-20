using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using DG.Tweening;
using Lean.Common;
using System.Linq;
public class SmashController : MonoBehaviour
{
    public static SmashController THIS;
    public bool isDead;
    public bool isOpening;
    public GameObject StickController;
    public GameObject SawController;
    public GameObject GmanController;

    //[SerializeField] MaterialChangeState[] materialChangeStates;
    //public SpriteRenderer dirtyHeadObject;
    //public SpriteRenderer bloodHeadObject;
    private void Awake()
    {
        THIS = this;
    }
    void Start()
    {
        SoundController.THIS.PlayInGameBGClip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        isOpening = true;
        gameObject.SetActive(true);
    }

    public void HideAllTrap()
    {
        StickController.SetActive(false);   
        SawController.SetActive(false);
        GmanController.SetActive(false);
        MonsterModeGameManager.THIS.HideAllTheIconGetWeapon();
    }

    //public void SetMaterialOfCharacterDirty()
    //{
    //    for (int i = 0; i < materialChangeStates.Length; i++)
    //    {
    //        materialChangeStates[i].SetDirty();
    //    }
    //    dirtyHeadObject.enabled = (true);
    //    dirtyHeadObject.DOFade(0, 0);
    //    dirtyHeadObject.DOFade(1, .15f);
    //}
    //public void SetMaterialOfCharacterBlood()
    //{
    //    for (int i = 0; i < materialChangeStates.Length; i++)
    //    {
    //        materialChangeStates[i].SetBlood();
    //    }
    //    bloodHeadObject.enabled = (true);
    //    bloodHeadObject.DOFade(0, 0);
    //    bloodHeadObject.DOFade(1, .15f);
    //}

}

//[SerializeField]
//public class MaterialChangeState
//{
//    public Material material;
//    public Texture2D defaultTexture;
//    public Texture2D dirtyTexture;
//    public Texture2D bloodTexture;
//    public void SetDefault()
//    {
//        //material.mainTexture = defaultTexture;
//        material.EnableKeyword("_NORMALMAP");
//        material.SetTexture("_MainTex", defaultTexture);
//    }
//    public void SetDirty()
//    {
//        //material.mainTexture = dirtyTexture;
//        material.EnableKeyword("_NORMALMAP");
//        material.SetTexture("_MainTex", dirtyTexture);
//    }
//    public void SetBlood()
//    {
//        //material.mainTexture = bloodTexture;
//        material.EnableKeyword("_NORMALMAP");
//        material.SetTexture("_MainTex", bloodTexture);
//    }
//}


