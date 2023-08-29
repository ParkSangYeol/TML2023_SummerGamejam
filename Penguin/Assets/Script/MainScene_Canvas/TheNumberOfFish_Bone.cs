using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheNumberOfFish_Bone : MonoBehaviour
{
    public Text FishBoneText;

    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager._onBulletsChangeEvent.AddListener(FishBonePrint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FishBonePrint(int FishNum)
    {
        FishBoneText.text = " X " + FishNum;
    }
}
