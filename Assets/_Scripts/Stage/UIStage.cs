using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Lean.Touch;
using UnityEngine.SceneManagement;
public class UIStage : MonoBehaviour
{
   // public bool isStateScores;

    UnityAction<BaseEventData, CharacterData> callbackBeginDrag;
    List<CharacterData> characDataListDonotInAblum;
    [SerializeField] TextMeshProUGUI countMonsterDontDragText;
    [SerializeField] StateCanvasGroup monsterSpawnSCG;
    [SerializeField] TextMeshProUGUI valueMonsterInAlbumText;
    [SerializeField] EventTrigger monsterSpawnEventTrigger;
    [SerializeField] Button DressUpButton;
    [SerializeField] Button MonsterModeButton;
   
    [SerializeField] GameObject LockOfMonsterPlayMode;
    [SerializeField] Button AddNewLimmitedModel;
    [SerializeField] Sprite NotFullModelSprite;
    [SerializeField] Sprite FullModelSprite;
    public GameObject deleteObject;
    [SerializeField] Button removeCharacterButton;
    public GameObject CharacterDoNotDragPanel;
    public GameObject StageLimitedPopUp;
    public ComfirmDeleteModelPopup ComfirmDeletePopup;
    public PopUpMonsterModeController popUpMonsterModeController;
    void Start()
    {
        EventTrigger.Entry entryBeginDrag = new EventTrigger.Entry();
        entryBeginDrag.eventID = EventTriggerType.BeginDrag;
        entryBeginDrag.callback.AddListener((data) =>
        {
            int countFinger = LeanTouch.Fingers.Count;
            if (countFinger >= 3)
            {
                return;
            }
            monsterSpawnSCG.SetState(false);
            callbackBeginDrag?.Invoke(data, characDataListDonotInAblum[^1]);
            //if (PlayerPrefs.GetInt(DataGame.isCompleteTutorialAlbum, 0) == 0)
            //{
            //    GameManager.Instance.tutorialManager.HideHand();
            //    //PlayerPrefs.SetInt(DataGame.isCompleteTutorialAlbum, 1);
            //    //PlayerPrefs.Save();
            //}
        });
        monsterSpawnEventTrigger.triggers.Add(entryBeginDrag);

        DressUpButton.onClick.AddListener(() => {
            GameManager.THIS.TurnOfCurrentPhase();
            GameManager.THIS.ChangePhaseYesNoPlay();
        });

        removeCharacterButton.onClick.AddListener(() =>
        {
            GameManager.THIS.stageManager.RemoveCharacterInWait(GameManager.THIS.stageManager.characterDatasDonotInStage[^1]);
            if (GameManager.THIS.stageManager.characterDatasDonotInStage.Count == 0)
            {
                CharacterDoNotDragPanel.SetActive(false);
            }
            else
            {
                GameManager.THIS.stageManager.characterManagerInCardSpawn.HandleShowAlbum(GameManager.THIS.stageManager.characterDatasDonotInStage[^1]);
                countMonsterDontDragText.text = GameManager.THIS.stageManager.characterDatasDonotInStage.Count.ToString();
            }
        });

        AddNewLimmitedModel.onClick.AddListener(()=> {
            GameManager.THIS.uIStage.StageLimitedPopUp.SetActive(true);
        });

        MonsterModeButton.onClick.AddListener(()=> {
            if (PlayerPrefs.GetInt(DataGame.NumberPlay, 0) <= 3)
            {
                ComonPopUpController.THIS.Container.SetActive(true);
                ComonPopUpController.THIS.inforText.text = "Play Play Dress Up " + (3 - PlayerPrefs.GetInt("NumberPlay", 0)) + " times to unlock";
            }
            else
            {
                LoadingController.THIS.LoadingAction(() => {
                    SceneManager.LoadScene(1);
                });
            }
            
        });

    }

    public void CheckUIOfMonsterMode()
    {
        if (PlayerPrefs.GetInt(DataGame.NumberPlay, 0) <= 3)
        {
            return;
        }
        else
        {   
            if (PlayerPrefs.GetInt(DataGame.isOpenMonsterMode, 0) == 0 )
            {
                popUpMonsterModeController.gameObject.SetActive(true);

                
            }
            LockOfMonsterPlayMode.SetActive(false);
        }

    }

    public void UpdateAddNewLimmitedModelIcon(bool isFull)
    {
        if (isFull)
        {
            AddNewLimmitedModel.GetComponent<Image>().sprite = FullModelSprite;
            return;
            
        }
        AddNewLimmitedModel.GetComponent<Image>().sprite = NotFullModelSprite;
        
    }

    public void ShowValueMonsterInAlbum(string valueString)
    {
        valueMonsterInAlbumText.text = valueString;
    }

    public void ShowMonsterSpawn(List<CharacterData> characterDataListDonotInAblum, UnityAction<BaseEventData, CharacterData> callbackBeginDrag)
    {
        
        this.characDataListDonotInAblum = characterDataListDonotInAblum;
        this.callbackBeginDrag = callbackBeginDrag;

        countMonsterDontDragText.text = characDataListDonotInAblum.Count.ToString();

        monsterSpawnSCG.SetState(true);
     
    }

    public void HideMonsterSpawnSCG()
    {
        monsterSpawnSCG.SetState(false, 0);
    }
}
