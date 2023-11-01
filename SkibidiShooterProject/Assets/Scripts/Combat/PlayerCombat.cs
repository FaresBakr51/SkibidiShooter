using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
namespace Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Weapon currentWeapon;
        [SerializeField] private Transform weaponParent;
        public Weapon CurrentWeapon { get { return currentWeapon; } }

        private FPSController currentController;
        private void Start()
        {
            currentController = GetComponent<FPSController>();
        }
        public void SetWeapon(Weapon weapon)
        {
            if (currentWeapon)
            {
                Destroy(currentWeapon.gameObject);
            }
            currentController.UpdateSwitchAnimations(currentWeapon, weapon);
            currentWeapon = weapon;
            Debug.Log("Change weapon");
            currentWeapon.UpdateBullets();
          
            AttachWeapon();
        }

        public void AttachWeapon()
        {
            if (weaponParent == null) return;
            print("Attach");
         //   Vector3 localscale = currentWeapon.transform.localScale;
            currentWeapon.transform.parent = weaponParent;
            currentWeapon.transform.localPosition = currentWeapon.HandPos;
            currentWeapon.transform.localScale = currentWeapon.HandScale;
            currentWeapon.transform.localRotation = Quaternion.Euler(currentWeapon.HandRot);
            currentWeapon.enabled = true;
        }


       
    }
}
