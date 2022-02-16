using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalNutsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private int globalNuts;

    private void OnEnable()
    {
        globalNuts = PlayerPrefs.GetInt("globalNuts");
        txt.text = PlayerPrefs.GetInt("globalNuts").ToString();
    }
}
