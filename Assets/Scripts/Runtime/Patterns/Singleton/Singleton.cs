using UnityEngine;

namespace Nara.Patterns
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {   
        protected static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + "_Auto-Generate");
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            InitializeSingleton();
        }
        private void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

