using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private bool invencible = false;
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float screenPadding = 1;
    [SerializeField] private int initialHealth = 1000;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private float projectileFirePeriod = 0.1f;

    [Header("FX")]
    [SerializeField] private GameObject playerHitVFX = null;
    [SerializeField] private GameObject deathVFX = null;
    [SerializeField] private float durationOfExplosion = 1;
    [SerializeField] private AudioClip playerHitSound = null;
    [SerializeField] [Range(0, 1)] private float playerHitSoundVolume = 0.7f;
    [SerializeField] private AudioClip deathSound = null;
    [SerializeField] [Range(0, 1)] private float deathSoundVolume = 0.7f;
    [SerializeField] private AudioClip shootSound = null;
    [SerializeField] [Range(0, 1)] private float shootSoundVolume = 0.1f;

    float xMin, xMax, yMin, yMax;
    Coroutine firingCoroutine;
    HealthManager healthManager;

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager == null ) {
            Debug.LogError("Unable to find healthManager");
            return;
        }
        SetupMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    public int GetInitialHealth() => initialHealth;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ProcessHit(collider.gameObject.GetComponent<DamageDealer>());
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (!damageDealer)
        {
            Debug.LogWarning("damageDealer null");
            return;
        }
        healthManager.Hit(damageDealer.GetDamage());
        if (healthManager.GetHealth() <= 0)
        {
            if (invencible)
            {
                return;
            }
            Die();
            return;
        }
        PlayHitFx();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                    laserPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    private void SetupMoveBoundaries()
    {
        var camera = Camera.main;
        var min = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var max = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        xMin = min.x + screenPadding;
        xMax = max.x - screenPadding;
        yMin = min.y + screenPadding;
        yMax = max.y - screenPadding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Die()
    {
        FindObjectOfType<LevelManager>().LoadGameOver();
        PlayDeathFx();
        Destroy(gameObject);
    }

    private void PlayHitFx()
    {
        if (playerHitSound)
        {
            AudioSource.PlayClipAtPoint(playerHitSound, Camera.main.transform.position, playerHitSoundVolume);
        }
        if (playerHitVFX)
        {
            var explosion = Instantiate(playerHitVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
        }
    }

    private void PlayDeathFx()
    {
        if (deathSound)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }
        if (deathVFX)
        {
            var explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
        }
    }

}
