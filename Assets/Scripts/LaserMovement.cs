using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public Vector2 movementDirection = Vector2.up;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
}