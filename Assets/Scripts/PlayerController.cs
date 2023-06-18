using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Renderer playerRenderer;
    public GameObject[] damageAssets;
    public int maxHealth = 4;
    public Color damageColor = new Color(1f, 0.4764151f, 0.4764151f, 1f);
    public TMPro.TextMeshProUGUI heartsText; // Pole tekstowe do wyœwietlania liczby serduszek

    private int currentHealth;
    private float flashDuration = 0.1f;
    private float flashTimer;
    private bool isFlashing;
    private GameManager gameManager;
    private Renderer shipRenderer;
    private GameObject ship;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        ship = GameObject.FindGameObjectWithTag("PlayerShip");
        shipRenderer = ship.GetComponent<Renderer>();

        currentHealth = maxHealth;

        gameManager = FindObjectOfType<GameManager>();

        UpdateHeartsText(); // Aktualizacja tekstu na pocz¹tku gry
    }

    private void Update()
    {
        if (isFlashing)
        {
            flashTimer += Time.deltaTime;
            shipRenderer.material.color = damageColor;

            if (flashTimer >= flashDuration)
            {
                isFlashing = false;
                shipRenderer.material.color = Color.white;
            }
        }
    }

    public void TakeDamage()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            flashTimer = 0f;

            currentHealth--;

            if (currentHealth > 0 && currentHealth < maxHealth)
            {
                GameObject damageAsset = Instantiate(damageAssets[currentHealth - 1], transform.position, Quaternion.identity);
                damageAsset.transform.parent = transform;
            }
            else if (currentHealth == 0)
            {
                gameManager.RestartGame();
            }

            UpdateHeartsText(); // Aktualizacja tekstu po otrzymaniu obra¿eñ
        }
    }

    private void UpdateHeartsText()
    {
        heartsText.text = currentHealth.ToString(); // Aktualizacja tekstu na podstawie aktualnej liczby serduszek
    }
}
