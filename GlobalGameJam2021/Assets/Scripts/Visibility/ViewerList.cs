using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visibility
{
    [CreateAssetMenu(fileName = "ViewerList", menuName = "SO/Visibility/ViewerList", order = 0)]
    public class ViewerList : ScriptableObject
    {
        public List<ViewerData> viewers;

        private void OnEnable()
        {
            viewers = new List<ViewerData>();
        }
    }
    
    [Serializable]
    public class ViewerData
    {
        public Transform transform;
        public float viewAngle = 40;
        public float maxDistance = 7;
    }
}

