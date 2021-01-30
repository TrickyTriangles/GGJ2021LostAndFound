using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealable : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust_particle;
    [SerializeField] private LevelManager level_manager;
    [SerializeField] private BoxCollider2D my_collider;
    [SerializeField] private BoxCollider2D goal_collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (!level_manager.GetIsGameComplete())
            {
                GameController.Instance.SetGameComplete(true);
                GameController.Instance.SetGameWon(true);
                level_manager.CompleteLevel();
            }
        }
    }

    public void StartParticle()
    {
        dust_particle.Play();
    }

    public void StopParticle()
    {
        dust_particle.Stop();
    }
}
