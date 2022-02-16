using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LvlWorldMap : MonoBehaviour
{
    [SerializeField] private string[] lvlInfo;
    [SerializeField] private TextMeshProUGUI[] lvlInfoTxt;
    [SerializeField] private GameObject lvlInfoGo;
    [SerializeField] private GameObject[] stars;

    private void OnEnable()
    {
        for (int x = 0; x < lvlInfo.Length ; x++)
        {
            lvlInfoTxt[x].text = lvlInfo[x];
        }

        for (int y = 0; y < PlayerPrefs.GetInt(gameObject.name+"Stars"); y++)
        {
            stars[y].SetActive(true);
        }

        lvlInfoTxt[3].text = "Hi-Score " + PlayerPrefs.GetInt(gameObject.name);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        lvlInfoGo.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        lvlInfoGo.SetActive(false);
    }

    public void GoToLvl()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
