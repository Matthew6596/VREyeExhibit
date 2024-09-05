using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SpaceshipScript : MonoBehaviour
{
    GameObject player;
    bool inShip = false;
    bool selectExited = false;
    public Transform cockPit;
    public float moveSpeed = 0.05f;
    public float turnSpeed = 0.05f;

    float velocity=0;
    float rotation=0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if (!inShip) return;

        gameObject.transform.Rotate(new Vector3(0, rotation*Time.deltaTime, 0));
        gameObject.transform.position += transform.forward * velocity * Time.deltaTime;
    }

    public void PlayerEnterShip()
    {
        inShip = true;

        //Player Movement
        player.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Disable Player Move
        player.transform.parent = gameObject.transform;

        //Enable Interior
        StartCoroutine(SetInteriorActive(true));
    }

    public void PlayerExitShip()
    {
        inShip = false;

        //PlayerMovement
        player.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Renable Player Move
        player.transform.parent = GameObject.Find("===XR Stuff===").transform;

        //Disable Interior
        StartCoroutine(SetInteriorActive(false));
    }

    IEnumerator SetInteriorActive(bool state)
    {
        yield return new WaitForSeconds(1.5f); //wait for teleport effect
        gameObject.transform.GetChild(0).gameObject.SetActive(state);
    }

    public void SetMoveVal(bool negative)
    {
        if(inShip) velocity += negative ? -moveSpeed : moveSpeed;
    }
    public void SetRotateVal(bool negative)
    {
        if (inShip) rotation += negative ? -turnSpeed : turnSpeed;
    }

    public void MoveShipForward()
    {
        if(inShip)
        {
            Debug.Log("Moving Forwards");
            gameObject.transform.position += transform.forward * moveSpeed;
        }
    }
    public void MoveShipBackward()
    {
        if (inShip)
        {
            Debug.Log("Moving Back");
            gameObject.transform.position += transform.forward * -moveSpeed;
        }
    }
    public void TurnShipLeft()
    {
        if (inShip)
        { //Rotate on Y
            gameObject.transform.Rotate(new Vector3(0, -turnSpeed, 0));
        }
    }
    public void TurnShipRight()
    {
        if (inShip)
        { //Rotate on Y
            gameObject.transform.Rotate(new Vector3(0, turnSpeed, 0));
        }
    }

    public void SetSelectExited(bool state)
    {
        selectExited = state;
    }
}
