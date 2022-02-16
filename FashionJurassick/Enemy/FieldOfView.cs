 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    [SerializeField] private Vector3 origin;
    [SerializeField] private float startingAngle;
    [SerializeField] private int rayCount;
    public bool playerSeen;
    public RoomAlarmManager AlarmManager;

    public Transform player;
    public Transform playerLastKnownPosition;

    public float cd; 

    private void Awake()
    {
        AlarmManager = GameObject.Find("RoomAlarmManager").GetComponent<RoomAlarmManager>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerLastKnownPosition = GameObject.Find("--PlayerLastKnownPosition--").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            Debug.DrawRay(origin, GetVectorFromAngle(angle) * viewDistance, Color.green);
            if (raycastHit2D.collider == null)  
            {
                // No hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                playerSeen = false;
                if (cd > 0f)
                {
                    cd -= Time.deltaTime;
                    playerLastKnownPosition.position = player.position;
                }
            }
            else
            {
                // Hit object
                vertex = raycastHit2D.point;
                
                Movement target = raycastHit2D.transform.GetComponent<Movement>();
                if ((target != null) && (target.isSommersaulting == false))
                {
                    AlarmManager.Alert = true;
                    AlarmManager.Normal = false;
                    AlarmManager.AlertCDCurrent = AlarmManager.AlertCD;
                    playerSeen = true;
                    playerLastKnownPosition.position = player.position;
                    cd = 2f;
                }
                else
                {
                    playerLastKnownPosition.position = playerLastKnownPosition.position;
                }

              
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

        
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }



    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
