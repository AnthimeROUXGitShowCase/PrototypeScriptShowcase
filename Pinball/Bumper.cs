using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{

    [SerializeField] private Vector3 force;
    [SerializeField] private float strength = 200;
    private Vector3 startingScale;
    public AnimationCurve curve;
    public float curveEvolution;
    public float animationSpeed = 0.01f;
    [SerializeField] private int scoreAdded = 50;
    [SerializeField] private Gradient gradient;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameManager gM;
 

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision other)
    {
       Bounce(other);
    }

    void Bounce(Collision other)
    {
        if (other.gameObject.GetComponent<Ball>());
        {
            force = (other.transform.position - transform.position) * strength;
            force.y = 0;
            
            other.rigidbody.AddForce(force);
            ChangeScale();
            gM.AddScore(scoreAdded);
        }
    }

    void ChangeScale()
    {
        curveEvolution = 0;
        StartCoroutine(ChangeScaleCoroutine());
    }

    IEnumerator ChangeScaleCoroutine()
    {
        yield return new WaitForEndOfFrame();
        curveEvolution += animationSpeed;
        transform.localScale = startingScale * (1 + curve.Evaluate(curveEvolution));

        meshRenderer.sharedMaterial.color = gradient.Evaluate(curveEvolution);

        if (curveEvolution < 1)
        {
            StartCoroutine(ChangeScaleCoroutine());
        }
       

    }
}
