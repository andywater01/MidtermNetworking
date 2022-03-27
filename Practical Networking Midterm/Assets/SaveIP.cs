using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveIP : MonoBehaviour
{

    public static GameObject IPinputField;
    public static string IP;

    public static void EnterServerIP()
    {

        IP = IPinputField.GetComponent<Text>().text;
        SetIP();
        
    }

    public static string SetIP()
    {
        return IP;
    }

}
