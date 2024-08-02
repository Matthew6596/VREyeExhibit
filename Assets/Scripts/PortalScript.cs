using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Camera cam;
    private Camera mainCam;
    public Transform otherPortal;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.projectionMatrix = mainCam.projectionMatrix;

        Vector3 relativePos = mainCam.transform.position-transform.position;
        relativePos = new Vector3(relativePos.x,0,relativePos.z);
        cam.transform.position = relativePos+otherPortal.position;

        cam.transform.forward = mainCam.transform.forward;
    }
}
