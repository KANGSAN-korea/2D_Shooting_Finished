using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;


    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Score") == 1)
        {
            Player1.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Score") == 2)
        {
            Player2.SetActive(true);

        }
    }
}
