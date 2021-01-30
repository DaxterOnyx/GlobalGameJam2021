using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private bool constantMove = false;
    [SerializeField] private float threshold = 0.1f;
    private Rigidbody2D rigid;
    private bool moving = false;
    private Vector2 direction = Vector2.down;

    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            moving = true;
        if (moving && Input.GetMouseButtonUp(0))
            moving = false;
        
        direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)) - ((Vector2)transform.position);
        float dist = direction.magnitude;
        if(constantMove) direction.Normalize();
        Vector2 velocity = Vector2.zero;
        if (moving && dist > threshold)
        {
            velocity = direction * speed;
        }

        rigid.velocity = velocity;
    }
}