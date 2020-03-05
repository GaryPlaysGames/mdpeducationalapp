using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public Camera camera;
    public GameObject[] objectsToToss;
    public void ResetPhase()
    {
        switch (camera.GetComponent<GrabTrash>().phase)
        {
            case 1:
            case 5:
            case 7:
                objectsToToss[0].gameObject.transform.position = new Vector3(-4f, 0, 0);
                objectsToToss[0].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                break;
            case 2:
            case 4:
                objectsToToss[0].gameObject.transform.position = new Vector3(-4f, 1.5f, 0);
                objectsToToss[1].gameObject.transform.position = new Vector3(-4f, -1.5f, 0);
                objectsToToss[0].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                objectsToToss[1].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                break;
            case 3:
            case 6:
                objectsToToss[0].gameObject.transform.position = new Vector3(-4f, 3f, 0);
                objectsToToss[1].gameObject.transform.position = new Vector3(-4f, 0f, 0);
                objectsToToss[2].gameObject.transform.position = new Vector3(-4f, -3f, 0);
                objectsToToss[0].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                objectsToToss[1].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                objectsToToss[2].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                break;
        }
    }
}
