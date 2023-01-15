using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoadScene : MonoBehaviour
{
    [SerializeField]
    GameObject canvas, cam, awakeSystem, characSelector;

    private void Awake()
    {
        canvas.SetActive(false);
        cam.SetActive(false);
        awakeSystem.SetActive(false);
        characSelector.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            canvas.SetActive(true);
            cam.SetActive(true);
            awakeSystem.SetActive(true);
            characSelector.SetActive(true);
        }
    }
}
