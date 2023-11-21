using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ComonPopUpController : MonoBehaviour
{
    public static ComonPopUpController THIS;

    public GameObject Container;

    [SerializeField] Button CloseButton;
    [SerializeField] Button OkButton;
    public TextMeshProUGUI inforText;

    private void Awake()
    {
        if (THIS != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        CloseButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
        OkButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
