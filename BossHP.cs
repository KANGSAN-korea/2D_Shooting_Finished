using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 300;
    [SerializeField]
    GameObject boss2_Prefab;
    [SerializeField]
    GameObject boss2HPsliderbar;
    [SerializeField]
    GameObject bossscript;

    private bool boss1isDead;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    private Boss boss;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if( currentHP <= 0 && !boss1isDead)
        {
            boss1isDead = true;
            
            boss.OnDie();
            StartCoroutine(SpawnBoss2());
        }
    }

    IEnumerator SpawnBoss2()
    {
        yield return new WaitForSeconds(1f);
        bossscript.SetActive(true);
        yield return new WaitForSeconds(3f);
        boss2HPsliderbar.SetActive(true);
        bossscript.SetActive(false);
        yield return null;
        if(boss2_Prefab != null)
        {
            boss2_Prefab.SetActive(true);
        }
    }


    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
