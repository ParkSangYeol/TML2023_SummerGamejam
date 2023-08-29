using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconChange : MonoBehaviour
{
    public GameObject AtkIcon;
    public GameObject DefIcon;
    public GameObject HitIcon;

    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager._stateChageEvent.AddListener(ChangingIcon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangingIcon(PlayerState state)
    {
        switch(state._stateName)
        {
            case "DefenceState":
            {
                DefIcon.SetActive(true);
                AtkIcon.SetActive(false);
                HitIcon.SetActive(false);
                break;
            }
            case "AttackState":
            {
                DefIcon.SetActive(false);
                AtkIcon.SetActive(true);
                HitIcon.SetActive(false);
                break;
            }
            case "HitState":
            {
                DefIcon.SetActive(false);
                AtkIcon.SetActive(false);
                HitIcon.SetActive(true);
                break;
            }
        }
    }

}
