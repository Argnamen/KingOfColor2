using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GenericLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static int Services
    {
        get {return _services.Count;}
    }

    public static void Register<T>(T newService) where T : class
    {
        Type key = typeof(T);

        if(newService == null)
        {
            _services[key] = FindNullObject<T>();
            Debug.Log($"Null {key} registered...");
        }
        else if (!_services.ContainsKey(key))
        {
            _services[key] = newService;
            Debug.Log($"{key} service registered...");
        }
        else
        {
            Debug.Log($"{key} service already registered...");
        }
    }

    public static T GetService<T>() where T : class
    {
        Type key = typeof(T);

        try
        {
            return _services[key] as T;
        }
        catch 
        {
            
            return FindNullObject<T>();
        }
    }

    private static T FindNullObject<T>() where T : class
    {
        Type key = typeof(T);

        switch (key.Name)
        {
            case nameof(ILogContract):
                return new NullLogger() as T;
            case nameof(ISaveContract):
                return new NullSaver() as T;
            default:
                return new NullLogger() as T;
        }
    }
}
