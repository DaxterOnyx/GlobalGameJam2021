using System;
using UnityEngine;

namespace Player
{
    public class PlayerObjective : MonoBehaviour
    {
        [SerializeField] private Transform pointer;
        
        public Transform Objective { get; set; }

        private void Update()
        {
            pointer.up = (Objective.position - pointer.position).normalized;
        }
    }
}