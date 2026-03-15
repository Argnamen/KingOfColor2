using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLocator
{
    private static ILogContract _logging;
    private static ISaveContract _saving;

    

    public static void RegisterLogger(ILogContract service)
    {
        _logging = service;
        Debug.Log("Logging service registered...");
    }

    public static void RegisterSaver(ISaveContract service)
    {
        _saving = service;
        Debug.Log("Saving service registered...");
    }

    public static ILogContract GetLogService()
    {
        return _logging;
    }

    public static ISaveContract GetSaveService()
    {
        return _saving;
    }

}
