using UnityEngine;

public class ItemControl : MonoBehaviour
{
    enum ItemType
    {
        Chest,
        Mantra,
    }
    
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private ItemType type = ItemType.Chest;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag(playerTag))
        {
            switch (type)
            {
                case ItemType.Chest: 
                    GameManager.Instance.FoundChest(gameObject);
                    break;
                case ItemType.Mantra:
                    GameManager.Instance.FoundMantra(gameObject);
                    break;
            }
        }
    }

}
