using UnityEngine;

public class GenericMonoSingelton<T> : MonoBehaviour where T : GenericMonoSingelton<T>
{
    public static T Instance { get { return instance; } }
    private static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
        else
            Destroy(gameObject);
    }
}
