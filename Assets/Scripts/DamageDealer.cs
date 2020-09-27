﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 100;

    public int GetDamage() => damage;

    void Hit()
    {
        Destroy(gameObject);
    }
}
