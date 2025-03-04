
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerHud : UdonSharpBehaviour
{
    [Header("LeftHand")]
    [SerializeField] TextMeshProUGUI leftHandWeapon;
    [SerializeField] TextMeshProUGUI leftHandAmmo;

    [Header("RightHand")]
    [SerializeField] TextMeshProUGUI rightHandWeapon;
    [SerializeField] TextMeshProUGUI rightHandAmmo;

    VRCPlayerApi localPlayer;
    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void FixedUpdate() {
        VRC_Pickup leftHandPickup = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left);
        VRC_Pickup rightHandPickup = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right);

        this.updateWeaponHand(leftHandPickup, leftHandWeapon, leftHandAmmo);
        this.updateWeaponHand(rightHandPickup, rightHandWeapon, rightHandAmmo);
        

    }
    private void updateWeaponHand(VRC_Pickup pickup, TextMeshProUGUI weaponName, TextMeshProUGUI weaponAmmo) {
        if (pickup != null) {
            GunScript testGun = pickup.gameObject.GetComponent<GunScript>();
            if (testGun != null) {
                weaponName.text = testGun.name;
                weaponAmmo.text = "" + testGun.getCurrMag()+" || "+testGun.getCurrAmmo();
            } else {
                weaponName.text = pickup.name;
                weaponAmmo.text = "";
            }
        } else {
            //Show Grenades
            weaponName.text = "Grenades go here";
            weaponAmmo.text = "";
        }
    }
}
