using Controller;
using Manager;
using SoundManage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Weapons
{
    public enum EWeaponType
    {
        EWT_Pistol, EWT_Rifle, EWT_Heavy, EWT_MAX
    }

    public class Weapon : MonoBehaviour
    {
      
        [Header("Weapon Properties")]
        
        [SerializeField] private ParticleSystem fireVfx;
        [SerializeField] private Transform shotPoint;
        [SerializeField] private float damage;
        [SerializeField] private float fireRate;
        [SerializeField] private LayerMask hitableLayer;
        [SerializeField] private EWeaponType weaponType;
        private float currentRate;
        [Header("Bullets")]
        [SerializeField] private int currentBullets;
        [SerializeField] private int maxbulletsCapacity;
        [SerializeField] private int maxweaponCapacity;
       
        public EWeaponType WeaponType { get { return weaponType; } }

        [Header("Hands&&Player")]
        [SerializeField] private FPSController myController;
        [SerializeField] private Vector3 inhand_rot;
        [SerializeField] private Vector3 inhand_pos;
        [SerializeField] private Vector3 inhand_scale;
        public Vector3 HandScale { get { return inhand_scale; } }
        public Vector3 HandRot { get { return inhand_rot; } }
        public Vector3 HandPos { get { return inhand_pos; } }


        [Header("Sounds")]
        private SoundManagment soundManage;
        [SerializeField] private AudioSource weaponsoundSource;



        private void Start()
        {
            soundManage = GetComponent<SoundManagment>();
           
           
        }
        private void Update()
        {
            if (myController == null) return;
            if (GameManager.Instance && GameManager.Instance.CurrentGameState == GameState.GS_Finished) return;
            if(Time.time > currentRate && currentBullets > 0)
            {
                currentRate = Time.time + fireRate;
                RayCast();
            }else if(currentBullets <= 0)
            {
                Reload();
            }
            Debug.DrawRay(shotPoint.position, myController.transform.forward, Color.red);


        }

        private void RayCast()
        {
         
            RaycastHit hit;
            if (Physics.Raycast(shotPoint.position, myController.transform.forward, out hit, 1000))
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out IHitable damagableobj))
                {
                   
                    if (damagableobj != null && damagableobj.Health >0)
                    {
                       if(soundManage)  soundManage.PlayOnShot(weaponsoundSource,"shot");
                        fireVfx.Play();

                        currentBullets--; //dont waste bullets
                        myController.UpdateBulletTxt(currentBullets, maxweaponCapacity);
                        damagableobj.TakeHit(damage);
                    }
                 
                }
             
            }
        }

        private void Reload()
        {
            myController.UpdateAnimation(1);
        }
        public void UpdateBullets()
        {
            if (myController && maxweaponCapacity - maxbulletsCapacity >= 0)//ex max cap = 100 // bulletscapacity = 20 // 100-20 >0 ? true reload ? /// 20 // max 20 ? 20
            {
                if (currentBullets > 0)// left ammo = 5 ?
                {
                   var increasevalu = maxbulletsCapacity - currentBullets;// 20 -5 ? 15
                    currentBullets += increasevalu;
                    maxweaponCapacity -= increasevalu;
                }
                else
                {
                    currentBullets += maxbulletsCapacity;
                    maxweaponCapacity -= maxbulletsCapacity;
                }
                myController.UpdateBulletTxt(currentBullets,maxweaponCapacity);
                myController.UpdateAnimationDependOnWeapontype(weaponType);

                Debug.Log("Update bullets");
            }
        }
      public void SetController(FPSController controller)
        {
            myController = controller;
        }
    }
}