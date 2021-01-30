using System;
using UnityEngine;
using UnityEngine.Events;

namespace Visibility
{
    [RequireComponent(typeof(VisibilityTest))]
    public class VisibilityEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnBecomeVisible;
        [SerializeField] private UnityEvent OnBecomeUnvisible;

        private VisibilityTest _tester;
        private bool _previous;
        
        private void Start()
        {
            if(_tester.IsVisible && !_previous) OnBecomeVisible.Invoke();
            else if (!_tester.IsVisible && _previous) OnBecomeUnvisible.Invoke();

            _previous = _tester.IsVisible;
        }
    }
}