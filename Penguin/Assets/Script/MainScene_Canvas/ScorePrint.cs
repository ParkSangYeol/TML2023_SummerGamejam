using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrint : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance._onChangePoint.AddListener(PointPrint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PointPrint(int score)
    {
        scoreText.text = "Score : " + score;
    }
}
