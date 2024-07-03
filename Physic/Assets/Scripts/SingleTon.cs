using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DefauleNameSpace
{
    //XONG
    public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
    {
        private static T instance;
        public static T Instance => instance;
        [SerializeField]
        private bool isDontDestroyOnload;

        
        private void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
            if (isDontDestroyOnload)
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}
