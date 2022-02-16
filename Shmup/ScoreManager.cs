using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float score;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    void Start()
    {
        instance = this;
    }
    public void AddScore(float scoreGained)
    {
        score += scoreGained;
        scoreTxt.text = score.ToString();
    }
}
