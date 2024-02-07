using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    AudioSource AudioSource;

    public bool IsDead { get; private set; } = false;
    public float MoveSpeed = 1f;
    public float StopDistance = 0.5f;
    public float AlertDistance = 15f;
    public float ChaseCooldownLow = .13f;
    public float ChaseCooldownHigh = .63f;

    private float[] deathClipStartTimes = new []{ 0, 3f, 7f };
    private bool resting = false;

    // Start is called before the first frame update
    void Start()
    {

        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            transform.localScale *= 0.95f;
        }

        // Move towards player
        Vector2 playerPos = GameController.self.Player.transform.position;
        float distance = Vector2.Distance(transform.position, playerPos);

        // Decide whether to chase the player or not
        if (resting)
        {
            return;
        }
     
        if (distance >= StopDistance)
        {
            transform.position = Vector2.Lerp(transform.position, playerPos, MoveSpeed * Time.deltaTime);
        }

        // Take a break after chasing
        if (distance < StopDistance)
        {
            StartCoroutine(TakeABreak(Random.Range(ChaseCooldownLow, ChaseCooldownHigh)));
        }
    }

    public void Die()
    {
        if (IsDead) return;
        IsDead = true;
        GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(PostDeath());
    }

    public IEnumerator PostDeath()
    {
        
        AudioSource.time = deathClipStartTimes[Random.Range(0, deathClipStartTimes.Length)];
        // AudioSource.Play();
        AudioSource.pitch = 1.5f;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        AudioSource.Stop();
        yield return null;
    }

    public IEnumerator TakeABreak(float duration)
    {
        if (resting) yield return null;
        resting = true;
        yield return new WaitForSeconds(duration);
        resting = false;
    }
}
