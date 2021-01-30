using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Soul : MonoBehaviour
    {
        [SerializeField] private int index = 0;

        private PlayerTrace trace;
        private bool isMoving = false;
        private int frameCounter;

        // Start is called before the first frame update
        public void StartMove(PlayerTrace t)
        {
            trace = t;
            frameCounter = 0;
            isMoving = true;
            FixedUpdate();
            gameObject.SetActive(true);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isMoving)
            {
                var pos = trace.GetPos(frameCounter);
                var dir = trace.GetDir(frameCounter);

                transform.position = pos;
                //TODO copy direction;
                frameCounter++;
            }
        }

        public void StopMove()
        {
            isMoving = false;
            frameCounter = 0;
            gameObject.SetActive(false);
        }
    }
}