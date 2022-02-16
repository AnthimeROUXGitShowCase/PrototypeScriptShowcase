using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Creeper : Monster
{
    [SerializeField] private float explosionForce;
    [SerializeField] private GameObject explosionParticle;
    
    private void FixedUpdate()
    {
        if (chaseMode == true)
        {
            if (distanceWithCurrentWaypoint < 3f)
            {
                StartCoroutine(ExplosionStart());
            }
        }
    }
    public override void OnEnable()
    {
        base.OnEnable();
        explosionParticle.SetActive(false);
    }
    
    private IEnumerator ExplosionStart()
    {
        soundPlayer.clip = MonsterSound[0];
        soundPlayer.Play();
        yield return new WaitForSeconds(3f);
        soundPlayer.clip = MonsterSound[1];
        soundPlayer.Play();
        explosionParticle.SetActive(true);
        if (distanceWithCurrentWaypoint < 3f)
        {
            player.GetComponent<PlayerHp>().ChangeHp(-10f);
            player.GetComponent<Rigidbody>().AddForce((player.transform.position -transform.position) * explosionForce,ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.5f);
        Died();
        Pooler.instance.DePop("Creeper", this.gameObject);
    }

    public override void Died()
    {
        roundManager.currentCreeper--;
        Pooler.instance.DePop("Creeper", this.gameObject);
    }


}
