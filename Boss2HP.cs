using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2HP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 300;
    [SerializeField]
    GameObject boss2_Prefab;
    [SerializeField]
    GameObject boss2HPsliderbar;
    private bool boss1isDead;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    private Boss2 boss;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss2>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if( currentHP <= 0 && !boss1isDead)
        {
            boss1isDead = true;
            StartCoroutine(SpawnBoss2());
            boss.OnDie();
        }
    }

    IEnumerator SpawnBoss2()
    {
        yield return new WaitForSeconds(3f);
        boss2HPsliderbar.SetActive(true);
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
