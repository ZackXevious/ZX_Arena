using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHudTest : MonoBehaviour
{
    Canvas thisCanvas;
    // Start is called before the first frame update
    void Start()
    {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.worldCamera = Camera.current;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
