
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ReloadInteract : UdonSharpBehaviour
{
    [SerializeField] GunScript attachedGun;
    void Start()
    {
        
    }
    public override void Interact() {
        if (attachedGun != null ) {
            attachedGun.reloadWeapon();
        }
    }
    
}
