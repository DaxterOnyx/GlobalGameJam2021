using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerRecord : MonoBehaviour
    {
        private bool isRecording;
        private PlayerTrace trace;

        public void StartRecord(PlayerTrace t)
        {
            trace = t;
            isRecording = true;
        }

        void FixedUpdate()
        {
            if (isRecording)
                trace.Add(transform.position, transform.rotation * Vector3.forward);
        }

        internal PlayerTrace StopRecord()
        {
            isRecording = false;
            return trace;
        }
    }
}