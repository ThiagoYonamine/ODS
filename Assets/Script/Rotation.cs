using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour {
    float tiltAngle = 0.0f;
    void Update() {
		tiltAngle -= 0.1f;
        Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime);
    }
}
