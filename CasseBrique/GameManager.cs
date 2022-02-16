using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentNoBall;
    private GameObject restartBall;
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private Transform paddle;
    public int life = 3;
    [SerializeField] private int restartoffset = 3;
    [SerializeField] private TextMeshProUGUI lifeTxt;
    [SerializeField] private GameObject gameOverScreen;
    
    void Start()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        lifeTxt.text = "Life :" + life.ToString();
        if (life == 0)
        {
            gameOverScreen.SetActive(true);
        }

        if ((life > 0) && (currentNoBall < 1))
        {
            StartCoroutine(NewStart());
        }

      
    }

    public void NewBall()
    {
        restartBall = Pooler.instance.Pop("Ball");
        restartBall.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + restartoffset, 0);
    }

    private IEnumerator NewStart()
    {
       NewBall();
        restartScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        restartScreen.SetActive(false);
    }

    public void ChangeNoBall(int databall)
    {
        currentNoBall = currentNoBall + databall;
    }
    
    public void ChangeNoLife(int datalife)
    {
        life = life + datalife;
    }
}
