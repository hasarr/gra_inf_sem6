using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{
    public float moveSpeed = 2.4f;
    public float shootingInterval = 1.7f;
    public GameObject bulletPrefab;
    private float maxX, minX;
    private GameObject spawnArea;
    private float shootingTimer;

    private void Start()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        spawnArea = GameObject.FindGameObjectWithTag("SpawnArea");

        minX = bottomLeft.x;
        maxX = topRight.x;

        shootingTimer = shootingInterval;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (transform.position.x > maxX || transform.position.x < minX)
        {
            moveSpeed *= -1;
        }

        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            EnemyManager enemyManager = spawnArea.GetComponent<EnemyManager>();
            enemyManager.EnemyDestroyed();
        }
    }
}
