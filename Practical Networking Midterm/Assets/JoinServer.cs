using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinServer : MonoBehaviour
{
    public static string IPinputfield;
    
    public void JoinTheServer()
    {
        IPinputfield = GameObject.Find("IPText").GetComponent<Text>().text;
        
    }

   public static string GetIP()
    {
        return IPinputfield;
    }
}
