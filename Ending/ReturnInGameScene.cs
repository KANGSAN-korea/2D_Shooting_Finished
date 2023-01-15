using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnInGameScene : MonoBehaviour
{
    public void OnClickEzzz()
    {
        SceneManager.LoadScene("Intro");
    }
}
