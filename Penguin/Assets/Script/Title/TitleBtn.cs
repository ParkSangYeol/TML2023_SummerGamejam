using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtn : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuBGM;
    [SerializeField] private AudioClip _inGameBGM;

    [SerializeField] private GameObject otherPanel;
    // Start is called before the first frame update
    void Start()
    {
        SFXManager.Instance.PlayLoop(_mainMenuBGM);
        otherPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("CombatScene");
        Time.timeScale = 1f;
        SFXManager.Instance.PlayLoop(_inGameBGM);
    }

    public void OnOtherPanel()
    {
        otherPanel.SetActive(true);
    }

    public void OffOtherPanel()
    {
        otherPanel.SetActive(false);
    }
}
