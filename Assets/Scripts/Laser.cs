using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public FiringObject firingObject = null;

    public FiringObject GetFiringObject() => firingObject;

    public void SetFiringObject(FiringObject firingObject) {
        this.firingObject = firingObject;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}