using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public Animator animator;
    public string arrowKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(arrowKey))
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            animator.SetTrigger("click");
        } else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
