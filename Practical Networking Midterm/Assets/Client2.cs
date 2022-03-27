using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lecture 4
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


public class Client2 : MonoBehaviour
{
    public static byte[] buffer = new byte[512];
    public static byte[] buffer2 = new byte[512];

    public GameObject myCube;
    public GameObject myCube2;

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static EndPoint remoteClient;

    private static IPEndPoint remoteEP2;
    private static EndPoint remoteClient2;

    private static Socket client_socket;


    //Lecture 5
    private float[] pos;
    private byte[] bpos;

    private float[] pos2;
    private byte[] bpos2;

    float isInput = 0.0f;
    bool isOpen = false;
    bool hasSetClient = false;

    float timer = 0.0f;


    public void SetUpClient()
    {
        if (JoinServer.GetIP() != null)
        {
            IPAddress ip = IPAddress.Parse(JoinServer.GetIP());
            remoteEP = new IPEndPoint(ip, 11112);
            remoteClient = (EndPoint)remoteEP;

            remoteEP2 = new IPEndPoint(ip, 11113);
            remoteClient2 = (EndPoint)remoteEP;

            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client_socket.Blocking = false;
        }
    }

    public void ReceiveData(byte[] buffer, GameObject _cube)
    {
        //remoteClient ///
        int rec = client_socket.ReceiveFrom(buffer, ref remoteClient);///
        Debug.Log("The message recieved was X: " + BitConverter.ToSingle(buffer, 0 * 4));///
        Debug.Log("The message recieved was Y: " + BitConverter.ToSingle(buffer, 1 * 4));///
        Debug.Log("The message recieved was Z: " + BitConverter.ToSingle(buffer, 2 * 4));///

        _cube.transform.position = new Vector3(BitConverter.ToSingle(buffer, 0 * 4), BitConverter.ToSingle(buffer, 1 * 4), BitConverter.ToSingle(buffer, 2 * 4));
    }




    public void SendData(GameObject cube)
    {
        pos = new float[] { cube.transform.position.x, cube.transform.position.y, cube.transform.position.z, isInput };



        Debug.Log(pos[0].ToString());
        Debug.Log(pos[1].ToString());
        Debug.Log(pos[2].ToString());
        //Debug.Log("isInput = " + pos[3].ToString());

        Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);


        client_socket.SendTo(bpos, remoteEP);
    }


    public void SendData2(GameObject cube)
    {
        pos2 = new float[] { cube.transform.position.x, cube.transform.position.y, cube.transform.position.z, isInput };



        Debug.Log(pos2[0].ToString());
        Debug.Log(pos2[1].ToString());
        Debug.Log(pos2[2].ToString());
        //Debug.Log("isInput = " + pos[3].ToString());

        Buffer.BlockCopy(pos2, 0, bpos2, 0, bpos2.Length);


        client_socket.SendTo(bpos2, remoteEP2);
    }



    // Start is called before the first frame update
    void Start()
    {


        myCube = GameObject.Find("Cube");
        myCube2 = GameObject.Find("Cube2");
        SetUpClient();

        //RunClient();

        //Lecture 5
        pos = new float[] { myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z, isInput };
        bpos = new byte[pos.Length * 4];

        //pos2 = new float[] { myCube2.transform.position.x, myCube2.transform.position.y, myCube2.transform.position.z, isInput };
        // bpos2 = new byte[pos2.Length * 4];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());

        //Debug.Log("Sent X:" + myCube.transform.position.x);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (isOpen == false && hasSetClient == false)
            {
                SetUpClient();

                hasSetClient = true;
            }
            isOpen = true;
            isInput = 1.0f;

            timer += Time.deltaTime;
        }
        else
        {
            isInput = 0.0f;
            isOpen = false;
            //if (client_socket != null)
            //    client_socket.Close();
            timer = 0.0f;
        }

        if (timer < 5.0f)
        {
            if (JoinServer.GetIP() != null)
            {
                SendData(myCube);
                //SendData(myCube2);

                ReceiveData(buffer, myCube);
                //ReceiveData(buffer2, myCube2);


            }


        }


        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isOpen == false && hasSetClient == false)
            {
                SetUpClient();

                hasSetClient = true;
            }
            isOpen = true;
            isInput = 1.0f;

            timer += Time.deltaTime;
        }
        else
        {
            isInput = 0.0f;
            isOpen = false;
            //if (client_socket != null)
            //    client_socket.Close();
            timer = 0.0f;
        }

        if (timer < 5.0f)
        {
            if (JoinServer.GetIP() != null)
            {
                SendData2(myCube2);
                //SendData(myCube2);

                ReceiveData(buffer, myCube2);
                //ReceiveData(buffer2, myCube2);


            }


        }




    }



}
