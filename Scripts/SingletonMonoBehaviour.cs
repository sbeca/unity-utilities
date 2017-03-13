using UnityEngine;

interface ISingletonMonoBehaviour
{
    void InitSingleton();
}

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingletonMonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool initialised = false;

    public static T Instance
    {
        get
        {
            if (!initialised && instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (instance != null)
                {
                    ((ISingletonMonoBehaviour)instance).InitSingleton();
                    initialised = true;
                }
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Multiple instances of SingletonMonoBehaviour type [" + typeof(T) + "]", this.gameObject);
#endif
            DestroyImmediate(this.gameObject);

            return;
        }

        instance = this as T;

        if (!initialised)
        {
            ((ISingletonMonoBehaviour)instance).InitSingleton();
            initialised = true;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            initialised = false;
        }
    }

    public virtual void InitSingleton()
    {
    }
}
