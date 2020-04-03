using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float length;
    public float movespeed;
    //private float mov;
    private float startY;
    //private Rigidbody2D rb2d;
    private bool faceUp = true;
    private void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        // mov = movespeed;
    }
    private void Update()
    {
        if (transform.position.y - startY >= length)
        {
            faceUp = false;
        }
        if (transform.position.y - startY <= 0)
        {
            faceUp = true;
        }
        if (faceUp)
        {
            transform.Translate(Vector3.up * movespeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * movespeed * Time.deltaTime);
        }

    }
}
