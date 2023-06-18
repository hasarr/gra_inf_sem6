using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Transform playerTransform;
    private GameObject spawnArea;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyManager enemyManager = spawnArea.GetComponent<EnemyManager>();

        if (other.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            enemyManager.EnemyDestroyed();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TakeDamage();
            }

            Destroy(gameObject);
            enemyManager.EnemyDestroyed();
        }
    }
}
