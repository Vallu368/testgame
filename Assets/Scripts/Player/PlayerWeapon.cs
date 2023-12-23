using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject weaponHolderR;
    public GameObject placeHolder;
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R))
        {
            GetWeapon(placeHolder);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            RemoveWeapon();
        }
    }

    public void GetWeapon(GameObject weapon)
    {
        if (weapon != null && weaponHolderR.transform.childCount == 0) {
            anim.SetBool("Weapon", true);
            GameObject heldWeapon = Instantiate(weapon, weaponHolderR.transform.position, weaponHolderR.transform.rotation);
            heldWeapon.transform.parent = weaponHolderR.transform;
            Debug.Log("Spawned weapon " +  weapon.name);
            
        }
        if (weaponHolderR.transform.childCount > 0)
        {
            Debug.Log("Already holding weapon, removing it and taking new one");
            RemoveWeapon();
            anim.SetBool("Weapon", true);
            GameObject heldWeapon = Instantiate(weapon, weaponHolderR.transform.position, weaponHolderR.transform.rotation);
            heldWeapon.transform.parent = weaponHolderR.transform;
            Debug.Log("Spawned weapon " + weapon.name);
        }
    }


    public void RemoveWeapon()
    {
        if (weaponHolderR.transform.childCount > 0)
        {
            anim.SetBool("Weapon", false);
            foreach (Transform child in weaponHolderR.transform)
            {
                Destroy(child.gameObject);
                Debug.Log("Removed weapon");
            }
        }
    }
}
