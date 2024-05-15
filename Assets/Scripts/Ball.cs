using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Vector2 velocity;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        velocity.x = Random.Range(-1f, 1f);
        velocity.y = 1;
        rb.AddForce(velocity * speed);
    }

    void Update()
    {
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

        //if (transform.position.y < -5f)
        //{
        //    ResetBall();
        //}

        //if (isPlaying) 
        //{
        //    rb.AddForce(transform.up * speed * Time.deltaTime);

        //}
    }

    //public void TheBallIsMoving()
    //{

    //    Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1).normalized;

    //    rb.AddForce(direction * speed);
    //}

    public void ResetBall()
    {
        transform.position = Vector2.zero;

        rb.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //float normalizedAngle;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            float hitAngle = (transform.position.x - collision.transform.position.x) * 5f;

        //    hitAngle = Mathf.Clamp(hitAngle, -60f, 60f);

        //    Vector2 direction = new Vector2(hitAngle, 1).normalized;

        //    rb.velocity = direction * speed;

        //    Vector2 contactPoint = collision.GetContact(0).point;
        //    normalizedAngle = contactPoint.x * 2;
        //    Debug.Log($" ,Punto de collision {collision.transform.InverseTransformPoint(contactPoint).x}");

        }
        //else
        //{
        //    float dot = Vector2.Dot(transform.up, Vector2.up);
        //    normalizedAngle = dot;
        //}

        //float desiredAngleRotation = Mathf.Lerp(0, 180, normalizedAngle);

        //Debug.Log($"DesiredAngleRotation: {desiredAngleRotation}");

        //transform.rotation = Quaternion.Euler(0, 0, desiredAngleRotation);




        if (collision.gameObject.CompareTag("Bricks")) // Destruye y suma 100 a los bloques de un solo golpe
        {
            Destroy(collision.gameObject);
            Score.Instance.AddPointsNormalBrick(Score.BlockType.NormalBrick);
        }
        if ((collision.gameObject.CompareTag("SilverBricks"))) // Destrye a los bloques de plata de dos golpes a partir del script SilverBrick y suma 200
        {
            SilverBrick silverBrick = collision.gameObject.GetComponent<SilverBrick>();

            if (silverBrick != null)
            {
                silverBrick.ReduceHits();
            }

        }
    }
}