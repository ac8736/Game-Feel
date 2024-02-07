using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    float up = 0f;
    float down = 0f;
    float left = 0f;
    float right = 0f;
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
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            up += Time.deltaTime;
        } else
        {
            up = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            down += Time.deltaTime;
        }
        else
        {
            down = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            left += Time.deltaTime;
        }
        else
        {
            left = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            right += Time.deltaTime;
        }
        else
        {
            right = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var incr = (int)(5f - (this.transform.position.x - collision.transform.position.x >= 0f ? this.transform.position.x - collision.transform.position.x : 2f)) * 50;
            var hit = false;
            hit = hit || (left > 0f && left <= 0.2f) && leftArrow;
            hit = hit || (right > 0f && right <= 0.2f) && rightArrow;
            hit = hit || (up > 0f && up <= 0.2f) && upArrow;
            hit = hit || (down > 0f && down <= 0.2f) && downArrow;
            if (hit) 
            {
                GlobalVars.score += incr;
                if (incr > 0) GlobalVars.combo++;
                else GlobalVars.combo = 0;
                Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            GlobalVars.combo = 0;
            Destroy(gameObject);
        }
    }
}
