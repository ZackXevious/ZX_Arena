
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class GunScript : UdonSharpBehaviour
{
    [Header("Bullet spawn")]
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 3;

    [Header("FireRate")]
    [SerializeField] bool singleShot = true;
    bool wantsToFire;
    float currTimer;
    [SerializeField] float timeBetweenBullets = .2f;
    [SerializeField] int magSize = 12;
    [SerializeField] int totalAllowedAmmo= 72;
    int currMag;
    int currAmmo;

    [Header("Sound Effects")]
    [SerializeField] AudioSource SoundSource;
    [SerializeField] AudioClip GunFireSound;
    [SerializeField] AudioClip GunEmptySound;
    void Start()
    {
        this.currAmmo = this.magSize * 3;
        this.reloadWeapon();
        
    }
    public override void OnPickup() {
        base.OnPickup();
        //Get the player who picked this up
        //Enable grip and reload interacts
        //adjust location on screen for if player is on PC or in VR.
        //Apply GUI stuff for player
    }

    public override void OnPickupUseDown() {
        if (currMag>0) {
            if (singleShot) {
                SendCustomNetworkEvent(NetworkEventTarget.All, nameof(fireWeapon));
            } else {
                wantsToFire = true;
            }
        } else {
            //Play gunshot sound
            if (this.SoundSource && this.GunEmptySound) {
                //Change pitch
                SoundSource.pitch = 1;
                // Play Sound
                SoundSource.PlayOneShot(this.GunEmptySound);
            }
        }
        
        
    }
    public override void OnPickupUseUp() {
        wantsToFire = false;
    }

    private void FixedUpdate() {
        if (currTimer>0) {
            currTimer = Mathf.MoveTowards(currTimer, 0, Time.deltaTime);
        }
        if (wantsToFire && !singleShot) {
            SendCustomNetworkEvent(NetworkEventTarget.All,nameof(fireWeapon));
        }
        if (Input.GetKey(KeyCode.R)) {
            this.reloadWeapon();
        }
    }

    public void fireWeapon() {
        if (currTimer != 0) { // Can't fire gun due to timer delay
            return;
        } else {
            if (bulletSpawn && bulletPrefab && this.currMag>0) {
                GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb) {
                    rb.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                }
                currTimer = timeBetweenBullets;

                //Play gunshot sound
                if (this.SoundSource && this.GunFireSound) {
                    //Change pitch
                    SoundSource.pitch = 1 + Random.Range(-.05f, .05f);
                    // Play Sound
                    SoundSource.PlayOneShot(this.GunFireSound);
                }
                this.currMag -= 1;
            }
            
        }
    }

    public void reloadWeapon() {
        if (this.currAmmo == 0 || this.currMag == this.magSize) {
            return ;
        }
        int ammoDifference = this.magSize - this.currMag;
        this.currMag = this.magSize;

    }
    
}
