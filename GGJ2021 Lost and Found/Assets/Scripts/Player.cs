using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        STAND,
        WALK,
        DRAG
    }

    private PlayerState state;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform grab_indicator;
    [SerializeField] private float walk_speed;
    [SerializeField] private float drag_speed;

    private void Awake()
    {
        if (grab_indicator != null) { grab_indicator.gameObject.SetActive(false); }
    }

    private void FixedUpdate()
    {
        Vector3 direction;
        HandleInput(out direction);

        transform.Translate(direction * walk_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stealable"))
        {
            grab_indicator.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stealable"))
        {
            grab_indicator.gameObject.SetActive(false);
        }
    }

    private void HandleInput(out Vector3 direction)
    {
        direction = new Vector3();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction.y = 1f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction.x = -1f;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction.y = -1f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction.x = 1f;
        }
    }
}
