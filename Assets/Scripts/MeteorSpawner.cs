using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] meteorPrefabs;
    public float spawnInterval = 1f;
    public float spawnForce = 5f;
    public float rotationSpeed = 100f;

    private float minX, maxX;

    private void Start()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomLeft.x;
        maxX = topRight.x;

        InvokeRepeating("SpawnMeteor", 0f, spawnInterval);
    }

    private void SpawnMeteor()
    {
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
        GameObject meteorPrefab = meteorPrefabs[randomIndex];

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * spawnForce, ForceMode2D.Impulse);

        RotateMeteor rotateScript = meteor.GetComponent<RotateMeteor>();
        if (rotateScript != null)
        {
            rotateScript.rotationSpeed = rotationSpeed;
        }

        Destroy(meteor, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        // Znalezienie komponentu PlayerController na obiekcie gracza
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        // Zadawanie obra¿eñ graczowi
        playerController.TakeDamage();
        
        // Zniszczenie meteorytu
        Destroy(gameObject);
    }
}
}
