using System;
using UnityEngine;

[RequireComponent(typeof(VisibilityTest), typeof(SpriteRenderer))]
public class VisibilityShowSprite : MonoBehaviour
{
    private VisibilityTest _tester;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _tester = GetComponent<VisibilityTest>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _renderer.enabled = _tester.IsVisible;
    }
}