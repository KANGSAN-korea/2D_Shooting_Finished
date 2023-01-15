using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    GameObject projectileSmashPrefab;
    [SerializeField]
    float attackRate = 0.1f;
    private int maxAttackLevel = 3;
    private int attackLevel = 1;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject boomPrefab;
    private int boomCount = 3;

    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }

    public int BoomCount
    {
        set => boomCount = Mathf.Max(0, value);
        get => boomCount;
    }

    private void Awake()
    {

    }

    public void StartBoom()
    {
        if(boomCount > 0)
        {
            boomCount--;
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
        }
    }

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }
    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }
    IEnumerator TryAttack()
    {
        while (true)
        {
            /*            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            */
            AttackByLevel();  
            yield return new WaitForSeconds(attackRate);
        }
    }

    public void StartSmashing()
    {
        StartCoroutine("TrySmashing");
    }

    IEnumerator TrySmashing()
    {

            Instantiate(projectileSmashPrefab, transform.position, Quaternion.identity);
            yield return null;
        
    }
    private void AttackByLevel()
    {
        GameObject cloneProjectile = null;
        switch (attackLevel)
        {
            case 1:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile.GetComponent<Movement>().MoveTo(new Vector3(-0.2f, 1, 0));
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile.GetComponent<Movement>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }

}
