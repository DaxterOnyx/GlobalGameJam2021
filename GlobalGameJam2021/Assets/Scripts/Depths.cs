using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depths : MonoBehaviour
{
    private Camera _cam;
    private SpriteRenderer _renderer;
    
    void Start()
    {
        _cam = Camera.main;
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sortingOrder = (int)_cam.WorldToScreenPoint(gameObject.transform.position).y*-1;
    }
}
