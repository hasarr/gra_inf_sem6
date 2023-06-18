using UnityEngine;

public class PlayerLaserMovement : MonoBehaviour
{
    public float speed = 10f; // Prędkość poruszania lasera
    public Vector2 direction = Vector2.up; // Kierunek poruszania lasera
    public float lifetime = 3f; // Czas życia lasera

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
