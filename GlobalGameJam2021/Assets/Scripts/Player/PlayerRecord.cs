using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerRecord : MonoBehaviour
    {
        private bool isRecording;
        private PlayerTrace trace;
        private PlayerControl _controller;

        public void StartRecord(PlayerTrace t)
        {
            trace = t;
            isRecording = true;
            _controller = GetComponent<PlayerControl>();
        }

        void FixedUpdate()
        {
            if (isRecording)
                trace.Add(transform.position, _controller.Direction);
        }

        internal PlayerTrace StopRecord()
        {
            isRecording = false;
            return trace;
        }
    }
}