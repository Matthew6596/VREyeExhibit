using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpaceshipScript : MonoBehaviour
{
    GameObject player;
    bool inShip = false;
    public Transform cockPit;
    public int moveSpeed = 1;
    public int turnSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if(inShip)
        {
            GetComponent<TeleportPlayer>().Teleport(); //CHANGE PARENT OF PLAYER INSTEAD
        }
    }

    public void PlayerEnterShip()
    {
        inShip = true;

        //Player Movement
        player.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Disable Player Move

        //Enable Interior
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void PlayerExitShip()
    {
        inShip = false;

        //PlayerMovement
        player.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Renable Player Move

        //Disable Interior
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void MoveShipForward()
    {
        if(inShip)
        {
            Debug.Log("Moving Forwards");
            gameObject.transform.position += new Vector3(0, 0, moveSpeed);
        }
    }
    public void MoveShipBackward()
    {
        if (inShip)
        {
            Debug.Log("Moving Back");
            gameObject.transform.position -= new Vector3(0, 0, moveSpeed);
        }
    }
    public void TurnShipLeft()
    {
        if (inShip)
        { //Rotate on Y
            gameObject.transform.Rotate(new Vector3(0, turnSpeed, 0));
        }
    }
    public void TurnShipRight()
    {
        if (inShip)
        { //Rotate on Y
            gameObject.transform.Rotate(new Vector3(0, -turnSpeed, 0));
        }
    }
}
