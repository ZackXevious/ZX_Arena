
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CanvasTest : UdonSharpBehaviour
{
    Canvas thisCanvas;
    // Start is called before the first frame update
    void Start() {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.sortingOrder = 90000;
    }
}
