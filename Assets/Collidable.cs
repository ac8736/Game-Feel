using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    AudioSource AudioSource;

    private Transform player;
    public float speed = 1.0f;

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
            GetComponent<Animator>().SetTrigger("Die");
            Destroy(gameObject, 0.3f);
        }
    }
}
