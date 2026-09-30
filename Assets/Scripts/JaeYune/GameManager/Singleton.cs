using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    
    /// <summary>
    /// GameManager.Instance 형식으로 출발할 것.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            
            return _instance;
        }
    }
    
    protected void SetSingleton(T value)
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
