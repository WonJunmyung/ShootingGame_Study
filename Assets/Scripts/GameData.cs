using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName ="ScriptableObject Asset/GameData")]
public class GameData : ScriptableObject
{
    public int No;
    public string Name;
    public int Score;
}
