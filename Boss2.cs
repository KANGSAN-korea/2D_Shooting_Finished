using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Boss2State { MoveToAppearPoint = 0, Phase01, Phase02, Phase03 }

public class Boss2 : MonoBehaviour
{
    [SerializeField]
    StageData stageData;
    [SerializeField]
    private GameObject explosionPrefab;
/*    [SerializeField]
    private PlayerController playerController;*/
    [SerializeField]
    private string nextSceneName;
/*    [SerializeField]
    private float bossAppearPoint = 2.5f;*/
    [SerializeField]
    PlayerController playerController;
    private Boss2State boss2State = Boss2State.MoveToAppearPoint;
    private Movement movement2D;
    private BossFire bossFire;
    private Boss2HP bossHP;
    private SpriteRenderer spriteRenderer;
    private bool isNotAwake;

    void Awake()
    {
        movement2D = GetComponent<Movement>();
        bossFire = GetComponent<BossFire>();
        bossHP = GetComponent<Boss2HP>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if(gameObject.activeSelf == true && !isNotAwake)
        {
            ChangeState(Boss2State.MoveToAppearPoint);
            isNotAwake = true;
        }
    }

    public void ChangeState(Boss2State newState)
    {
        StopCoroutine(boss2State.ToString());
        boss2State = newState;
        StartCoroutine(boss2State.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {

        ChangeState(Boss2State.Phase01);
        yield return null;
    }

    private IEnumerator Phase01()
    {

        bossFire.StartFiring(AttackType.SpiralFire);
        while (true)
        {
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
            {
                bossFire.StopFiring(AttackType.SpiralFire);
                ChangeState(Boss2State.Phase02);
            }
            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossFire.StartFiring(AttackType.holeCircleShot);


        /*        Vector3 direction = Vector3.right;
                movement2D.MoveTo(direction);
        */
        /*            if (transform.position.x <= stageData.LimMin.x + 0.5f || transform.position.x >= stageData.LimMax.x - 0.5f)
                    {
                        direction *= -1;
                        movement2D.MoveTo(direction);
                    }*/
        while (true)
        {
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.3f)
            {
                bossFire.StopFiring(AttackType.holeCircleShot);
                ChangeState(Boss2State.Phase03);
            }
            yield return null;
        }

        
    }
    private IEnumerator Phase03()
    {
        bossFire.StartFiring(AttackType.RandomSprial);
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
            yield return null;
        }
    }

    public void OnDie()
    {
        GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // 스코어 저장 스크립트 들어가야 함
        playerController.Score += 10000;
        PlayerPrefs.SetInt("Score", playerController.Score);
        SceneManager.LoadScene(nextSceneName);
    }
}

