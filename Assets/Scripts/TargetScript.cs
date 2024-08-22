using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetScript : MonoBehaviour
{
    public static int score=0;
    public TMP_Text[] canvasText;
    [Space(10)]
    public string targetPosition;
    public int targetValue;
    public bool isBullsye = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            collision.rigidbody.freezeRotation = true;
            collision.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            collision.rigidbody.velocity = Vector3.zero;
            collision.collider.enabled = false;

            OutputToScreen();

            /*string color = this.gameObject.name;
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
            }*/

        }
    }
    public void ResetScore() { score = 0; canvasText[1].text = "Score: " + score; }
    private void OutputToScreen()
    {
        score += targetValue;
        canvasText[0].text = targetPosition+" "+gameObject.name.ToLower() + " target hit!"+(isBullsye?" Bullseye!":"");
        canvasText[1].text = "Score: "+score;
    }
}
