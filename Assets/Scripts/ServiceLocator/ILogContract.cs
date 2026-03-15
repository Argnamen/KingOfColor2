using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogContract
{
    void Log(string message);
    void Throw(string message);
}
