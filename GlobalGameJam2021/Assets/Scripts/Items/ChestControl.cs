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

        private ItemControl item;

        [HideInInspector] public UnityEvent OnSoulAnimEnded = new UnityEvent();
        [HideInInspector] public UnityEvent OnOniAnimEnded = new UnityEvent();
        
        private void Awake()
        {
            renderer.enabled = false;
            item.enabled = false;
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

            playerRenderer.enabled = true;
            playerControl.FreezeMoving = false;
            playerControl.IntoSoul();

            item.enabled = true;
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

            playerRenderer.enabled = true;
            playerControl.FreezeMoving = false;
            playerControl.IntoSoul();
            
            Destroy(gameObject);
        }
    }
}