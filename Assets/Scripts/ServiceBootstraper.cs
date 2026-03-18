using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceBootstraper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Init()
    {
        GenericLocator.Register<ILogContract>(new SystemDebuger());
        GenericLocator.Register<ISaveContract>(new CloudStorage());
    }
    
}
