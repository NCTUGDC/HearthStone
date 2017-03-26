using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HearthStone.Library;

public class SystemInitializer : MonoBehaviour
{
    private void Awake()
    {
        LogService.InitialService(
            infoMethod: Debug.Log,
            infoFormatMethod: Debug.LogFormat,
            errorMethod: Debug.LogWarning,
            errorFormatMethod: Debug.LogWarningFormat,
            fatalMethod: Debug.LogError,
            fatalFormatMethod: Debug.LogErrorFormat);
    }
}
