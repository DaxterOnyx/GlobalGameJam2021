using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visibility
{
    public class VisibilityTest : MonoBehaviour
    {
        [SerializeField] private List<Vector2> checkRelativePoints = new List<Vector2>();
        [SerializeField] private List<ViewerList> visibleBy;
        [SerializeField] private float checkFrequency = 8;
        
        public bool IsVisible { get; private set; }
        
        private void Start()
        {
            StartCoroutine(CheckVisibility());
        }
    
        IEnumerator CheckVisibility()
        {
            for (;;)
            {
                var visible = false;
                
                foreach (var viewerList in visibleBy)
                {
                    foreach (var viewer in viewerList.viewers)
                    {
                        foreach (var relativePoint in checkRelativePoints)
                        {
                            // points from transform center
                            var point = (Vector2)transform.TransformPoint(relativePoint);
    
                            // computing direction and angle from viewer to point
                            var dir = point - (Vector2)viewer.transform.position;
                    
                            // skip too far
                            if(dir.sqrMagnitude > viewer.maxDistance * viewer.maxDistance) continue;
                    
                            // skipping points not in angle
                            var angle = Vector2.Angle(viewer.transform.up, dir);
                            if (Mathf.Abs(angle) > viewer.viewAngle / 2) continue;
                    
                            // raycast from target to viewer, so only viewer need a collider
                            var hit = Physics2D.Raycast(point, -dir);
                            if (hit.transform == viewer.transform) visible = true;
                        }
                    }
                }
    
                IsVisible = visible;
                
                yield return new WaitForSeconds(1 / checkFrequency);
            }
        }
    
        private void OnDrawGizmosSelected()
        {
            foreach (var relativePoint in checkRelativePoints)
            {
                Gizmos.DrawWireSphere((Vector2)transform.TransformPoint(relativePoint), 0.1f);
            }
        }
    }
}

