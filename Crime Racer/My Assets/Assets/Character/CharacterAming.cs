using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAming : MonoBehaviour
{
    public float turnSpeed;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = camera.transform.rotation.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}
