
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DestroyOverTime : UdonSharpBehaviour
{
    public float DeathDelay = 3;
    void Start()
    {
        
    }
    private void FixedUpdate() {
        if (DeathDelay > 0) {
            DeathDelay -= Time.deltaTime;
        } else {
            Destroy(this.gameObject);
        }
    }
}
