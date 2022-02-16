using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    [SerializeField] private int hp = 3;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private EventSystem ev;
    [SerializeField] private GameObject restartButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hp--;
            hpText.text = hp.ToString();
        }
    }

    private void LateUpdate()
    {
        if (hp == 0)
        {
            gameOverScore.text = "Score" + ScoreManager.instance.score;
            ev.firstSelectedGameObject = restartButton;
            gameOverScreen.SetActive(true);
        }
    }
}
