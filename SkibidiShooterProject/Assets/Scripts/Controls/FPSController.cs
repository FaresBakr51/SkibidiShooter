using Combat;
using Manager;
using SoundManage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using Weapons;

namespace Controller
{
    public class FPSController : MonoBehaviour,IHitable
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float speed;
        [SerializeField] private float mouseSens;
        private float currentMouseSens;
        [SerializeField] private float downSpeed;
        [SerializeField] private FixedJoystick joystick;
        [SerializeField] private float playerHeight;
        [SerializeField] private bool isGrounded;
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask groundMaskRaycast;
        [Header("Combat")]
        private PlayerCombat playerCombat;
        [SerializeField] private float health;

        [Header("Animation")]
        [SerializeField] private Animator animator;

        [Header("UI")]
        [SerializeField] private Text healthTxt;
        [SerializeField] private Text bulletTxt;


        [Header("Sounds")]
        [SerializeField] private AudioSource soundsource;
        private SoundManagment soundManage;
        private bool canPlay;
        /**
         * movment floats
         /**/
        private bool isMoving => joystick.Vertical !=0 || joystick.Horizontal != 0;
        private Vector2 camRot;
        [Header("Inputs")]
        [SerializeField] int touchCounts = 0;
        [SerializeField] bool onDrag = false;
         public int TouchCounts { get { return touchCounts; } set { touchCounts = value; } }
        public bool OnDrag { get { return onDrag; } set { onDrag = value; } }



        private Vector3 velocity;
        private float gravity = -9.8f;
        public float Health => health;

        private void Start()
        {

            currentMouseSens = mouseSens;
             characterController = GetComponent<CharacterController>();
            playerCombat = GetComponent<PlayerCombat>();
            soundManage = GetComponent<SoundManagment>();
        }
        private void Update()
        {
            if (!joystick) return;
            if (GameManager.Instance && GameManager.Instance.CurrentGameState == GameState.GS_Finished) return;
            if (Input.touchCount >0)
            {
                if (Input.touchCount == 2 && touchCounts == 1)//first is joystick and the second on the screen
                {
                    //2 fingers
                    touchCounts = 2;
                   
                }
            }
           // touchestxt.text = touchCounts.ToString();
            camRot.y = Mathf.Clamp(camRot.y, -20, 20);
            isGrounded = Physics.Raycast(transform.position, Vector3.down,playerHeight * 0.5f+0.2f, groundMaskRaycast);

            Move(joystick.Horizontal, joystick.Vertical, speed);
            ControlRotation(camRot);
            HandleGravity(isGrounded);
            if (isMoving)
            {
                HandleRunSounds();
            }
            else
            {
                canPlay = true;
                soundsource.Stop();
            }
        }
        public int GetTouchCounts()
        {
            return Input.touchCount;
        }

        #region Controls
        private void Move(float horizontalAx,float verticalAxis,float speed)
        {
            Vector3 direction = transform.forward * verticalAxis+ transform.right * horizontalAx;
            characterController.Move(direction * Time.deltaTime * speed);
           
        }

        private void ControlRotation(Vector2 rotvector)
        {
          //  if (!canRotate) return;
          //  Debug.Log("Rotate");
           transform.rotation = Quaternion.Euler(-rotvector.y, rotvector.x, 0);
   
        }
        public void Jump()
        {
            if (isGrounded)
            {
                velocity.y = jumpForce;
            }
        }
        #endregion
        private void HandleGravity(bool grounded)
        {
            if (!grounded)
            {
                velocity.y += gravity * Time.deltaTime * downSpeed;
            }
            else if (velocity.y <= 0)
            {
                velocity.y = -3;
            }
            characterController.Move(velocity * Time.deltaTime * downSpeed);
        }

        #region Collides&Events
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.CompareTag("Gun"))
            {
                Weapon weapon = other.gameObject.GetComponentInChildren<Weapon>(); //.TryGetComponent(out Weapon weapon);
                if (weapon != null)
                {
                    Destroy(other.gameObject);
                    print("Collided weapon");
                    weapon.SetController(this);
                    playerCombat.SetWeapon(weapon);
                   
                }
            }
        }
        #endregion
        #region Animations
        public void UpdateSwitchAnimations(Weapon oldWeapon,Weapon currentWeapon)
        {
           if(oldWeapon.WeaponType == EWeaponType.EWT_Pistol && currentWeapon.WeaponType == EWeaponType.EWT_Rifle)
            {
                //swith pistol to rifle
                UpdateAnimation( 2);
            }else if(oldWeapon.WeaponType == EWeaponType.EWT_Pistol && currentWeapon.WeaponType == EWeaponType.EWT_Heavy)
            {
                //switch pistol to heavy
                UpdateAnimation( 4);
            }
        }
        public void UpdateAnimationDependOnWeapontype(EWeaponType type)
        {
            switch (type)
            {
                case EWeaponType.EWT_Pistol:
                    UpdateAnimation(0);
                    break;
                case EWeaponType .EWT_Rifle:
                    UpdateAnimation(2);
                    break;
                case EWeaponType.EWT_Heavy:
                    UpdateAnimation(4);
                    break;
            }
        }
        public void UpdateAnimation(int val)
        {
            animator.SetInteger("State", val);
        }
        public void ReloadWeaponEvent()
        {
          
            playerCombat.CurrentWeapon.UpdateBullets();
        }
        #endregion
        #region Healh
        public void TakeHit(float damage)
        {
            if (health <= 0) return;
            UpdateHealth(health -= damage);
        }
        private void UpdateHealth(float val)
        {
            health = val;
            if (health > 0)
            {
                healthTxt.text = health.ToString();
            }
            else
            {
                healthTxt.text = 0.ToString();
            }
        }
        public void UpdateBulletTxt(int current,int max)
        {
            bulletTxt.text = current +" / " + max.ToString();
        }
        #endregion

        #region Inputs
        //private bool IsPointerOverUIObject()
        //{
        //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //    return results.Count > 0;
        //}
        public void OnTouchInput1(InputAction.CallbackContext context)
        {

          //  if (IsPointerOverUIObject()) return;
            if (touchCounts == 2 && !OnDrag)
            {
                camRot += context.ReadValue<Vector2>() * currentMouseSens;
               

            }
        }
        public void OnTouchInput2(InputAction.CallbackContext context)
        {

           // if (IsPointerOverUIObject()) return;
            if (!isMoving) return;
            if (touchCounts < 2) return;
            if (!OnDrag) return;
            camRot += context.ReadValue<Vector2>() * currentMouseSens;
        }
        public void OnTouchInputNoMovment(InputAction.CallbackContext context)
        {
           // if (IsPointerOverUIObject()) return;
            if (touchCounts > 0) return; // 2 fingers   
            if (context.started && !isMoving)
            {
                camRot += context.ReadValue<Vector2>() * currentMouseSens;
            }


        }
        #endregion
        #region Sounds
        private void HandleRunSounds()
        {
            if (canPlay)
            {
                AudioClip run = soundManage.RequestSound("run");
                soundManage.PlayOnShot(soundsource,run);
                StartCoroutine(CanRunSound(run.length));
                canPlay = false;
            }
        }
        IEnumerator CanRunSound(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canPlay = true;
        }
        #endregion
    }
}