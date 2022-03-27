using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUpdate : MonoBehaviour
{
    private Text myText;

    private static Vector3 position;

    public static void UpdatePos(float posX, float posY, float posZ)
    {
        position = new Vector3(posX, posY, posZ);
    }

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "PositionX: " + position.x + "\n" + "PositionX:" + position.y + "\n" + "PositionZ:" + position.z;
    }
}
