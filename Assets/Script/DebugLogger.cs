using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogger : MonoBehaviour {

    void Update() {
        print(transform.position +"/"+transform.localPosition);
        print(transform.parent);
    }


}
