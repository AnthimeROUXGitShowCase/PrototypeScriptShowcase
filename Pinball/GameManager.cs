using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int life;
    public static int currentBall;
    public float score;
    [SerializeField] private int highScore;

    [SerializeField] private TextMeshProUGUI textLife;
    [SerializeField] private TextMeshProUGUI textCurrentBall;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textHighScore;
    [SerializeField] private TextMeshProUGUI textCombo;

    private int currentCombo;
    [SerializeField] private float comboTimer;
    [SerializeField] private float comboTimerMax = 3f;
    public float comboMultiplier = 1f;


    private void Update()
    {
        if (currentCombo > 0)
        {
            if (comboTimer > 0f)
            {
                comboTimer -= Time.deltaTime;
            }
            else
            {
                currentCombo = 0;
                comboMultiplier = 1;
            }

        }
    }

    private void LateUpdate()
    {
        textLife.text = "Life : " + life;
        textCurrentBall.text = "Current Ball : " + currentBall;
        textScore.text = "Score : " + score;
        textHighScore.text = "HighScore : " + highScore;
        textCombo.text = "Combo x" + currentCombo;

    }


    public void AddScore(float scoreAdded)
    {
        currentCombo += 1;
        comboTimer = comboTimerMax;
        comboMultiplier = comboMultiplier * 2;
        score += scoreAdded * comboMultiplier;
    }
}