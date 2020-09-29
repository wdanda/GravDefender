using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health = 100;
    [SerializeField] protected int hitScore = 10;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float minTimeBetweenShots = 1f;
    [SerializeField] private float maxTimeBetweenShots = 2f;
    [SerializeField] private float projectileSpeed = 2f;

    [Header("FX")]
    [SerializeField] private GameObject deathVFX = null;
    [SerializeField] private float durationOfExplosion = 1;
    [SerializeField] private AudioClip deathSound = null;
    [SerializeField] [Range(0, 1)] private float deathSoundVolume = 0.7f;
    [SerializeField] private AudioClip shootSound = null;
    [SerializeField] [Range(0, 1)] private float shootSoundVolume = 0.1f;

    private float shotCounter = 0;
    private ScoreManager scoreManager = null;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null) {
            Debug.LogError("Enemy - Unable to find scoreManager");
        }
        UpdateShotCounter();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ProcessHit(collider.gameObject.GetComponent<DamageDealer>());
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
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        if (shootSound)
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (!damageDealer)
        {
            return;
        }
        health -= damageDealer.GetDamage();
        scoreManager.AddToScore(hitScore);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        var explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
