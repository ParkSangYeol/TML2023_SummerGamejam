using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player State", menuName = "ScriptableObject/PlayerStates")]
public class PlayerState : ScriptableObject
{
    public Texture2D _playerImage;
    public string _stateName;
    public bool _isGetDamage;
}
