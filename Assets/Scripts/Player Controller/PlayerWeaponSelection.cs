using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelection : MonoBehaviour
{
    public GameObject dusket;
    public GameObject distol;

    public void ToggleWeapon()
    {
        if (dusket.activeSelf)
        {
            dusket.SetActive(false);
            distol.SetActive(true);
        }
        else if (distol.activeSelf)
        {
            distol.SetActive(false);
            dusket.SetActive(true);
        }
    }
}
