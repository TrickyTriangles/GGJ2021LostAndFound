using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealable : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust_particle;
    [SerializeField] private LevelManager level_manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (!level_manager.GetIsGameComplete())
            {
                level_manager.CompleteLevel();
            }
        }
    }

    public void StartParticle()
    {
        if (dust_particle != null)
        {
            dust_particle.Play();
        }
    }

    public void StopParticle()
    {
        if (dust_particle != null)
        {
            dust_particle.Stop();
        }
    }
}
