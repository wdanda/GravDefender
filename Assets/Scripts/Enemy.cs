using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, FiringObject
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float minTimeBetweenShots = 1f;
    [SerializeField] private float maxTimeBetweenShots = 2f;
    [SerializeField] public float projectileSpeed = 2f;
    
    private float shotCounter = 0;

    void Start()
    {
        UpdateShotCounter();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            UpdateShotCounter();
        }
    }
    
    private void UpdateShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, minTimeBetweenShots + maxTimeBetweenShots);
    }

    private void Fire()
    {
        var spawnPosition = transform.position;
        spawnPosition.y += -1;
        var laser = Instantiate(
            laserPrefab,
            spawnPosition,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Laser>().SetFiringObject(this);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var laser = collider.gameObject.GetComponent<Laser>();
        if (!Utils.IsPlayerLaser(laser)) {
            Debug.Log("IGNORIN TRIGGER, NOT PLAYER LASER");
            return;
        }
        Debug.Log("PLAYER LASER HIT ENEMY!!!");
        ProcessHit(collider.gameObject.GetComponent<DamageDealer>());
    }

    

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
