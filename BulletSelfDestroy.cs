using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSelfDestroy : MonoBehaviour
{
    [SerializeField]
    StageData stagedata;
    void Update()
    {
        if(transform.position.x > stagedata.LimMax.x || transform.position.x < stagedata.LimMin.x)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > stagedata.LimMax.y || transform.position.y < stagedata.LimMin.y)
        {
            Destroy(gameObject);
        }
    }
}
