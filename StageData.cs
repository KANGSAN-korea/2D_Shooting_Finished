using UnityEditor.U2D.Path;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField]
    Vector2 limitMin;
    [SerializeField]
    Vector2 limitMax;

    public Vector2 LimMin => limitMin;
    public Vector2 LimMax => limitMax;
}
