using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool constantMove = true;
        [SerializeField] private float threshold = 0.1f;
        private Rigidbody2D rigid;
        private bool moving = false;
        private Vector2 direction = Vector2.down;
        private bool m_freez_moving = false;

        public enum Type {
            Chest,
            Soul
        }

        private Type m_player_type = Type.Chest;

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                moving = true;
            if (moving && Input.GetMouseButtonUp(0))
                moving = false;
            
            if (!FreezMouving) {
                direction = ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition)) -
                            ((Vector2) transform.position);
                float dist = direction.magnitude;
                if (constantMove) direction.Normalize();
                Vector2 velocity = Vector2.zero;
                if (moving && dist > threshold)
                {
                    velocity = direction * speed;
                }

                rigid.velocity = velocity;
            }
            else {
                rigid.velocity = Vector2.zero;
            }
        }
        
        public void IntoSoul() 
        {
            m_player_type = Type.Soul;
        }
        
        public void IntoChest() 
        {
            m_player_type = Type.Chest;
        }
        
        public Type PlayerType { get { return m_player_type; } }
        public bool FreezMouving { get { return m_freez_moving; } set { m_freez_moving = value; } }
        
    }
}