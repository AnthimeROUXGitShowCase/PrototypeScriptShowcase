using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Missille : MonoBehaviour
{
    [SerializeField] private Vector2 mouseMouvement;
    [SerializeField] private Transform missilleStart;
    [SerializeField] private float speed = 0.005f;
    [SerializeField] private float speedRotate = 0.1f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject missileSystem;
    

    private void OnEnable()
    {
        transform.position = missilleStart.position;
        transform.rotation = quaternion.identity;
        explosion.SetActive(false);
    }

    void Update()
    {
        mouseMouvement = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        transform.Translate(-transform.up * speed);

        gameObject.transform.Rotate(mouseMouvement.y *speedRotate, 0, mouseMouvement.x*speedRotate);
    }

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        missileSystem.SetActive(false);
    }
}
