using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;


public class EnemyAi : MonoBehaviour{
    [SerializeField] public Transform target;
    [SerializeField] public FieldOfView fov;
    [SerializeField] private Transform pFov;
    [SerializeField] private float fovSize;
    [SerializeField] float fovWhenAlert;
    public Transform[] patrolWaypoint;
    public int currentWaypoint;
    [SerializeField] int MaxWaypoint;
    public AIDestinationSetter destination;
    public Transform player;
    public Transform playerLastKnownPosition;
    //private Vector3 lastMoveDir;
    private Vector3 aimDirection;
    public Room isInRoomNo;
    public RoomAlarmManager alarmManager;
    public AIPath pathfinding;
    public float movementSpeed;
    public float normalSpeed = 1f;
    public bool PLKP;
    public Transform lookThisWay;
 

    [SerializeField] private bool lookLeft;
    [SerializeField] private bool lookRight;
    private float leftCD = 2f;
    [SerializeField] private float leftCurrentCD;
    private float rightCD = 2f;
    [SerializeField] private float rightCurrentCD;
    [SerializeField]  private int leftOrRight;
    [SerializeField] private bool randomThrow;
    private void Awake()
    {
     
        fov = Instantiate(pFov, null).GetComponent<FieldOfView>();
        target = patrolWaypoint[currentWaypoint];
        MaxWaypoint = patrolWaypoint.Length;    

        player = GameObject.Find("Player").GetComponent<Transform>();
        playerLastKnownPosition = GameObject.Find("--PlayerLastKnownPosition--").GetComponent<Transform>();
        pathfinding.maxSpeed = movementSpeed;
        alarmManager = GameObject.Find("RoomAlarmManager").GetComponent<RoomAlarmManager>();
        //lastMoveDir = aimDirection;

    }
    
    public void OnTriggerEnter2D(Collider2D other) 
    {
      
     
    }
   

    private void Update()
    {
        Vector3 targetDir = (target.transform.position - transform.position).normalized;

        if ((alarmManager.Alert) && (isInRoomNo.playerIsInRoom) && (isInRoomNo.playerSpotted))
        {
            foreach (EnemyAi enemy in isInRoomNo.enemyInRoom)
            {
                enemy.target = enemy.playerLastKnownPosition;

            }
        }

        if (MaxWaypoint == currentWaypoint)
        {
            currentWaypoint = 0;
            target = patrolWaypoint[currentWaypoint];
            destination.target = target;
        }

        if (fov != null)
        {
            fov.SetOrigin(transform.position);
            fov.SetAimDirection(targetDir);
        }

        //if (fov.playerSeen == true)

        //if ((fov.playerSeen == false) && (cooldown < 0f))

    }

    public void AlertMode()
    {
        if ((fov.playerSeen) && (fov.cd > 0f))
        {
            target = playerLastKnownPosition;
        }

 

        destination.target = playerLastKnownPosition;
        isInRoomNo.playerSpotted = true;
        alarmManager.Alert = true;
        fov.SetFoV(fovWhenAlert);
        if (PLKP) 
        { 
            LookAround();
            
   
           
           if (fov.playerSeen)
           {
                PLKP = false;
                pathfinding.maxSpeed = normalSpeed;
                lookRight = false;
                lookLeft = false;
                randomThrow = false;

                 

           }
          
        }
        

    }

    public void GoingBackToNormalPatrol()
    {
        pathfinding.maxSpeed = normalSpeed;
        destination.target = patrolWaypoint[currentWaypoint];
        target = patrolWaypoint[currentWaypoint];
        isInRoomNo.playerSpotted = false;
        fov.SetFoV(fovSize);
    }

 
    private void LookAround()
    {
      
        pathfinding.maxSpeed = 0; 
        target = lookThisWay;
        if  (!randomThrow)
        {
            leftOrRight = Random.Range(1, 2);
            randomThrow = true;
            leftCurrentCD = leftCD;
            rightCurrentCD = rightCD;
        }
            
      

        if ((leftOrRight == 1) && (!lookLeft))
        {
            LookLeft();
            if (leftCurrentCD < 0f)
            {
                lookLeft = true;
                if (!lookRight)
                {
                    leftOrRight = 2;
                }
            }
            
        }
    
        if ((leftOrRight == 2) && (!lookRight))
        {
            LookRight();
            if (rightCurrentCD < 0f)
            {
                lookRight = true;
                if (!lookLeft)
                {
                    leftOrRight = 1;
                }
            }


        }

        
       
        if (lookLeft && lookRight)
        {
            GoingBackToNormalPatrol();
            PLKP = false; 
            lookRight = false;
            lookLeft = false;
            randomThrow = false;
        }
   
    

    }

    private void LookLeft()
    {
        lookThisWay.transform.RotateAround(gameObject.transform.position, Vector3.forward, 0.3f);
        leftCurrentCD -= Time.deltaTime;
        
    }
    private void LookRight()
    {
        lookThisWay.transform.RotateAround(gameObject.transform.position, Vector3.forward, -0.3f);
        rightCurrentCD -= Time.deltaTime;
    }

    public void SchearchMode()
    {
        fov.SetFoV(fovWhenAlert);
    }

    

}
