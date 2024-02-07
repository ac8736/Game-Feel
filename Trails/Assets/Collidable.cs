using System.Collections;
using System.Collections.Generic;
using Assets;
using Unity.VisualScripting;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    AudioSource AudioSource;

    public float speed = 1.0f;
    private Transform player;
    private bool isDead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AudioSource = GetComponent<AudioSource>();
        Vector3 direction = (player.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            transform.localScale *= 0.95f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        if (GameFeelConfig.config[GameFeelFeature.Particles])
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
        StartCoroutine(PostDeath());
    }

    public IEnumerator PostDeath()
    {
        // AudioSource.Play();
        AudioSource.pitch = 1.5f;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        AudioSource.Stop();
        yield return null;
    }
}
