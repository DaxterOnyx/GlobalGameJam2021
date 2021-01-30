using UnityEngine;

public class MantraControl : MonoBehaviour
{
    private Rigidbody2D rigid;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            GameManager.Instance.FoundMantra(gameObject);
        }
    }

}
