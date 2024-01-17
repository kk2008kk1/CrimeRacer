using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeChanger : MonoBehaviour
{
    //publics
    public Transform StartTransform;
    public Transform OfficeTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StartTransform.position = OfficeTransform.position;
        StartTransform.rotation = OfficeTransform.rotation;


    }
}
