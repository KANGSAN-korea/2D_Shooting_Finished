using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum AttackType { CircleFire = 0, SingleFireToCenterPosition, SpiralFire, holeCircleShot, RandomSprial }
public class BossFire : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;



    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }
    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 1f;
        int count = 15;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                clone.GetComponent<Movement>().MoveTo(new Vector2(x, y));
            }
            weightAngle += 1;
            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.1f;

        while (true)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 direction = (targetPosition - clone.transform.position).normalized;
            clone.GetComponent<Movement>().MoveTo(direction);
            yield return new WaitForSeconds(attackRate);
        }
    }
    private IEnumerator SpiralFire()
    {
        float angle = 0f;
        float attackRate = 0.1f;

        while (true)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f); // 불릿의 X 방향
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f); // 불릿의 Y 방향

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);                   // 설정한 x, y 방향을 벡터로 전환
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;            // 방향과 보스의 방향을 이어 노멀라이즈
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 탄환 생성
            clone.GetComponent<Movement>().MoveTo(bulDir); //!!!!!
            angle += 10f;
            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator holeCircleShot()
    {
        float attackRate = 0.1f;
        float angle = 0;
 
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {


                float bulDirX = transform.position.x + Mathf.Sin(((angle + 90f * i) * Mathf.PI) / 180f); // 불릿의 X 방향
                float bulDirY = transform.position.y + Mathf.Cos(((angle + 90f * i) * Mathf.PI) / 180f); // 불릿의 Y 방향

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector3 bulDir = (bulMoveVector - transform.position).normalized;            // 방향과 보스의 방향을 이어 노멀라이즈

                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 탄환 생성
                clone.GetComponent<Movement>().MoveTo(bulDir);
            }
                angle += 10f;
                if (angle >= 360f)
                {
                    angle = 0f; 
                }
            yield return new WaitForSeconds(attackRate);

            //ShotBullet(bullet, m_bulletSpeed, angle);

        }

    }
    private IEnumerator RandomSprial()
    {
        float attackRate = 0.1f;
        float angle = 0;

        while (true)
        {
            for (int i = 0; i <= 5; i++)
            {


                float bulDirX = transform.position.x + Mathf.Sin(((angle + 60f * i) * Mathf.PI) / 180f); // 불릿의 X 방향
                float bulDirY = transform.position.y + Mathf.Cos(((angle + 60f * i) * Mathf.PI) / 180f); // 불릿의 Y 방향
                bulDirX = Random.Range(bulDirX - 1, bulDirX + 1);
                bulDirY = Random.Range(bulDirY - 1, bulDirY + 1);
                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector3 bulDir = (bulMoveVector - transform.position).normalized;            // 방향과 보스의 방향을 이어 노멀라이즈

                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 탄환 생성
                clone.GetComponent<Movement>().MoveTo(bulDir);
            }
            angle += 10f;
            if (angle >= 360f)
            {
                angle = 0f;
            }
            yield return new WaitForSeconds(attackRate);


        }

    }

}
