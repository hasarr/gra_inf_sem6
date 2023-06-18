using UnityEngine;

public class BuffGenerator : MonoBehaviour
{
    public GameObject buffPrefab;
    public float spawnInterval = 20f;
    private float timer = 0f;
    private float minX, maxX, maxY; // Granice ekranu
    public float spawnForce = 6f; // Siła wyrzutu buffa

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        CalculateScreenBounds();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBuff();
            timer = 0f;
        }
    }

    private void CalculateScreenBounds()
    {
        float camDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        minX = bottomLeft.x;
        maxX = topRight.x;
    }

    private void SpawnBuff()
    {
        float randomX = Random.Range(minX, maxX); // Losowa pozycja X dla buffa
        Vector3 spawnPosition = new Vector3(randomX, maxY, 0f); // Początkowa pozycja buffa
        GameObject buff = Instantiate(buffPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = buff.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * spawnForce, ForceMode2D.Impulse);
        Destroy(buff, 3f);
    }
}
