using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public GameObject GMPanel;
    public Text GameText;


    public PlayerManager playerManager;
    public EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        playerManager._onDeadEvent.AddListener(GameOverPanelOn);
        playerManager._onDeadEvent.AddListener(GameOverPrint);
        enemySpawner._onGameClear.AddListener(GameCleatPrint);
        enemySpawner._onGameClear.AddListener(GameOverPanelOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverPrint()
    {
        GameText.text = " Game " + " Over ";
    }
    public void GameCleatPrint()
    {
        GameText.text = " Game " + " Clear ";
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
