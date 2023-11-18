using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhaseGame
{
    YesNoPlay,
    Complete,
    Album,
    Stage,
}


public class GameManager : MonoBehaviour
{
    public static GameManager THIS;

    [ConditionalHide] public PhaseGame phaseGame;

    [Header("UI GamePlay")]
    public UIYesNoPlay uIYesNoPlay;
    public UIComplete uIComplete;
    public UIStage uIStage;
    public LoadingController loadingController;
    [Header("UI DungChung")]
    public NotificationPopup notificationPopup;
    public QuestNotificationPopup questNotificationPopup;
    public OverlayCanvasController overlayCanvasController;
    [Header("Logic")]
    public YesNoPlayMananger yesNoPlayMananger;
    public StageManager stageManager;
    public CompleteManager completeManager;
    [Header("Character")]
    public MainCharacterController mainCharacterController;

    private void Awake()
    {
        THIS = this;
        //QualitySettings.vSyncCount = 0; // Disable VSync
        //Application.targetFrameRate = 60; // Set the target FPS

    }
    void Start()
    {
        
        SoundController.THIS.PlayInGameBGClip();

        if (PlayerPrefs.GetInt(DataGame.isDoneFirstTimePlay, 0) != 0)
        {
            TurnOfCurrentPhase();
            ChangePhaseStage();
            
        }
        else
        {
            TurnOfCurrentPhase();
            ChangePhaseYesNoPlay();
        }
    }

    public void ChangePhaseComplete()
    {       
        phaseGame = PhaseGame.Complete;
        
            HandlerPhaseChange();
       
    }

    public void ChangePhaseAlbum()
    {     
        phaseGame = PhaseGame.Album;
        LoadingController.THIS.LoadingAction(() => {
            HandlerPhaseChange();
        });
    }

    public void ChangePhaseYesNoPlay()
    {        
        phaseGame = PhaseGame.YesNoPlay;
        LoadingController.THIS.LoadingAction(() => {
            HandlerPhaseChange();
        });
    }

    public void ChangePhaseStage()
    {
        phaseGame = PhaseGame.Stage;
        LoadingController.THIS.LoadingAction(() => {
            HandlerPhaseChange();
        });
        
    }

    public void TurnOfCurrentPhase()
    {
        switch (phaseGame)
        {
            case PhaseGame.YesNoPlay:
                
                uIYesNoPlay.gameObject.SetActive(false);
                yesNoPlayMananger.gameObject.SetActive(false);
                YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Idle", true);
                break;
            case PhaseGame.Complete:
                uIComplete.ContentHolder.SetActive(false);
                completeManager.gameObject.SetActive(false);
                
                break;
            case PhaseGame.Album:

                break;
            case PhaseGame.Stage:
                uIStage.gameObject.SetActive(false);
                stageManager.containerObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void HandlerPhaseChange()
    {
        
        switch (phaseGame)
        {
            case PhaseGame.YesNoPlay:
                overlayCanvasController.buttonPanel.SetActive(true);
                uIYesNoPlay.gameObject.SetActive(true);
                yesNoPlayMananger.gameObject.SetActive(true);
                yesNoPlayMananger.SetUpToPlay();
                break;
            case PhaseGame.Complete:
                uIComplete.ContentHolder.SetActive(true);
                uIComplete.Show();
                uIComplete.UpdateCompleteUI();
                completeManager.gameObject.SetActive(true);
                SoundController.THIS.PlayInGameBGClip();
                break;
            case PhaseGame.Album:

                break;
            case PhaseGame.Stage:
                uIStage.gameObject.SetActive(true);
                //stageManager.gameObject.SetActive(true);
                stageManager.Show();
                overlayCanvasController.buttonPanel.SetActive(true);
                break;
            default:
                break;
        }
    }


    protected void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            CharactersData.WriteFile(stageManager.charactersData);
        }
    }

}
