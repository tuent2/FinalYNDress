using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIComplete : MonoBehaviour
{
    public static UIComplete THIS;
    public GameObject ContentHolder;
    public Image ScreenShotImage;
    [SerializeField] GameObject ShareGameobject;
    [SerializeField] GameObject MainCompleteobject;
    [SerializeField] Button nextButton;
    [SerializeField] Button shareButton;
    [SerializeField] Button changeStageSenceButton;
    [SerializeField] SharePanelController sharePanel;
    [SerializeField] MainCharacterController characterManagerInCardSpawn;
    private Sprite ScreenShotSprite;
    [SerializeField] TextMeshProUGUI numberOfCharacterNeedDrag;
    [SerializeField] Button removeCharacterButton;
    [SerializeField] GameObject CharacterDoNotDragPanel;
    private void Awake()
    {
        THIS = this;
    }
    void Start()
    {
        
        changeStageSenceButton.onClick.AddListener(() => {
            GameManager.THIS.TurnOfCurrentPhase();
            GameManager.THIS.ChangePhaseStage();
        });
        shareButton.onClick.AddListener(() =>
        {
            sharePanel.UpdateImageShareAndSpirte(ScreenShotSprite);
            MainCompleteobject.SetActive(false);
            ShareGameobject.SetActive(true);
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
                UpdateCompleteUI();
            }
        });
        nextButton.onClick.AddListener(() => {
            GameManager.THIS.TurnOfCurrentPhase();
            GameManager.THIS.ChangePhaseYesNoPlay();
        });
    }
    
    public void Show()
    {
        GameManager.THIS.mainCharacterController.SaveMonsterData(5);
    }

    public void UpdateCompleteUI()
    {
        characterManagerInCardSpawn.HandleShowAlbum(GameManager.THIS.stageManager.characterDatasDonotInStage[^1]);

        numberOfCharacterNeedDrag.text = GameManager.THIS.stageManager.characterDatasDonotInStage.Count.ToString();

    }

    public void SetSripteForScreenShot(Sprite ScreenShot)
    {
        ScreenShotSprite = ScreenShot;
        ScreenShotImage.sprite = ScreenShotSprite;
    }
}
