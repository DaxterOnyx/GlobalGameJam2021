using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class ChestControl : MonoBehaviour
    {
        [SerializeField] private float animationDuration;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer renderer;

        private Collider2D col;

        [HideInInspector] public UnityEvent OnSoulAnimEnded = new UnityEvent();
        [HideInInspector] public UnityEvent OnOniAnimEnded = new UnityEvent();
        
        private void Awake()
        {
            col = GetComponent<Collider2D>();
            
            renderer.enabled = false;
            col.enabled = false;
        }

        // animate chest in place
        public void ToSoul(GameObject player)
        {
            var playerControl = player.GetComponent<PlayerControl>();
            var playerRenderer = player.GetComponentInChildren<SpriteRenderer>();

            // lock player during animation
            playerControl.FreezeMoving = true;
            
            // swap visible
            playerRenderer.enabled = false;
            renderer.enabled = true;
            
            // move chest to player pos
            transform.position = player.transform.position;

            // play animation
            animator.SetBool("IsChest", true);

            StartCoroutine(ToSoulAnimEnded(playerControl, playerRenderer));
        }

        IEnumerator ToSoulAnimEnded(PlayerControl playerControl, SpriteRenderer playerRenderer)
        {
            yield return new WaitForSeconds(animationDuration);
            
            OnSoulAnimEnded.Invoke();

            playerRenderer.enabled = true;
            playerControl.FreezeMoving = false;
            playerControl.IntoSoul();

            yield return new WaitForSeconds(2f);
            
            col.enabled = true;
        }

        public void ToOni(GameObject player)
        {
            var playerControl = player.GetComponent<PlayerControl>();
            var playerRenderer = player.GetComponentInChildren<SpriteRenderer>();

            // lock player during animation
            playerControl.FreezeMoving = true;
            
            // swap visible
            playerRenderer.enabled = false;
            renderer.enabled = true;
            
            // move chest to player pos
            player.transform.position = transform.position;

            // play animation
            animator.SetBool("IsChest", false);

            StartCoroutine(ToOniAnimEnded(playerControl, playerRenderer));
        }

        IEnumerator ToOniAnimEnded(PlayerControl playerControl, SpriteRenderer playerRenderer)
        {
            yield return new WaitForSeconds(animationDuration);

            OnOniAnimEnded.Invoke();
            
            playerRenderer.enabled = true;
            playerControl.FreezeMoving = false;
            playerControl.IntoOni();
            
            Destroy(gameObject);
        }
    }
}