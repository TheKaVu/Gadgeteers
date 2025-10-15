using UnityEngine;

namespace Source.Gadgeteers
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        private static T _instance;
        private static readonly object _instanceLock = new();
        private static bool _quitting = false;


        public static T Singleton
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null && !_quitting)
                    {
                        _instance = FindAnyObjectByType<T>();
                        if (_instance == null)
                        {
                            GameObject go = new(typeof(T).ToString());
                            _instance = go.AddComponent<T>();

                            DontDestroyOnLoad(_instance.gameObject);
                        }
                    }

                    return _instance;
                }
            }
        }

        protected void Awake()
        {
            if (_instance == null) _instance = gameObject.GetComponent<T>();
            else if (_instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
                throw new System.Exception(string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString()));
            }
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _quitting = true;
        }

    }
}
