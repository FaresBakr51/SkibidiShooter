using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class FPSAnimationControll : MonoBehaviour
    {
        [SerializeField] private FPSController controller;
        // Start is called before the first frame update
       public void ReloadFireEvent()
        {
            controller.ReloadWeaponEvent();
        }
    }
}