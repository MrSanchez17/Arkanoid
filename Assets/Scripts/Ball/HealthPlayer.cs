using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour
{
    public static HealthPlayer Instance { get; private set; }

    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] TMP_Text texto;
    [SerializeField] public string firstScene;

    [SerializeField] GameObject LoseScene;
    Ball2 ball;
    Score score;
    [SerializeField] AudioSource losingBall;

    private void Awake()
    {
        ball = GetComponentInChildren<Ball2>();
        score = GetComponent<Score>();
        losingBall = GetComponentInChildren<AudioSource>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        currentHealth = maxHealth;    
    }

    private void Update()
    {
        texto.text = $"Health: {currentHealth} ";
    }

    public void LoseLife()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            LoseScene.SetActive(true);
            Invoke("RestartGame", 5f);
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void SoundsDead()
    {
        losingBall.Play();
        Debug.Log("lose");
    }
    public void AddingHealth()
    {
        currentHealth = currentHealth + 1 ;    
    }
}
