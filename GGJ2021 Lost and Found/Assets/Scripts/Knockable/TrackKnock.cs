using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackKnock : MonoBehaviour
{
    public bool touchOnce = false;
    public bool touchTwice = false;
    private Coroutine wabble_routine;

    float rotate_dir = 1f;
    float rotate_degree = 30f;
    float lerp_speed = 0.2f;

    public void Wabble()
    {
        
        if (wabble_routine != null)
        {
            StopCoroutine(wabble_routine);
        }
        
        wabble_routine = StartCoroutine(WabbleThing());
    }

    public IEnumerator WabbleThing()
    {
        touchOnce = true;
        bool stillWabble = true;

        float current_rotation = 0f;
        while (stillWabble)
        {

            //Debug.Log(transform.rotation.z);

            float distance_to_target = (rotate_dir * rotate_degree) - current_rotation;
            // flip rotation direction
            if (Mathf.Abs(distance_to_target) < 0.1f)
            {
                rotate_dir *= -1f;
                rotate_degree -= 5f;
                lerp_speed += 0.02f;
            }
            /*
            else if(rotate_dir == 1f && current_rotation >= (rotate_dir * rotate_degree))
            {
                rotate_dir *= -1f;
                rotate_degree -= 5f;
                lerp_speed += 0.02f;
            }
            */
            current_rotation = Mathf.Lerp(current_rotation, rotate_dir * rotate_degree, lerp_speed);
            Quaternion new_rotation = Quaternion.Euler(0f, 0f, current_rotation);

            transform.rotation = new_rotation;

            if (rotate_degree < 0f) {stillWabble = false; }
            yield return null;

        }

    }
}
