using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrint : MonoBehaviour
{
    public int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PointIncreased()
    {
        //오르는 조건 작성

        scoreText.text = "Score : " + score;
    }

    public void PointPrint()
    {
        score = 0;
        scoreText.text = "Score : " + score;
    }
}
