using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
