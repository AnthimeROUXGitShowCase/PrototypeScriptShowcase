
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp = 100f;

    [SerializeField] private Image healthbar;

    private void Update()
    {
        healthbar.fillAmount = currentHp / maxHp;
    }

    public void ChangeHp(float hp)
    {
        currentHp += hp;
    }
}
