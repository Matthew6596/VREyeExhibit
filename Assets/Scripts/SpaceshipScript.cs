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

    public void PlayerEnterShip()
    {
        inShip = true;

        //Movement
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Disable Player Move
        characterControllerDriver.locomotionProvider = shipMove.GetComponent<DynamicMoveProvider>();

        //Enable Interior
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void PlayerExitShip()
    {
        inShip = false;

        //Movement
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        player.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Renable Player Move
        characterControllerDriver.locomotionProvider = playerMove.GetComponent<DynamicMoveProvider>();

        //Disable Interior
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

}
