using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerIP : MonoBehaviour
{
    public string serverIP;
    public GameObject inputText;
    public GameObject displayMessage;

    public void SetServerIP()
    {
        serverIP = inputText.GetComponent<Text>().text;
        displayMessage.GetComponent<Text>().text = "Server IP: " + serverIP;
    }

    
}
