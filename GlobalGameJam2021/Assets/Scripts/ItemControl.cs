using UnityEngine;

public class ItemControl : MonoBehaviour
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
            if (gameObject.transform.name.Contains("Mantra")) {
                GameManager.Instance.FoundMantra(gameObject);
            } else if (gameObject.transform.name.Contains("Chest")) {
                GameManager.Instance.FoundChest(gameObject);
            }
        }
    }

}
