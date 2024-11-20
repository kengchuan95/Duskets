using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour
{
    public float spreadAmount = 0.05f;
    private int Damage;
    public AudioClip shotSound;

    // Start is called before the first frame update
    void Start()
    {
        Damage = new System.Random().Next(1, 5);
    }

    public void ShootAtPlayer(float attackRange, LayerMask isPlayer)
    {
        PlayShotAudio();
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, getGunSpread(), out Hit, attackRange, isPlayer))
        {
            PlayerValues target = Hit.transform.GetComponent<PlayerValues>();
            if (target != null)
            {
                target.PlayerDamaged(Damage);
            }
        }
    }
    private Vector3 getGunSpread()
    {
        var forward = transform.forward;
        Vector3 fwd = forward + transform.TransformDirection(new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount)));
        return fwd;
    }
    private void PlayShotAudio()
    {
        AudioSource.PlayClipAtPoint(shotSound, transform.position, 0.5f);
    }
}
