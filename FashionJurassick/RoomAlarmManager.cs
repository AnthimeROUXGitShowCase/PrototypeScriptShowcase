using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RoomAlarmManager : MonoBehaviour
{
    public Room[] roomList;
    public Camera cam;
    public Transform player;
    public Transform playerLastKnownPosition;

    public Vector2 maxPosition;
    public Vector2 minPosition;


    public bool Normal;
    public bool Schearch;
    public bool Alert;

    [SerializeField] public float AlertCD = 99f;
    [SerializeField] public float AlertCDCurrent;
    [SerializeField] private float SchearchCD = 99f;
    [SerializeField] private float SchearchCDCurrent;

    [SerializeField] private TextMeshProUGUI AlertText;
    [SerializeField] private TextMeshProUGUI CDText; 

     
    

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerLastKnownPosition = GameObject.Find("--PlayerLastKnownPosition--").GetComponent<Transform>();
        AlertText = GameObject.Find("StateText").GetComponent<TextMeshProUGUI>();
        CDText = GameObject.Find("CDState").GetComponent<TextMeshProUGUI>();
        AlertText.gameObject.SetActive(false);
        CDText.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Alert == true)
        {
            AlertText.gameObject.SetActive(true);
            CDText.gameObject.SetActive(true);

            AlertText.SetText("Alert");
            CDText.SetText(AlertCDCurrent.ToString());
            if (AlertCDCurrent > 0f)
            {
                AlertCDCurrent -= Time.deltaTime; 
            }
            else
            {
                Alert = false;
                Schearch = true;
                SchearchCDCurrent = SchearchCD; 
            }
        }

        if (Schearch == true)
        {
            AlertText.gameObject.SetActive(true);
            CDText.gameObject.SetActive(true);

            AlertText.SetText("Schearch");
            CDText.SetText(SchearchCDCurrent.ToString());

            if (SchearchCDCurrent> 0f)
            {
                SchearchCDCurrent -= Time.deltaTime;
            }
            else
            {
                Schearch = false;
                Normal = true; 
                SchearchCDCurrent = SchearchCD;
            }
        }


        if (Normal == true)
        {
            AlertText.gameObject.SetActive(false);
            CDText.gameObject.SetActive(false);
        }


        foreach (Room room in roomList)
        {
            if ((room.playerIsInRoom == true) && (Alert == true))
            {

                foreach (EnemyAi enemy in room.enemyInRoom)
                    enemy.AlertMode();
            }
            
            
            if (room.playerIsInRoom == false)
            {
                foreach (EnemyAi enemy in room.enemyInRoom)
                    enemy.GoingBackToNormalPatrol();
            }

            if ((room.playerIsInRoom == true) && (Schearch == true))
            {
                foreach (EnemyAi enemy in room.enemyInRoom)
                enemy.target = player;
                Alert = false; 

            }

            if ((room.playerIsInRoom == true) && (Normal == true))
            {
                foreach (EnemyAi enemy in room.enemyInRoom)
                enemy.GoingBackToNormalPatrol();
                Alert = false;
                Schearch = false;


            }
        }
    }

}
