using System;
using UnityEngine;

namespace Visibility
{
    public class Viewer : MonoBehaviour
    {
        [SerializeField] private ViewerList list;

        [Header("Data")] 
        [SerializeField] private Collider2D checkCollider;
        [SerializeField] private float viewAngle = 40;
        [SerializeField] private float maxDistance = 7;

        private ViewerData _data;

        private void Awake()
        {
            _data = new ViewerData()
            {
                collider = this.checkCollider,
                transform = this.transform,
                maxDistance = this.maxDistance,
                viewAngle = this.viewAngle,
            };
        }

        private void OnEnable()
        {
            list.viewers.Add(_data);
        }

        private void OnDisable()
        {
            list.viewers.Remove(_data);
        }
    }
}