using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    StageData stageData;
    [SerializeField]
    GameObject enemyPrefabs;
    [SerializeField]
    GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private BGMController bgmController;
    [SerializeField]
    private GameObject textBossWarning;
    [SerializeField]
    private GameObject panelBossHP;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    float spawnerTime;
    [SerializeField]
    private int maxEnemyCount = 15;
    [SerializeField]
    GameObject enemyPrefabs2;

    [SerializeField]
    private GameObject panelBoss2HP;
    [SerializeField]
    private GameObject boss2;

    private void Awake()
    {
        boss.SetActive(false);
        boss2.SetActive(false);

        textBossWarning.SetActive(false);
        panelBossHP.SetActive(false);
        panelBoss2HP.SetActive(false);
        StartCoroutine(WaitFor1sec());
        
    }

    IEnumerator WaitFor1sec()
    {
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine("SpawnEnemy");

    }

    IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0;

        while (true)
        {
            float positionX = Random.Range(stageData.LimMin.x, stageData.LimMax.x);
            GameObject enemyClone = Instantiate(enemyPrefabs, new Vector3(positionX, stageData.LimMax.y - 3.0f, 0.0f), Quaternion.identity);
            SpawnEnemyHPSlider(enemyClone);

            currentEnemyCount++;

            if (currentEnemyCount > (maxEnemyCount / 2) && currentEnemyCount < maxEnemyCount)
            {
                StartCoroutine("SpawnEnemy2");
            }

            if (currentEnemyCount == maxEnemyCount)
            {
                StopCoroutine("SpawnEnemy2");
                StartCoroutine("SpawnBoss");
                break;
            }
            yield return new WaitForSeconds(spawnerTime);

        }
    }
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    IEnumerator SpawnEnemy2()
    {
        while (true)
        {

            float position = (Random.Range(stageData.LimMin.x, stageData.LimMax.x));
            GameObject enemyClone2 = Instantiate(enemyPrefabs2, new Vector3(position, stageData.LimMax.y - 3.0f, 0.0f), Quaternion.identity);
            SpawnEnemyHPSlider(enemyClone2);

            yield return new WaitForSeconds(4f); // ±¸Çö ¾ÈµÊ

        }
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMtype.Boss);
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        textBossWarning.SetActive(false);
        panelBossHP.SetActive(true);
        boss.SetActive(true);
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }

}