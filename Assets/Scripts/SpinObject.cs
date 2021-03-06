//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour {
 
    public Vector3 RotateAmount = Vector3.zero;  // degrees per second to rotate in each axis. Set in inspector.
   
    // Update is called once per frame
    void Update () {

        transform.Rotate(RotateAmount * Time.deltaTime);
    }

}
