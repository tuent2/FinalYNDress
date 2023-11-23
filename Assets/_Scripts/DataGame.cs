using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    //---------------------------------------------GameManager--------------------------------------------------//
    public const string isDoneFirstTimePlay = "isDoneFirstTimePlay";
    //private static string path = "Assets/_ConfigAllSlotsData/CSV/AllSlotDatas.csv";
    public const string pathSpriteHair = "Assets/_Textures/Character/Hair/Hair.png";
    public const string pathSpriteShirt = "Assets/_Textures/Character/Shirt/Shirt.png";
    public const string pathSpriteSkirt = "Assets/_Textures/Character/Skirt/Skirt.png";
    public const string pathSpriteAcc = "Assets/_Textures/Character/Acc/Acc.png";
    public const string pathSpriteShoes = "Assets/_Textures/Character/Shoes/Shoes.png";
    public const string pathSpriteFace = "Assets/_Textures/Character/Face/Face.png";

    //---------------------------------------------YesNoPlayManager--------------------------------------------------//
    public const string NumberPlay = "NumberPlay";
    public const string isRating = "isRating";
    public const string isOpenApp = "isOpenApp";
    public const string appPackageName = "https://play.google.com/store/apps/details?id=com.dressup.makeup.leftright.monstermakeover.monsterdressup.yesorno";
    //---------------------------------------------Album--------------------------------------------------//
    public const string screenShotNumber = "ScreenShot_number";
    public static string pathScreenShotDataSave = Application.persistentDataPath + "/ScreenShot/";
    //---------------------------------------------Stage--------------------------------------------------//
    public static string pathCharacterDataSave = Application.persistentDataPath + "/CharactersData.data";
    public static string countMonsterOnAlbumMax = "countMonsterOnAlbumMax";
    public static string isOpenMonsterMode = "isOpenMonsterMode";
    //---------------------------------------------IronSoure--------------------------------------------------//
    //---------------------------------------------FireBase--------------------------------------------------//
}
