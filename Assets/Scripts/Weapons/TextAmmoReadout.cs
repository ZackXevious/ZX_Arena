
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TextAmmoReadout : UdonSharpBehaviour
{
    [SerializeField] TextMeshProUGUI AmmoReadout;
    [SerializeField] GunScript attachedGun;
    private void Start() {
        if (attachedGun == null || AmmoReadout == null) {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate() {
        this.AmmoReadout.text = ""+attachedGun.getCurrMag();
    }
}
