using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;
    private TextMeshProUGUI textBossWarining;

    private void Awake()
    {
        textBossWarining = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }
    private IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            textBossWarining.color = Color.Lerp(startColor, endColor, percent);

            yield return null;
        }
    }
}
