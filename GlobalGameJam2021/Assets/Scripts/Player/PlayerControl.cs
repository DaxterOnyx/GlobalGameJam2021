using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool constantMove = true;
        [SerializeField] private float threshold = 0.1f;
        
        [SerializeField] private Animator animator;
        [SerializeField] private Transform viewDirection; 
        
        private Rigidbody2D rigid;
        private bool moving = false;
        private Camera _cam;
        public Vector2 Direction { get; private set; } = Vector2.down;

        public bool FreezeMoving { get; set; } = false;
        
        public enum Type {
            Chest,
            Soul
        }
        public Type PlayerType { get; private set; } = Type.Chest;
        
        
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            _cam = Camera.main;
        }

        void Update()
        {
            if (FreezeMoving)
            {
                rigid.velocity = Vector2.zero;
                return;
            }
            
            moving = Input.GetMouseButton(0);

            Vector2 dir = (Vector2) _cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
            Vector2 normDir = dir.normalized;

            viewDirection.up = normDir;
            
            animator.SetBool("Walk", moving);
            animator.SetFloat("DirX", normDir.x);
            animator.SetFloat("DirY", normDir.y);

            Direction = constantMove ? normDir : dir;
            
            if (moving && dir.magnitude > threshold) 
                rigid.velocity = Direction * speed; 
            else 
                rigid.velocity = Vector2.zero;
        }

        public void IntoSoul()
        {
            animator.SetBool("Soul", true);
            PlayerType = Type.Soul;
        }

        public void IntoChest()
        {
            animator.SetBool("Soul", false);
            PlayerType = Type.Chest;
        }

    }
}