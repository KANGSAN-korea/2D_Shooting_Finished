using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    StageData stageData;
    Movement movement;
    [SerializeField]
    KeyCode keyCodeAttack = KeyCode.Space;
    KeyCode keyCodeAttackSmash = KeyCode.LeftControl;
    private bool isDie = false;
    private Animator animator;

    PlayerFire playerFire;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    void Start()
    {
        movement= GetComponent<Movement>();
        playerFire = GetComponent<PlayerFire>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDie == true) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        movement.MoveTo(new Vector3(x, y, 0));

        if (Input.GetKeyDown(keyCodeAttack))
        {
            playerFire.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack))
        {
            playerFire.StopFiring();
        }

        if (Input.GetKeyDown(keyCodeAttackSmash))
        {
            playerFire.StartBoom();
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimMin.x, stageData.LimMax.x),
        Mathf.Clamp(transform.position.y, stageData.LimMin.y, stageData.LimMax.y),transform.position.z);

    }

    public void OnDie()
    {
        animator.SetTrigger("onDie");
        Destroy(GetComponent<CircleCollider2D>());
        isDie = true;
    }
    
    public void OnDieEvent()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene("GameOver");
    }
}
