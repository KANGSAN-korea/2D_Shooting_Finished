using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2HPViewer : MonoBehaviour
{
    [SerializeField]
    private Boss2HP bossHP;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;
    }
}
