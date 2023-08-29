using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public GameObject GMPanel;

    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager._onDeadEvent.AddListener(GameOverPanelOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverPanelOn()
    {
        GMPanel.SetActive(true);
    }
    public void MenuButtonOn()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
