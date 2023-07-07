using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void OnEnable()
    {
        camera = Camera.main;
    }

    private void OnDisable()
    {
        camera = null; 
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(camera.transform.forward); 
    }

    //Calculate distance between player. and its size should be adjusted depending on the distance. 


}
