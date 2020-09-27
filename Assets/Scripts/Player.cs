﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 10;
    [SerializeField] public float screenPadding = 1;
    [SerializeField] public GameObject laserPrefab;
    [SerializeField] public float projectileSpeed = 10;
    [SerializeField] public float projectileFirePeriod = 0.1f;

    float xMin, xMax, yMin, yMax;
    Coroutine firingCoroutine;

    void Start()
    {
        SetupMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
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
        while(true) {
            GameObject laser = Instantiate(
                    laserPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
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

}