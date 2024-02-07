using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int SpriteColorIndex = 0;

    SpriteRenderer SpriteRenderer;
    AudioSource AudioSource;
    Collider2D Collider;
    Rigidbody2D _rigidBody;

    public bool IsDead { get; private set; } = false; 

    private float cosX = 0;
    private float moveFreq = 2;
    private float moveHeight = 8;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
        AudioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        // Collider.excludeLayers = 1 << gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead && transform.localScale.magnitude > 0)
        {
            // Shrink on death
            // transform.localScale -= new Vector3(0.4f, 0.4f, 0.4f) * Time.deltaTime;
            if (transform.localScale.magnitude < 0)
            {
                transform.localScale = Vector3.zero;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !GameController.GameOver)
        {
            AudioSource.time = 0f;
            AudioSource.volume = 0.5f;
            AudioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 movementDirection = (cursorPos - transform.position).normalized;
        _rigidBody.velocity = movementDirection * 30f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collidable component;

        // Only proceed if the other object is an obstacle
        if (!collision.gameObject.TryGetComponent(out component))
        {
            return;
        }

        if (component.IsDead)
        {
            return;
        }

        IsDead = true;
        var rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1;
    }
}
