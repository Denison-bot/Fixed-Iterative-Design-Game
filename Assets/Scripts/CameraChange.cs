using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject defaultCam;
        void OnTriggerEnter(Collider other)
        
        {
        if (other.tag == ("Player"))
        {
            defaultCam.SetActive(false);
            targetObject.SetActive(true);
        }
        }

        void OnTriggerExit(Collider other)
        {
        if (other.tag == ("Player"))
        {
            defaultCam.SetActive(true);
            targetObject.SetActive(false);
        }
        }
    
}
