using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;


public class Ball2 : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform resetBall;
    [SerializeField] public string nextLevel;
    public AudioSource ballBouncing;

    HealthPlayer hp;

    Vector2 velocity;
    Rigidbody2D rb;

    NavigateTo nav;
    PositionConstraint pc;


    [SerializeField] GameObject winCondition;

    void Awake()
    {
        nav = GetComponent<NavigateTo>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponentInChildren<PositionConstraint>();
        hp = GetComponentInChildren<HealthPlayer>();
        ballBouncing = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        //Establece los limites de como se puede mover la pala

        if (transform.position.x < -8f || transform.position.x > 8f)
        {
            Vector2 currentVelocity = rb.velocity;
            rb.velocity = new Vector2(-currentVelocity.x, currentVelocity.y);
        }

        if (transform.position.y > 5f)
        {
            Vector2 currentVelocity = rb.velocity;
            rb.velocity = new Vector2(currentVelocity.x, -currentVelocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && pc.enabled == true)
        {
            GetComponentInChildren<PositionConstraint>().enabled = false;
            Vector2 direction = new Vector2(1, 1).normalized;
            rb.velocity = direction * speed;

        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextLevel);
        }
        if (!NoBricks())
        {
            StopMovement();
            winCondition.SetActive(true);
            Invoke("NextLevel", 5f);
        }
    }

    public void ResetBall()
    {
        transform.position = resetBall.position;
        pc.enabled = true;
        rb.velocity = Vector2.zero;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks")) // Destruye y suma 100 a los bloques de un solo golpe
        {
            Destroy(collision.gameObject);
            Score.Instance.AddPointsNormalBrick(Score.BlockType.NormalBrick);
            SoundsBouncing();
        }
        else if ((collision.gameObject.CompareTag("SilverBricks"))) // Destrye a los bloques de plata de dos golpes a partir del script SilverBrick y suma 200
        {
            SilverBrick silverBrick = collision.gameObject.GetComponent<SilverBrick>();

            if (silverBrick != null)
            {
                silverBrick.ReduceHits();
                SoundsBouncing();
            }
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            SoundsBouncing();
        }
        else if (collision.gameObject.CompareTag("Scenario"))
        {
            SoundsBouncing();
        }
        else if (collision.gameObject.CompareTag("GoldBrick"))
        {
            SoundsBouncing();
        }
    }
    bool NoBricks()
    {
        GameObject[] normalBrikcs = GameObject.FindGameObjectsWithTag("Bricks");
        GameObject[] SilverBrikcs = GameObject.FindGameObjectsWithTag("SilverBricks");

        return (normalBrikcs.Length + SilverBrikcs.Length) > 0;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void SoundsBouncing()
    {
        Debug.Log("sonando");
        ballBouncing.Play();
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            hp.LoseLife();
            ResetBall();
            hp.SoundsDead();
        }
    }

}

