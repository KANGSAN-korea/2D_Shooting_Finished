using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    private void Start()
    {
        moveSpeed = 10f;
    }
    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }
    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {

        // CancleInvoke();
    }
}
