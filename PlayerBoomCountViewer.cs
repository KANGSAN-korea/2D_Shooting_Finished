using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBoomCountViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerFire playerFire;
    private TextMeshProUGUI textBoomCount;

    private void Awake()
    {
        textBoomCount = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        textBoomCount.text = "x " + playerFire.BoomCount;
    }
}
