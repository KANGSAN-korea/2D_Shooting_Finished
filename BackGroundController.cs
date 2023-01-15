using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float scrollRange = 8.0f;
    [SerializeField]
    float moveSpeed = 3.0f;
    [SerializeField]
    Vector3 moveDistance = Vector3.zero;

    void Update()
    {
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
        if (transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
