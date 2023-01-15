using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseEnemyFire : MonoBehaviour
{
    [SerializeField]
    GameObject enemyProject;
    [SerializeField]
    float radius = 0f;
    [SerializeField]
    float projectileSpeed = 0.1f;
    [SerializeField]
/*    int _numberOfProjectiless = 20;
*/    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AutoFire");
    }

    // Update is called once per frame
    void Update()
    {
/*        if (transform.position.x > ) ;
*/    }

    IEnumerator AutoFire()
    {
        while (true)
        {
           Instantiate(enemyProject, transform.position, Quaternion.Euler(0, 180, 0));
            yield return new WaitForSeconds(1f);
        }

    }

    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360 / _numberOfProjectiles;
        float angle = 0f;
        for (int i = 0; i <= _numberOfProjectiles - 1; i++)
        {
            float projectileDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - transform.position).normalized * projectileSpeed;

            GameObject tmp0bj = Instantiate(enemyProject, transform.position, Quaternion.identity);
            tmp0bj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

            angle += angleStep;

        }
    }
}
