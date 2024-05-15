using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PalaMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f; // Velocidad de movimiento de la pala
    [SerializeField] float minX = -7f; // L�mite izquierdo del escenario
    [SerializeField] float maxX = 7f;  // L�mite derecho del escenario
    Rigidbody2D rb;
    Ball2 ballController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ballController = FindObjectOfType<Ball2>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }
    void MoveLeft()
    {
        if (transform.position.x > minX)
        {
            float moveAmount = -speed * Time.deltaTime;

            transform.Translate(Vector2.right * moveAmount);
        }
    }

    void MoveRight()
    {
        if (transform.position.x < maxX)
        {
            float moveAmount = speed * Time.deltaTime;
            transform.Translate(Vector2.right * moveAmount);
        }
    }
}