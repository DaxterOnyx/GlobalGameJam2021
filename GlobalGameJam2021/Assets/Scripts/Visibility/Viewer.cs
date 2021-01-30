using UnityEngine;

namespace Visibility
{
    public class Viewer : MonoBehaviour
    {
        [SerializeField] private ViewerList list;

        [Header("Data")]
        [SerializeField] private float viewAngle = 40;
        [SerializeField] private float maxDistance = 7;

        private void Start()
        {
            var data = new ViewerData()
            {
                transform = this.transform,
                maxDistance = this.maxDistance,
                viewAngle = this.viewAngle,
            };
            
            list.viewers.Add(data);
        }
    }
}