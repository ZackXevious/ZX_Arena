﻿
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
    bool wantsToFire;
    float currTimer;
    [SerializeField] float timeBetweenBullets = .2f;

    [Header("Sound Effects")]
    [SerializeField] AudioSource SoundSource;
    [SerializeField] AudioClip GunFireSound;
    void Start()
    {
        
    }

    public override void OnPickupUseDown() {
        wantsToFire = true;
    }
    public override void OnPickupUseUp() {
        wantsToFire = false;
    }

    private void FixedUpdate() {
        if (currTimer>0) {
            currTimer = Mathf.MoveTowards(currTimer, 0, Time.deltaTime);
        }
        if (wantsToFire) {
            SendCustomNetworkEvent(NetworkEventTarget.All,nameof(fireWeapon));
        }
    }

    public void fireWeapon() {
        if (currTimer != 0) { // Can't fire gun due to timer delay
            return;
        } else {
            if (bulletSpawn && bulletPrefab) {
                GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb) {
                    rb.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                }
                currTimer = timeBetweenBullets;
            }
            if (this.SoundSource && this.GunFireSound) {
                SoundSource.PlayOneShot(this.GunFireSound);
            }
        }
    }
    
}
