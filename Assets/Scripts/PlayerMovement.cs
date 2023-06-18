using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int numberOfLasers = 1;
    public float fireRate = 1.2f;
    public Transform firePoint;
    public GameObject laserPrefab;
    private float nextFireTime = 0f;
    private int playerLevel = 1;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = touch.position;
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                transform.position = new Vector3(worldTouchPosition.x, worldTouchPosition.y, transform.position.z);
            }
        }

        if (Input.touchCount > 0 && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Fire();
        }
    }

    void Fire()
    {
        if (playerLevel == 1)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
        else if (playerLevel >= 2)
        {
            Vector3 offset = new Vector3(0.2f, 0, 0);
            Instantiate(laserPrefab, transform.position - offset, Quaternion.identity);
            Instantiate(laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Buff"))
        {
            CollectBuff(other.gameObject);
        }
    }

    void CollectBuff(GameObject buff)
    {
        Destroy(buff);
        playerLevel++;
    }
}
