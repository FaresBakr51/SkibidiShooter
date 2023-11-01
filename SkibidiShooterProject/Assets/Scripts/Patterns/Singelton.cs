using UnityEngine;
namespace Patterns {
    public class Singelton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("Instance of " + typeof(T)).AddComponent<T>();
                    }
                }
                return _instance;
            }

        }
        void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
  

}
