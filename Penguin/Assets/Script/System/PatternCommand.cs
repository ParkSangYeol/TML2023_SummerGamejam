using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternCommand
{
    public Vector3 pos;
    public float duration;
}

public enum MovePattern
{
    MoveHorizontal,
    MovePosition,
    Stop,
    MovePoint,
    StopExactly
}
