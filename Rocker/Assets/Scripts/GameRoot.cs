﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FingerGestureSystem.GetInstance().Update();
        RoleController.GetInstance().Update();
    }

    private void LateUpdate()
    {
        RoleController.GetInstance().LateUpdate();
    }
}
