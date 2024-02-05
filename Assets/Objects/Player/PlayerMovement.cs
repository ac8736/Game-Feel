using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] jumpSpots;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = jumpSpots[0].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = jumpSpots[1].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = jumpSpots[2].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = jumpSpots[3].transform.position;
        }
    }
}
