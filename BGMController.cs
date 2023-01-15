using UnityEngine;
public enum BGMtype { Stage = 0, Boss, Boss2}
public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void ChangeBGM(BGMtype index)
    {
        audioSource.Stop();
        audioSource.clip = bgmClips[(int)index];
        audioSource.Play();
    }
}
