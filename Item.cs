using Unity.VisualScripting;
using UnityEngine;

public enum ItemType { PowerUp = 0, Boom, HP}

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    private Movement movement2D;

    private void Awake()
    {
        movement2D = GetComponent<Movement>();
        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);
        movement2D.MoveTo(new Vector3(x, y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Use(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Use(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.PowerUp:
            player.GetComponent<PlayerFire>().AttackLevel++;
                break;
            case ItemType.Boom:
                player.GetComponent<PlayerFire>().BoomCount++;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerHP>().CurrentHP += 2;
                break;
        } 

        
    }
}
