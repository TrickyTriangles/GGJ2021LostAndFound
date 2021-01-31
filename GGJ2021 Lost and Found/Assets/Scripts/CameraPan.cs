using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float pan_speed;
    private Vector3 direction;

    private void Start()
    {
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * pan_speed * Time.deltaTime);

        if (transform.position.y > bounds.y)
        {
            transform.position = new Vector3(transform.position.x, bounds.y, transform.position.z);
            direction *= -1f;
        }
        else if (transform.position.y < bounds.x)
        {
            transform.position = new Vector3(transform.position.x, bounds.x, transform.position.z);
            direction *= -1f;
        }
    }
}
