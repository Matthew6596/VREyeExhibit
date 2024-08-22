using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            collision.rigidbody.freezeRotation = true;
            collision.rigidbody.velocity = Vector3.zero;
            string color = this.gameObject.name;
            switch (color)
            {
                case "White":
                    Debug.Log("White");
                    break;
                case "Black":
                    Debug.Log("Black");
                    break;
                case "Blue":
                    Debug.Log("Blue");
                    break;
                case "Red":
                    Debug.Log("Red");
                    break;
                case "Yellow":
                    Debug.Log("Yellow");
                    break;
            }

        }
    }
}
