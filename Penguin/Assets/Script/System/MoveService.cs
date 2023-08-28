using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveService : Singleton<MoveService>
{
    public static float minHor, maxHor;
    public static float minVer, maxVer;
    public static float minDuration, maxDuration;

    private void Start()
    {
        // 수평 이동 범위 지정
        minHor = -8;
        maxHor = 8;
        
        // 수직 이동 범위 지정
        minVer = 3.5f;
        maxVer = 5.5f;
        
        // 시간 범위 지정
        minDuration = 1f;
        maxDuration = 3f;
    }

    private static Vector3 GetMoveHorizontal(Vector3 position)
    {
        return new Vector3(UnityEngine.Random.Range(minHor, maxHor),position.y,position.z);;
    }

    private static Vector3 GetMove()
    {
        return new Vector3(UnityEngine.Random.Range(minHor, maxHor), UnityEngine.Random.Range(minVer, maxVer), 0);
    }

    private static Vector3 GetStop(Vector3 pos)
    {
        return pos;
    }

    public static PatternCommand GetMoveHorizontalPattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetMoveHorizontal(position);
        command.duration = UnityEngine.Random.Range(minDuration, maxDuration);

        return command;
    }
    
    public static PatternCommand GetMovePattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetMove();
        command.duration = UnityEngine.Random.Range(minDuration, maxDuration);

        return command;
    }
    
    public static PatternCommand GetStopPattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetStop(position);
        command.duration = UnityEngine.Random.Range(minDuration, maxDuration);

        return command;
    }
}
