using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public static UICamera Instance;
    public Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Camera = transform.Find("Camera").GetComponent<Camera>();
    }


}
