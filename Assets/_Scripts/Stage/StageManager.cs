using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using DG.Tweening;
using Lean.Common;
using Lean.Touch;
using static Lean.Touch.LeanSpawnWithFinger;
public class StageManager : MonoBehaviour
{
    [Header("Controlls In Album")]
    public GameObject leanTouchObject;
    public GameObject pressToSelectObject;
    [Header("Refs In album")]
    public GameObject containerObject;
    [ConditionalHide] public CharactersData charactersData;
    public MainCharacterController characterManagerInCardSpawn;
    [ConditionalHide] [SerializeField] MainCharacterController characterManagerDragging;
    [SerializeField] CharacterManagerPooling characterManagerPooling;

    //[ConditionalHide] [SerializeField] List<CharacterTextScore> textScoreCharacters = new List<CharacterTextScore>();
    List<CharacterData> characterDatasInStage = new List<CharacterData>();
    public List<CharacterData> characterDatasDonotInStage = new List<CharacterData>();
    [ConditionalHide] public List<MainCharacterController> mainCharacterManagerInStage = new List<MainCharacterController>();
    
    //[SerializeField] Animator binAnimator;
    CharacterData characterDataDragging;
    //[SerializeField] GameObject[] notisNewCharacterDontInAlbum;

    [ConditionalHide] public int countMonsterOnAlbumMax;

    public bool isStateControllStage;
    [ConditionalHide] [SerializeField] private List<FingerData> fingerDatas;
    private static Stack<FingerData> fingerDataPool = new Stack<FingerData>();
    LeanSelectableByFinger selectableByFinger;

    // [SerializeField] LeanDragTranslate leanDragTranslateBG;
    // [SerializeField] LeanPinchScale leanPinchScaleBG;
    public UIStage uIStage;
    Camera cameraMain;
    Vector2 localPositionMonsterStartDrag;

    
    private void Awake()
    {
        InitData();
    }
    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerUp -= HandleFingerUp;
        //for (int i = 0; i < stageInLandAlbums.Length; i++)
        //{
        //    stageInLandAlbums[i].SaveSecondProcessing();
        //}
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        var fingerData = LeanFingerData.Find(fingerDatas, finger);
        if (fingerData == null)
        {
            return;
        }
        if (selectableByFinger)
        {
            selectableByFinger.Deselect();
            selectableByFinger = null;
        }
        LeanFingerData.Remove(fingerDatas, finger, fingerDataPool);
        //if (characterManagerDragging)
        //{
        //    characterManagerDragging.SetStateIdleInAlbum();
        //}
    }

    void Start()
    {
        for (int i = 0; i < characterDatasInStage.Count; i++)
        {
            var character = AddMonster(characterDatasInStage[i]);
            character.transform.localPosition = characterDatasInStage[i].posDropedInStage;
        }

        UpdateUIOfLimited();

        UpdateStateStage();


        if (cameraMain == null)
        {
            cameraMain = Camera.main;
        }
    }

    public void UpdateUIOfLimited()
    {
        countMonsterOnAlbumMax = PlayerPrefs.GetInt(DataGame.countMonsterOnAlbumMax, 3);

        int countMonsterOnAlbum = mainCharacterManagerInStage.Count;
        if (countMonsterOnAlbumMax == countMonsterOnAlbum)
        {
            uIStage.UpdateAddNewLimmitedModelIcon(true);
        }else
        {
            uIStage.UpdateAddNewLimmitedModelIcon(false);
        }
        uIStage.ShowValueMonsterInAlbum(countMonsterOnAlbum.ToString("00") + "/" + countMonsterOnAlbumMax.ToString("00"));
    }

    protected virtual void Update()
    {
        for (var i = fingerDatas.Count - 1; i >= 0; i--)
        {
            var fingerData = fingerDatas[i];

            if (fingerData.Clone != null)
            {
                UpdateSpawnedTransform(fingerData.Finger, fingerData.Clone);
            }
        }
    }

    private void UpdateSpawnedTransform(LeanFinger finger, Transform instance)
    {
        var screenPoint = finger.ScreenPosition;
        var worldPoint = cameraMain.ScreenToWorldPoint(screenPoint);
        instance.position = new Vector3(worldPoint.x, worldPoint.y, 0);
    }


    public void SaveMonsterData(CharacterData monsterData)
    {
        
        if (charactersData.characterDataMaked == null)
        {
            charactersData.characterDataMaked = new List<CharacterData>();
        }
        AddNewMonsterMaked(monsterData);
        GameManager.THIS.stageManager.charactersData.characterDataMaked.Add(monsterData);
        CharactersData.WriteFile(GameManager.THIS.stageManager.charactersData);
    }

    public void AddNewMonsterMaked(CharacterData monsterData)
    {
        characterDatasDonotInStage.Add(monsterData);
        Debug.Log("characterDatasDonotInStage: " + characterDatasDonotInStage.Count);
        //for (int i = 0; i < notisNewCharacterDontInAlbum.Length; i++)
        //{
        //    notisNewCharacterDontInAlbum[i].SetActive(characterDatasDonotInStage.Count > 0);
        //}
    }

    MainCharacterController AddMonster(CharacterData characterData)
    {
        
        var characterManager = characterManagerPooling.GetObjectInPooling();

        characterManager.HandleShowAlbum(characterData);

        characterManager.transform.position = GameManager.THIS.uIStage.CharacterDoNotDragPanel.transform.position;
        //var textScore = characterManager.GetComponent<CharacterTextScore>();
        // textScore.SetData(characterManager.characterData.scoreCharacter);
        // textScoreCharacters.Add(textScore);
        //textScore.SetState(uIStage.isStateScores);
        mainCharacterManagerInStage.Add(characterManager);
        
        return characterManager;
    }

   

    public void InitData()
    {
        string saveFile = DataGame.pathCharacterDataSave;
        charactersData = CharactersData.ReadFile(saveFile);

        if (charactersData == null)
        {
            charactersData = new CharactersData();
        }
        if (charactersData.characterDataMaked != null)
        {
            for (int i = 0; i < charactersData.characterDataMaked.Count; i++)
            {
                if (charactersData.characterDataMaked[i].isDropInStage == true)
                {
                    characterDatasInStage.Add(charactersData.characterDataMaked[i]);
                }
                else
                {
                    characterDatasDonotInStage.Add(charactersData.characterDataMaked[i]);
                }
            }
        }
        //for (int i = 0; i < notisNewCharacterDontInAlbum.Length; i++)
        //{
        //    notisNewCharacterDontInAlbum[i].SetActive(characterDatasDonotInStage.Count > 0);
        //}

        //countMonsterOnAlbumMax = countMonsterOnAlbumCenter;
        //if (rightLandInfor.SetDataInit())
        //    countMonsterOnAlbumMax += rightLandInfor.monsterAdd;


        //countMonsterOnAlbumMax = countMonsterOnAlbumCenter;
        //if (rightLandInfor.SetDataInit())
        //    countMonsterOnAlbumMax += rightLandInfor.monsterAdd;
        //if (leftLandInfor.SetDataInit())
        //    countMonsterOnAlbumMax += leftLandInfor.monsterAdd;
        //if (upLandInfor.SetDataInit())
        //    countMonsterOnAlbumMax += upLandInfor.monsterAdd;
        //if (downLandInfor.SetDataInit())
        //    countMonsterOnAlbumMax += downLandInfor.monsterAdd;

        //AlbumUI.isStateScores = PlayerPrefs.GetInt(DataGame.isShowStateScoreInAlbum, 1) == 1;
        //GameManager.Instance.albumUI.UpdateTextStateScore();

        //for (int i = 0; i < stageInLandAlbums.Length; i++)
        //{
        //    stageInLandAlbums[i].InitData();
        //}
    }

    public void UpdateStateStage()
    {
        HandleMonsterDontInAlbum();
        //UpdateScoresAlbum();
        //UpdateCrownCharactersAlbum();
    }

    

    void HandleMonsterDontInAlbum()
    {
        if (characterDatasDonotInStage.Count > 0
            && characterDataDragging != null)
        {
            characterDatasDonotInStage.Remove(characterDataDragging);
            characterDatasInStage.Add(characterDataDragging);
            characterDataDragging = null;
        }

        if (characterDatasDonotInStage.Count > 0)
        {
            characterManagerInCardSpawn.HandleShowAlbum(characterDatasDonotInStage[^1]);
            
            uIStage.ShowMonsterSpawn(characterDatasDonotInStage, (baseEventData, monsterData) =>
            {
                
                this.characterDataDragging = monsterData;
                characterManagerDragging = AddMonster(monsterData);
                characterManagerDragging.transform.position =  new Vector3 (0,0,0);
                if (LeanTouch.Fingers.Count == 0)
                {
                    return;
                }

                LeanFinger finger = LeanTouch.Fingers[LeanTouch.Fingers.Count - 1];
                var fingerData = default(FingerData);

                fingerData = LeanFingerData.FindOrCreate(ref fingerDatas, finger);

               // fingerData.Clone = characterManagerDragging.transform;

                var selectable = characterManagerDragging.GetComponent<LeanSelectable>();
                selectableByFinger = selectable as LeanSelectableByFinger;
                if (selectableByFinger != null)
                {
                    selectableByFinger.SelectSelf(finger);
                }
                //Debug.Log(123);
            });
           
        }
        else
        {
            uIStage.HideMonsterSpawnSCG();
        }
    }

    public void OnSelectMonster(LeanSelect leanSelect)
    {
        
        if (leanSelect && leanSelect.Selectables.Count > 0)
        {
            characterManagerDragging = leanSelect.Selectables[0].GetComponent<MainCharacterController>();
            
        }
        //leanDragTranslateBG.enabled = false;
        //leanPinchScaleBG.enabled = false;
        //landBoxsObject.SetActive(true);
        //characterManagerDragging.transform.localPosition;
        ShowBin();
        localPositionMonsterStartDrag = characterManagerDragging.transform.localPosition;
        //Vibration.Vibrate(DataGame.numberPowerVibration);

    }

    public void OnDisSelectMonster()
    {
        //leanDragTranslateBG.enabled = isStateControllStage;
       // leanPinchScaleBG.enabled = isStateControllStage;

        //landBoxsObject.SetActive(false);
        HideBin();
        if (mainCharacterManagerInStage.Count > countMonsterOnAlbumMax)
        {   
            if (PlayerPrefs.GetInt(DataGame.countMonsterOnAlbumMax,3) == 15 )
            {
                
            }
            GameManager.THIS.uIStage.StageLimitedPopUp.SetActive(true);
            mainCharacterManagerInStage.Remove(characterManagerDragging);
            characterDatasInStage.Remove(characterManagerDragging.characterData);
            characterDatasDonotInStage.AddFront(characterManagerDragging.characterData);
            //textScoreMonsters.Remove(characterManagerDragging.GetComponent<TextScoreMonster>());
            characterManagerDragging.gameObject.SetActive(false);
        }
        else
        {
            
           characterManagerDragging.characterData.UpdatePosDropedInStage(characterManagerDragging.transform.localPosition);
            int countMonsterOnAlbum = mainCharacterManagerInStage.Count;
            uIStage.ShowValueMonsterInAlbum(countMonsterOnAlbum.ToString("00") + "/" + countMonsterOnAlbumMax.ToString("00"));
        }
        UpdateStateStage();
        //UpdateStateLands();
        //for (int i = 0; i < notisNewMonsterDontInAlbum.Length; i++)
        //{
        //    notisNewMonsterDontInAlbum[i].SetActive(monsterDataListDontInAlbum.Count > 0);
        //}
        //if (PlayerPrefs.GetInt(DataGame.isCompleteShowTutorialNoti1Album, 0) == 0)
        //{
        //    bool isOnStage = characterManagerDragging.GetComponent<EffectDancingOnStage>().isMonsterOnStage;
        //    if (isOnStage)
        //    {
        //        GameManager.THIS.notificationPopup.ShowWithText(LocalizationManager.GetTranslation("TutorialNoti1"));
        //        PlayerPrefs.SetInt(DataGame.isCompleteShowTutorialNoti1Album, 1);
        //        PlayerPrefs.Save();
        //    }
        //}
    }

   

    public void ShowBin()
    {
        uIStage.deleteObject.SetActive(true);
    }
    public void HideBin()
    {
        uIStage.deleteObject.SetActive(false);
    }

    public void RemoveCharacterInStage(MainCharacterController characterManager)
    {
        //textScoreCharacters.Remove(characterManager.GetComponent<CharacterTextScore>());
        characterDatasInStage.Remove(characterManager.characterData);
        mainCharacterManagerInStage.Remove(characterManager);
        GameManager.THIS.stageManager.charactersData.characterDataMaked.Remove(characterManager.characterData);
    }

    public void RemoveCharacterInWait(CharacterData characterManager)
    {
        characterDatasDonotInStage.Remove(characterManager);
        GameManager.THIS.stageManager.charactersData.characterDataMaked.Remove(characterManager);
    }

    public void BackPositionCharacterDragWhenNotRemove()
    {
        characterManagerDragging.transform.localPosition = localPositionMonsterStartDrag;

    }

    public void Show()
    {
        containerObject.SetActive(true);
       
            //GameManager.THIS.uIStage.Show();
        SetStateControllInAlbum(true);
        UpdateStateStage();
    }

    public void SetStateControllInAlbum(bool isState)
    {
        isStateControllStage = isState;
        leanTouchObject.SetActive(isState);
        pressToSelectObject.SetActive(isState);
        //leanDragTranslateBG.enabled = isState;
        //leanPinchScaleBG.enabled = isState;
    }
}

[Serializable] 
public class CharacterData
{
    [ConditionalHide] public string id;
    [ConditionalHide] public string baseSkin;
    [ConditionalHide] public string hairSkin;
    [ConditionalHide] public string shirtSkin;
    [ConditionalHide] public string skirtSkin;
    [ConditionalHide] public string accSkin;
    [ConditionalHide] public string shoeSkin;
    [ConditionalHide] public string faceSkin;
    [ConditionalHide] public int scoreCharacter;
    [ConditionalHide] public bool isDropInStage;
    [ConditionalHide] public Vector2 posDropedInStage;

    public void UpdatePosDropedInStage(Vector2 pos)
    {
        isDropInStage = true;
        posDropedInStage = pos;
    }
}

[Serializable]
public class CharactersData
{
    [ConditionalHide] public List<CharacterData> characterDataMaked;
    public static CharactersData ReadFile(string path)
    {
        // Does the file exist?
        if (File.Exists(path))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(path);
#if !UNITY_EDITOR
            string jsonString = DecodeString(fileContents);
#else
            string jsonString = fileContents;
#endif
            CharactersData monstersData = JsonUtility.FromJson<CharactersData>(jsonString);
            // Work with JSON
            return monstersData;
        }
        else
        {
            return null;
        }
    }

    public static void WriteFile(CharactersData charactersData)
    {
        string saveFile = DataGame.pathCharacterDataSave;
        string jsonString = JsonUtility.ToJson(charactersData);
#if !UNITY_EDITOR
        string json = EncodeString(jsonString);
#else
        string json = jsonString;
#endif
        // Work with JSON

        // Write JSON to file.
        File.WriteAllText(saveFile, json);
    }
    public static string EncodeString(string value)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        return encodedText;
    }
    public static string DecodeString(string encodedText)
    {
        byte[] decodedBytes = Convert.FromBase64String(encodedText);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        return decodedText;
    }
}

