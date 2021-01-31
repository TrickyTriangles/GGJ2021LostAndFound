using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //Vector3 startPos = transform.position;
        transform.Translate(Vector3.up * Time.deltaTime);

       // transform.position = startPos;
    }
}
