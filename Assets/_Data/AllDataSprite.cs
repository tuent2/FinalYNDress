using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using NaughtyAttributes;
using System.Text.RegularExpressions;
[CreateAssetMenu(fileName = "AllDataSprite", menuName = "ScriptableObjects/All Data Sprite", order = 3)]
public class AllDataSprite : ScriptableObject
{
    public List<Sprite> hairSprite;
    public List<Sprite> shirtSprite;
    public List<Sprite> skirtSprite;
    public List<Sprite> accSprite;
    public List<Sprite> shoeSprite;
    public List<Sprite> faceSprite;

#if UNITY_EDITOR
    [Button("GetDataSprite")]
    public void ImportSpriteSkin()
    {
        hairSprite.Clear();
        shirtSprite.Clear();
        skirtSprite.Clear();
        accSprite.Clear();
        shoeSprite.Clear();
        faceSprite.Clear();
        hairSprite = AssetDatabase.LoadAllAssetsAtPath(DataGame.pathSpriteHair)
            .OfType<Sprite>().ToList();
        //List<Sprite> spritesHeads2 = AssetDatabase.LoadAllAssetsAtPath(DataGame)
        //    .OfType<Sprite>().ToList();
        //List<Sprite> spritesHeads3 = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHeads3)
        //    .OfType<Sprite>().ToList();
        //spritesHeads.AddRange(spritesHeads2);
        //spritesHeads.AddRange(spritesHeads3);
        shirtSprite = AssetDatabase.LoadAllAssetsAtPath(DataGame.pathSpriteShirt)
            .OfType<Sprite>().ToList();
        skirtSprite = AssetDatabase.LoadAllAssetsAtPath(DataGame.pathSpriteSkirt)
            .OfType<Sprite>().ToList();
        accSprite = AssetDatabase.LoadAllAssetsAtPath(DataGame.pathSpriteAcc)
            .OfType<Sprite>().ToList();
        shoeSprite = AssetDatabase.LoadAllAssetsAtPath(DataGame.pathSpriteShoes)
            .OfType<Sprite>().ToList();
        faceSprite = AssetDatabase.LoadAllAssetsAtPath(DataGame.pathSpriteFace)
            .OfType<Sprite>().ToList();
    }

    //[Button("SortDataSprite")]
    //public void SortDataSprite()
    //{
    //    hairSprite.Sort();
    //    shirtSprite = shirtSprite.OrderBy(sprite => sprite.name).ToList();
    //    skirtSprite = skirtSprite.OrderBy(sprite => sprite.name).ToList();
    //    accSprite = accSprite.OrderBy(sprite => sprite.name).ToList();
    //    shoeSprite = shoeSprite.OrderBy(sprite => sprite.name).ToList();
    //    faceSprite = faceSprite.OrderBy(sprite => sprite.name).ToList();
    //}

#endif
}


