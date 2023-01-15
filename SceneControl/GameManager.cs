using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioSource audios;

    public void OnClickonRed()
    {
        SceneManager.LoadScene("inGameStage");
        audios.Stop();

    }

    public void OnClickonBlue()
    {
        SceneManager.LoadScene("inGameStage");
        audios.Stop();

    }

    void Awake()
    {
        audios = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
}
