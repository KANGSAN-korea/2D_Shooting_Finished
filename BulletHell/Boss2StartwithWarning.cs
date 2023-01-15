using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2StartwithWarning : MonoBehaviour
{
    [SerializeField]
    GameObject warings;
    [SerializeField]
    GameObject boss2;
    void Awake()
    {
        StartCoroutine("popupWarning");
        StartCoroutine("SpawnBoss");

    }

    IEnumerator popupWarning()
    {
        warings.SetActive(true);
        yield return new WaitForSeconds(2f);
        warings.SetActive(false);
    }
/*    IEnumerator SpawnBoss()
    {
        warings.SetActive(true);
    }*/

    void Update()
    {
        
    }
}
