using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if(instance == null)
                {
                    instance = new GameObject(name: "Instance of" + typeof(T)).AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("You should not have 2 of " + typeof(T) + "in the scene.");
            Destroy(gameObject);
        }
        else
        {
            instance = GameObject.FindObjectOfType<T>();
        }
    }
}
