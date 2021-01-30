using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Soul : MonoBehaviour
    {
        [SerializeField] private int index = 0;
        [SerializeField] private Animator animator;
        
        private PlayerTrace trace;
        private bool isMoving = false;
        private int frameCounter;

        private Vector2 _pos;
        private Vector2 _dir;

        public void StartMove(PlayerTrace t)
        {
            trace = t;
            frameCounter = 0;
            isMoving = true;
            FixedUpdate();
            gameObject.SetActive(true);
            animator.SetBool("Walk", true);
        }
        
        public void StopMove()
        {
            isMoving = false;
            frameCounter = 0;
            animator.SetBool("Walk", false);
            gameObject.SetActive(false);
        }

        void FixedUpdate()
        {
            if (isMoving)
            {
                _pos = trace.GetPos(frameCounter);;
                _dir = trace.GetDir(frameCounter);
                
                transform.position = _pos;
                frameCounter++;
            }
        }

        private void Update()
        {
            animator.SetFloat("DirX", _dir.x);
            animator.SetFloat("DirY", _dir.y);
        }
    }
}