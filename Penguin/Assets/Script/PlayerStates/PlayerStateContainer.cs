using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Player State Container", menuName = "ScriptableObject/Player State Container")]
public class PlayerStateContainer : SerializedScriptableObject
{
    public Dictionary<string, PlayerState> PlayerStates;
}
