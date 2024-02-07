using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ParticleSystem particles;

    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        if (!particles.isPaused && !GameFeelConfig.config[GameFeelFeature.Particles])
        {
            particles.Pause();
        }
        else if (particles.isPaused && GameFeelConfig.config[GameFeelFeature.Particles])
        {
            particles.Play();
        }
        Destroy(gameObject, 2.5f);
    }

    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            collision.gameObject.GetComponent<Collidable>().Die();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
