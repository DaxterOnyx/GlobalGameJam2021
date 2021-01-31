using System;
using UnityEngine;

namespace Visibility
{
    public class VisibilityLoseGame : MonoBehaviour
    {
        private VisibilityTest _tester;
        
        private void Start()
        {
            _tester = GetComponent<VisibilityTest>();
        }

        private void Update()
        {
            if (_tester.IsVisible)
            {
                SoundManager.Instance.PlayEffect(SoundManager.Instance.flip);
                GameManager.Instance.EndGameLose();
            }
        }
    }
}