using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : MonoBehaviour
{
    [SerializeField] private float walk_speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3[] route_nodes;
    [SerializeField] private Transform flashlight;
    [SerializeField] private float flashlight_swing;
    [SerializeField]private LevelManager level_manager;
    private bool is_active;
    private Coroutine guard_routine;

    private void Start()
    {
        is_active = true;
        guard_routine = StartCoroutine(GuardRoutine());
    }

    public void CatchPlayer()
    {
        if (guard_routine != null)
        {
            StopCoroutine(guard_routine);
            StartCoroutine(GuardSwingRoutine());
        }
    }

    private IEnumerator GuardRoutine()
    {
        if (route_nodes.Length > 0)
        {
            float timer = 0f;
            int active_node = 0;
            Vector3 distance_to_point = new Vector3();

            while (is_active)
            {
                transform.position = Vector3.MoveTowards(transform.position, route_nodes[active_node], walk_speed * Time.deltaTime);
                distance_to_point = route_nodes[active_node] - transform.position;

                animator.SetFloat("dirX", distance_to_point.normalized.x);
                animator.SetFloat("dirY", distance_to_point.normalized.y);

                if (distance_to_point.magnitude < 0.1f)
                {
                    active_node++;

                    if (active_node == route_nodes.Length)
                    {
                        active_node = 0;
                    }
                }

                // Flashlight rotation
                timer += Time.deltaTime;

                float new_angle = Mathf.Sin(timer * 2f) * flashlight_swing;
                Quaternion look_direction = Quaternion.LookRotation(Vector3.forward, distance_to_point.normalized);
                Quaternion new_rotation = Quaternion.Euler(0f, 0f, new_angle);
                flashlight.rotation = look_direction * new_rotation * Quaternion.Euler(0f, 0f, 90f);

                yield return null;
            }
        }
    }

    private IEnumerator GuardSwingRoutine()
    {
        float timer = 0f;
        animator.SetBool("caught_player", true);

        while (timer < animator.GetCurrentAnimatorStateInfo(0).length)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        level_manager.FailLevel();
    }
}
