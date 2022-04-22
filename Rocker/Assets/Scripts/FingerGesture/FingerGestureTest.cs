using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerGestureTest : MonoBehaviour
{
    private FingerGestureSystem fingerGestureSystem;
    // Start is called before the first frame update
    void Start()
    {
        fingerGestureSystem = new FingerGestureSystem();
    }

    // Update is called once per frame
    void Update()
    {
        fingerGestureSystem.Update();
    }
}
