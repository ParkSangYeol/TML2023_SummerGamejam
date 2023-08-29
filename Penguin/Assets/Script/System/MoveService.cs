using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveService : Singleton<MoveService>
{
    public float minHor, maxHor;
    public float minVer, maxVer;
    public float minDuration, maxDuration;
    public List<Vector3> movePoint;

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

    private Vector3 GetMoveHorizontal(Vector3 position)
    {
        return new Vector3(UnityEngine.Random.Range(minHor, maxHor),position.y,position.z);;
    }

    private Vector3 GetMove()
    {
        return new Vector3(UnityEngine.Random.Range(minHor, maxHor), UnityEngine.Random.Range(minVer, maxVer), 0);
    }

    private Vector3 GetStop(Vector3 pos)
    {
        return pos;
    }

    private Vector3 GetMovePoint(Vector3 pos)
    {
        return movePoint[UnityEngine.Random.Range(0, movePoint.Count)];
    }
    
    public PatternCommand GetMoveHorizontalPattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetMoveHorizontal(position);
        command.duration = UnityEngine.Random.Range(minDuration, maxDuration);

        return command;
    }
    
    public PatternCommand GetMovePattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetMove();
        command.duration = UnityEngine.Random.Range(minDuration, maxDuration);

        return command;
    }
    
    public PatternCommand GetStopPattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetStop(position);
        command.duration = UnityEngine.Random.Range(minDuration, maxDuration);

        return command;
    }

    public PatternCommand GetMovePointPattern(Vector3 position)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetMovePoint(position);
        command.duration = 0.5f;

        return command;
    }

    public PatternCommand GetStopExactlyPattern(Vector3 position, float time)
    {
        PatternCommand command = new PatternCommand();
        command.pos = GetStop(position);
        command.duration = time;

        return command;
    }
}
