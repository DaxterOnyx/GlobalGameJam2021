using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VisibilityTest : MonoBehaviour
{
    [SerializeField] private List<Vector2> checkRelativePoints;
    [SerializeField] private Transform visibleFrom;
    [SerializeField] private float checkFrequency;

    public bool IsVisible { get; private set; }
    
    private void Start()
    {
        StartCoroutine(CheckVisibility());
    }

    IEnumerator CheckVisibility()
    {
        for (;;)
        {
            Vector2 from = visibleFrom.position;
            bool visible = false;

            foreach (var relativePoint in checkRelativePoints)
            {
                Vector2 dir = (Vector2)transform.position - ((Vector2)visibleFrom.position + relativePoint);
                var hit = Physics2D.Raycast(from, dir);
                if (hit.transform == transform)
                {
                    visible = true;
                }
            }

            IsVisible = visible;
            
            yield return new WaitForSeconds(1 / checkFrequency);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var relativePoint in checkRelativePoints)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + relativePoint, 0.1f);
        }
    }
}
