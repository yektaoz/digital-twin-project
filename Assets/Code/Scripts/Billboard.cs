using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform _camera;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>().gameObject.transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position +  _camera.transform.forward);
    }

}
