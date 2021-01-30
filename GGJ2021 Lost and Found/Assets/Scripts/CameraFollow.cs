using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float camera_speed;

    private void Update()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3();

            //newPosition.x = Mathf.MoveTowards(transform.position.x, target.transform.position.x, camera_speed * Time.deltaTime);
            //newPosition.y = Mathf.MoveTowards(transform.position.y, target.transform.position.y, camera_speed * Time.deltaTime);
            newPosition.x = Mathf.Lerp(transform.position.x, target.transform.position.x, 0.006f);
            newPosition.y = Mathf.Lerp(transform.position.y, target.transform.position.y, 0.006f);
            newPosition.z = transform.position.z;

            transform.position = newPosition;
        }
    }
}
