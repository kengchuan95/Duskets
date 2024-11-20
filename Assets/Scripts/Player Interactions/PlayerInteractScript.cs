using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractScript : MonoBehaviour
{
    public GameObject Dusket;
    public GameObject Distol;
    public DusketBehaviour dusketScript;
    public DistolBehaviour distolScript;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void Shoot() 
    {
        if (Dusket.activeSelf)
        {
            dusketScript.Shoot();
        }
        else if (Distol.activeSelf)
        {
            distolScript.Shoot();
        }
    }

    public void Reload()
    {
        if (Dusket.activeSelf)
        {
            dusketScript.Reload();
        }
        else if (Distol.activeSelf)
        {
            distolScript.Reload();
        }
    }
    public void ToggleWeapon()
    {
        if (Dusket.activeSelf)
        {
            Dusket.SetActive(false);
            Distol.SetActive(true);
        }
        else if (Distol.activeSelf)
        {
            Distol.SetActive(false);
            Dusket.SetActive(true);
        }
    }
}
