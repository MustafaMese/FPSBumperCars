using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI scoreText;

    private int score;

    private void Start() 
    {
        score = FindObjectsOfType<BumperCar>().Length;
        SetScoreText();
    }

    private void SetScoreText()
    {
        string str = score.ToString() + " Cars Remaining";
        scoreText.text = str;
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void UpdateScore()
    {
        score--;

        if(score > 1)
            SetScoreText();
        else
        {
            var controller = FindObjectOfType<Controller>();
            if(controller.GetControllerType() == ControllerType.PLAYER)
                UIManager.Instance.OpenEndCanvas(false);
            else
                UIManager.Instance.OpenEndCanvas(true);
        }
    }
}
