using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip boomAudio;
    [SerializeField]
    private int damage = 100;
    private float boomDelay = 0.5f;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;
        float current = 0;
        float percent = 0;

        while( percent < 1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;

            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));
            yield return StartCoroutine("OnBoom");
        }

/*        animator.SetTrigger("onBoom");
        audioSource.clip = boomAudio;
        audioSource.Play();*/
    }


    IEnumerator OnBoom()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");

        for(int i = 0; i < enemys.Length; ++i)
        {
            enemys[i].GetComponent<EnemyMovement>().OnDie();
        }
        for(int i = 0; i < meteorites.Length; ++i)
        {
            meteorites[i].GetComponent<Meteorite>().OnDie();
        }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        for( int i = 0; i < projectiles.Length; ++i)
        {
            projectiles[i].GetComponent<EnemyProjectile>().OnDie();
        }
        GameObject[] projectiless = GameObject.FindGameObjectsWithTag("EnemyProjectiles");
        for (int i = 0; i < projectiless.Length; ++i)
        {
            projectiless[i].GetComponent<EnemyProjectiles>().OnDie();
        }
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            boss.GetComponent<BossHP>().TakeDamage(damage);
        }
        GameObject boss2 = GameObject.FindGameObjectWithTag("Boss2");
        if (boss2 != null)
        {
            boss2.GetComponent<Boss2HP>().TakeDamage(damage);
        }

        Destroy(gameObject);
        yield return null;
    }

}
