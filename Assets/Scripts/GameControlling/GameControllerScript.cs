
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GameControllerScript : UdonSharpBehaviour
{
    [SerializeField] GameObject test;
    GameObject localHitbox;
    private VRCPlayerApi localPlayer;
    [SerializeField] GameObject playerHud; //This item contains the player's hud, if at all possible. Maybe also an extra hitbox for headshots?
    void Start()
    {
        localPlayer = Networking.LocalPlayer;
        localHitbox = Instantiate(test,localPlayer.GetPosition(), localPlayer.GetRotation());
    }
    public override void OnPlayerJoined(VRCPlayerApi player) {
        if (player.IsUserInVR()) {
            Debug.Log("Joined player is in VRC");
        } else {
            Debug.Log("Joined player is on PC");
        }
        
    }

    public override void PostLateUpdate() {
        base.PostLateUpdate();
        localHitbox.transform.position = localPlayer.GetPosition();
        localHitbox.transform.rotation = localPlayer.GetRotation();
        VRCPlayerApi.TrackingData headData = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
        this.playerHud.transform.position = headData.position;
        this.playerHud.transform.rotation = headData.rotation;
    }
}
