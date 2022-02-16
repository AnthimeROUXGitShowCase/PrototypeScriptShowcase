using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class WinSequence : MonoBehaviour
{
    [SerializeField] private LevelManager lvlManager;
    [SerializeField] private TextMeshProUGUI[] winInfo;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private int starsToSpawn;
    [SerializeField] private int spawnedStars;
    private int score;

    private void OnEnable()
    {
        winInfo[1].text = "Hi-Score " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
        starsToSpawn = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars");
    }


    private void FixedUpdate()
    {
        if ((score > lvlManager.requiredNuts[spawnedStars]) && (spawnedStars < 2))
        {
            stars[spawnedStars].SetActive(true);
            spawnedStars++;
            
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        score++;
        winInfo[0].text = "Score " + score;
    }
}
