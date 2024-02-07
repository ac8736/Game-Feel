using System.Collections;
using Assets;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    AudioSource AudioSource;

    public float speed = 1.0f;
    private Transform player;
    private bool isDead;

    public AudioClip[] deathSounds = { };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AudioSource = GetComponent<AudioSource>();
        Vector3 direction = (player.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().IsInvincible) {
                ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
                var main = particle.main;
                main.startColor = Color.cyan;
            }
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Die");
        if (GameFeelConfig.config[GameFeelFeature.Particles])
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
        if (!GameController.GameOver)
        {
            GameController.self.Score += 100;
        }
        StartCoroutine(PostDeath());
    }

    public IEnumerator PostDeath()
    {
        AudioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
        AudioSource.volume = 0.8f;
        if (GameFeelConfig.config[GameFeelFeature.Audio])
        {
            AudioSource.Play();
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        AudioSource.Stop();
        yield return null;
    }
}
