using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalBillboard : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target, Vector3.up);
    }
}
