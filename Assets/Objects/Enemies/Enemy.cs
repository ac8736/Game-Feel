using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public bool leftArrow = false;
    public bool rightArrow = false;
    public bool upArrow = false;
    public bool downArrow = false;
    public GameObject particle;

    private Shake shake;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(-5f, 0f);
        transform.rotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<Shake>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.LeftArrow) && leftArrow)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if (Input.GetKey(KeyCode.RightArrow) && rightArrow)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if (Input.GetKey(KeyCode.UpArrow) && upArrow)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if (Input.GetKey(KeyCode.DownArrow) && downArrow)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            GlobalVars.health -= 10;
            shake.shake();
            Destroy(gameObject);
        }
    }
}
