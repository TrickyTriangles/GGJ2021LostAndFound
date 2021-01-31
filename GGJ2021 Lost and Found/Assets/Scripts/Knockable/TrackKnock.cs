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
    float lerp_speed = 0.04f;

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

        while(stillWabble)
        {
            float current_rotation = transform.rotation.z;
            // flip rotation direction
            if(rotate_dir == 1f && current_rotation > (rotate_dir * rotate_degree))
            {
                rotate_dir *= -1f;
                rotate_degree -= 5f;
                lerp_speed += 0.02f;
            }
            else if(rotate_dir == -1f && current_rotation < (rotate_dir * rotate_degree))
            {
                rotate_dir *= -1f;
                rotate_degree -= 5f;
                lerp_speed += 0.02f;
            }
            float new_angle = Mathf.Lerp(transform.rotation.z, rotate_dir * rotate_degree, lerp_speed);
            Quaternion new_rotation = Quaternion.Euler(0f, 0f, new_angle);

            transform.rotation = new_rotation;

            if (rotate_degree < 0f) {stillWabble = false; }
            yield return null;

        }

    }
}
