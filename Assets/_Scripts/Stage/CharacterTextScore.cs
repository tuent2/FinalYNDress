using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CharacterTextScore : MonoBehaviour
{
    public int score;
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] GameObject crownObject;
    public void SetData(int score)
    {
        this.score = score;
        string valueText = score.ToString("#,##0");
        scoreText.text = "$ " + valueText;
    }
    public void SetState(bool isState)
    {
        scoreText.gameObject.SetActive(isState);
    }
    public void SetStateCrown(bool isState)
    {
        crownObject.SetActive(isState);
    }
}
