using System;
using UnityEngine;

namespace Player
{
    public class SoulSteps : MonoBehaviour
    {
        [SerializeField] private GameObject footstep;
        [SerializeField] private float distBetweenSteps = 2;
        
        private Vector3 lastStepPos;
        
        private void Update()
        {
            Vector3 dep = lastStepPos - transform.position;
            
            if (dep.sqrMagnitude > distBetweenSteps * distBetweenSteps)
            {
                var inst_footstep = Instantiate(footstep, transform.position, transform.rotation);
                inst_footstep.transform.up = -dep.normalized;
                lastStepPos = transform.position;
            }
        }
    }
}