using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealable : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust_particle;
    [SerializeField] private LevelManager level_manager;
    private Vector3 last_pos;

    private void Start()
    {
        last_pos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Instance.SetGameComplete(true);
        GameController.Instance.SetGameWon(true);
        level_manager.CompleteLevel();
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
