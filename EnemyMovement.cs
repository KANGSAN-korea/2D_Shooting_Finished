using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int scorePoint = 100;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject[] itemPrefabs;

    private PlayerController playerController;
    Rigidbody2D rbEnemy;
    [SerializeField]
    float moveSpeed = 1f;


    Vector3 randomDirection;
    private void Awake()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    void Start()
    {
        StartCoroutine("ChangeDirection");
    }

    void Update()
    {
        transform.position += randomDirection * moveSpeed * Time.deltaTime;
        if(transform.position.x > 2.4 || transform.position.x < -2.4)
        {
            randomDirection = - randomDirection;
            rbEnemy.velocity = new Vector2(randomDirection.x * moveSpeed, randomDirection.y);
            StartCoroutine("ChangeDirection");
            
        }
        if (transform.position.y > 4.4 || transform.position.y < 0)
        {
            randomDirection = -randomDirection;
            rbEnemy.velocity = new Vector2(randomDirection.x * moveSpeed, randomDirection.y);
            StartCoroutine("ChangeDirection");
        }
    }

    IEnumerator ChangeDirection()
    {

            float xx = Random.Range(-1.0f, 1.0f);
            float yy = Random.Range(-1.0f, 1.0f);
            randomDirection = new Vector3(xx, yy).normalized;
            yield return randomDirection;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            Destroy(gameObject);
            OnDie();
        }
    }

    public void OnDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        playerController.Score += scorePoint;
        SpawnItem();
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        int spawnItem = Random.Range(0, 100);
        if(spawnItem < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 15)
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 30)
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }
    }
}
