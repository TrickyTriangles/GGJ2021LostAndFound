using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        ACTIVE,
        INACTIVE
    }

    private PlayerState state;
    private Coroutine drag_routine;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform grab_indicator;
    [SerializeField] private float walk_speed;
    [SerializeField] private float drag_speed;
    private float speed;

    private void Awake()
    {
        if (!GameController.IsInitialized)
        {
            SceneManager.LoadScene(0);
        }

        if (grab_indicator != null) { grab_indicator.gameObject.SetActive(false); }

        speed = walk_speed;
        state = PlayerState.ACTIVE;
    }

    private void FixedUpdate()
    {
        if (state == PlayerState.ACTIVE)
        {
            Vector3 direction;
            HandleInput(out direction);

            transform.Translate(direction * speed * Time.deltaTime);
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

    /// <summary>
    /// Call this method whenever the player needs to enter a losing state / animation
    /// </summary>
    public void GetCaught()
    {
        // Other game over stuff here
        SetInactive();
    }

    public void SetActive()
    {
        state = PlayerState.ACTIVE;
    }

    public void SetInactive()
    {
        state = PlayerState.INACTIVE;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stealable") && drag_routine == null)
        {
            grab_indicator.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space) && drag_routine == null)
        {
            Stealable stealable_script = collision.GetComponent<Stealable>();
            drag_routine = StartCoroutine(DragRoutine(collision.gameObject, stealable_script));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stealable") && drag_routine == null)
        {
            grab_indicator.gameObject.SetActive(false);
        }
    }

    private IEnumerator DragRoutine(GameObject stealable, Stealable stealable_script)
    {
        Vector3 offset = stealable.transform.position - transform.position;
        stealable_script.StartParticle();
        grab_indicator.gameObject.SetActive(false);
        speed = drag_speed;

        while (Input.GetKey(KeyCode.Space))
        {
            stealable.transform.position = transform.position + offset;

            if (state == PlayerState.INACTIVE)
            {
                break;
            }

            yield return null;
        }

        drag_routine = null;
        speed = walk_speed;
        grab_indicator.gameObject.SetActive(true);
        stealable_script.StopParticle();
    }
}
