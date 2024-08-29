using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpaceshipScript : MonoBehaviour
{
    GameObject player;
    bool inShip = false;
    GameObject playerMove;
    GameObject shipMove;
    public Transform cockPit;
    CharacterControllerDriver characterControllerDriver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterControllerDriver = player.GetComponent<CharacterControllerDriver>();
        playerMove = characterControllerDriver.locomotionProvider.GameObject();
        shipMove = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(inShip)
        {
            if (player.transform.position == cockPit.position)
            {
                player.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void PlayerEnterShip()
    {
        Debug.Log("Selected");
        inShip = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        characterControllerDriver.locomotionProvider = shipMove.GetComponent<DynamicMoveProvider>();
    }

    public void PlayerExitShip()
    {
        inShip = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        characterControllerDriver.locomotionProvider = playerMove.GetComponent<DynamicMoveProvider>();
    }

}
