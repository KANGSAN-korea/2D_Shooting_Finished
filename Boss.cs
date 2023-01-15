using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03 }

public class Boss : MonoBehaviour
{
    [SerializeField]
    StageData stageData;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement movement2D;
    private BossFire bossFire;
    private BossHP bossHP;
    private SpriteRenderer spriteRenderer;
    private Rigidbody rb;
    private Collider2D colliders;

    void Awake()
    {
        movement2D = GetComponent<Movement>();
        bossFire = GetComponent<BossFire>();
        bossHP = GetComponent<BossHP>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<Collider2D>();

    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);

        while (true)
        {
            if ( transform.position.y <= bossAppearPoint)
            {
                movement2D.MoveTo(Vector3.zero);

                ChangeState(BossState.Phase01);
            }
            yield return null;
        }
    }
    private IEnumerator Phase01()
    {

            bossFire.StartFiring(AttackType.CircleFire);

            while (true)
            {
                if(bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
                {
                    bossFire.StopFiring(AttackType.CircleFire);
                    ChangeState(BossState.Phase02);
                }
                yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossFire.StartFiring(AttackType.SingleFireToCenterPosition);

        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            if (transform.position.x <= stageData.LimMin.x || transform.position.x >= stageData.LimMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }
            if(bossHP.CurrentHP <= bossHP.MaxHP * 0.3f)
            {
                bossFire.StopFiring(AttackType.SingleFireToCenterPosition);
                ChangeState(BossState.Phase03);
            }
            yield return null;
        }
    }
    private IEnumerator Phase03()
    {
        bossFire.StartFiring(AttackType.CircleFire);
        bossFire.StartFiring(AttackType.SingleFireToCenterPosition);
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            if(transform.position.x <= stageData.LimMin.x || transform.position.x >= stageData.LimMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }
            yield return null;
        }
    }

    public void OnDie()
    {
        playerController.Score += 5000;
        GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        /*        clone.GetComponent<BossExplosion>().Setup(playerController, nextSceneName);
        */
        Destroy(spriteRenderer);
        Destroy(bossFire);
        Destroy(movement2D);
        Destroy(rb);
        Destroy(colliders);

    }
}

