using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titledisplayManager : MonoBehaviour
{
    [SerializeField] private Canvas uicanvas;

    private void Awake()
    {
        uicanvas.worldCamera = Camera.main;;
    }
}
