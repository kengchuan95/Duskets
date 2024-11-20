using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage = 0;
    [SerializeField]
    private float impactMultiplier = 1000f, fireDelay = 1f, reloadDelay = 1f, spreadAmount = 0.05f;

    public int maxBullets, maxBulletDamage, currentBullets;

    public bool isLoading = false, canShoot = true;
    
    public Camera cam;
    public ParticleSystem muzzleflash;
    public ParticleSystem impactEffect;
    public PlayerValues playerValues;
    public PauseBehavior pauseBehavior;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI remainingBulletText;
    private System.Random rand = new System.Random();

    public AudioClip lowDamageClip, midDamageClip, highDamageClip, reloading;
    public Transform weaponLocation;


    #region start/update
    public void Start()
    {
        maxBullets = currentBullets;
        GetNextBulletDamage();
    }
    public void Update()
    {
        //ReloadCheck();
        UpdateGunUI();
        //IncrementShotDelay();
    }
    #endregion
    #region Shoot
    public void Shoot()
    {
        if (CanShoot())
        {
            muzzleflash.Play();
            PlayAudio(bulletDamage);
            currentBullets -= 1;
            canShoot = false;
            ShootRaycast();
            Invoke("ResetShotTimer", fireDelay);
        }
    }
    public void ShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, getGunSpread(), out hit))
        {
            if (hit.transform.CompareTag("TargetHead") || hit.transform.CompareTag("TargetBody"))
            {
                var multiplier = 1f;
                if (hit.transform.CompareTag("TargetHead"))
                {
                    Debug.Log("Head Hit");
                    multiplier = 1.5f;
                }
                else
                {
                    Debug.Log("Body Hit");
                }

                Target target = hit.transform.GetComponentInParent<Target>();
                if (target != null)
                {
                    target.Damaged(Mathf.FloorToInt(bulletDamage * multiplier));
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * bulletDamage * impactMultiplier);
                }
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        if (currentBullets > 0)
        {
            GetNextBulletDamage();
        }
    }
    #endregion
    #region reload
    public void Reload()
    {
        if (!isLoading)
        { 
            AudioSource.PlayClipAtPoint(reloading, weaponLocation.position, 1f);
            currentBullets = 0;
            bulletDamage = 0;
            canShoot = false;
            isLoading = true;
            Invoke("FinishReload", reloadDelay);
        }
    }
    public void FinishReload()
    {
        isLoading = false;
        GetNextBulletDamage();
        currentBullets = maxBullets;
        ResetShotTimer();
    }
    #endregion
    #region UI
    public void UpdateGunUI()
    {
        if (currentBullets > 0)
        {
            bulletText.text = bulletDamage.ToString();
            remainingBulletText.text = (currentBullets - 1).ToString();
        }
        else if (isLoading)
        {
            bulletText.text = "...";
            remainingBulletText.text = maxBullets.ToString();
        }
        else
        {
            bulletText.text = "-";
            remainingBulletText.text = "0";
        }
    }
    #endregion
    #region Auxiliary Shoot
    public bool CanDoActions()
    {
        var live = playerValues.isLive;
        var paused = pauseBehavior.isPaused;
        return (live && !paused);
    }
    public Vector3 getGunSpread()
    {
        var forward = cam.transform.forward;
        Vector3 fwd = forward + cam.transform.TransformDirection(new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount)));
        return fwd;
    }
    public void GetNextBulletDamage()
    {
        bulletDamage = rand.Next(1, maxBulletDamage);
    }
    public void PlayAudio(int floatDamage)
    {
        float maxDamage = maxBulletDamage;
        float lowerThird = 1f / 3f;
        float middleThird = 2f / 3f;
        float percentOfMax = floatDamage / maxDamage;
        if (percentOfMax < lowerThird)
        {
            AudioSource.PlayClipAtPoint(lowDamageClip, weaponLocation.position, 1f);
        }
        else if (percentOfMax < middleThird)
        {
            AudioSource.PlayClipAtPoint(midDamageClip, weaponLocation.position, 1f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(highDamageClip, weaponLocation.position, 1f);
        }
    }
    public bool CanShoot()
    {
        return canShoot && CanDoActions() && currentBullets > 0;
    }
    public void ResetShotTimer()
    {
        canShoot = !(isLoading);
    }
    #endregion


}
