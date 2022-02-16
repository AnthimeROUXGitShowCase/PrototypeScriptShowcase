using System;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int[] requiredNuts;
    [SerializeField] public int currentNuts;
    [SerializeField] public bool lvlHasTime = true;
    private float currentTime;
    [HideInInspector]
    public float lvlDuration;   
    [HideInInspector]
    public int noNutsInLVL;
    [HideInInspector]
    public Transform nutsParent;
    [SerializeField] private TextMeshProUGUI currentNutsTxt;
    [SerializeField] private TextMeshProUGUI currentTimeTxt;
    [SerializeField] private TextMeshProUGUI currentHpTxt;
    [SerializeField] private GameObject Ui;
    [SerializeField] private GameObject[] gameState;
    private String lvlNo;
    private int stars;
    
    void Awake()
    {
        lvlNo = SceneManager.GetActiveScene().name;
        instance = this;
        if (lvlHasTime)
        {
            currentTime = lvlDuration;
        }
        else
        {
            noNutsInLVL = nutsParent.childCount;
        }

    }
    
    
    public void AddNuts()
    {
        currentNuts++;
    }

    private void LateUpdate()
    {

        if ((currentTime < 0) && (lvlHasTime) || (!lvlHasTime) && (noNutsInLVL == 0))
        {
            if (currentNuts > requiredNuts[0])
            {
                if (!gameState[1].active)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (currentNuts > requiredNuts[x])
                        {
                            stars = x + 1;
                            if (PlayerPrefs.GetInt(lvlNo+"Stars") < stars)
                            {
                                PlayerPrefs.SetInt(lvlNo+"Stars",stars);
                            }
                        }
                    }
                    Win();
                }
            }
            else
            {
                Lose();
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
            currentNutsTxt.text = "nuts" + currentNuts;
            currentTimeTxt.text = "Time" + currentTime;
        }
    }

    public void HpTextUpdater(int hp)
    {
        currentHpTxt.text = "hp " + hp.ToString();
    }

    private void Win()
    {
        PlayerPrefs.SetInt("globalNuts",PlayerPrefs.GetInt("globalNuts") + currentNuts);
        if (currentNuts > PlayerPrefs.GetInt(lvlNo))
        {
            PlayerPrefs.SetInt(lvlNo,currentNuts);
        }
        Ui.SetActive(false);
        gameState[0].SetActive(false);
        gameState[1].SetActive(true);
    }

    public void Lose()
    {
        Pooler.instance.DePopAll();
        Ui.SetActive(false);
        gameState[0].SetActive(false);
        gameState[2].SetActive(true);
    }
}

