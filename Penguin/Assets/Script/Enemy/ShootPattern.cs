using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ShootPattern", fileName = "ShootPattern")]
public class ShootPattern : SerializedScriptableObject
{
    // 발사 딜레이
    public float shootDelay;
    
    // 한번 발사시 반복 횟수
    public int iterTime;
    
    // 발사 각도
    public List<Quaternion> quaternions;
    
    // 트리거 이름
    public string triggerName;
    
    // AudioCLip
    public AudioClip _attackSFX;
}
