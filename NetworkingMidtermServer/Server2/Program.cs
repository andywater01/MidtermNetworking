using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server2
{
    class Program
    {

        private static byte[] bpos;
        private static byte[] bpos2;

        private static byte[] buffer = new byte[512];
        private static byte[] buffer2 = new byte[512];
        private static byte[] buffer3 = new byte[512];
        private static byte[] buffer4 = new byte[512];


        private static IPHostEntry hostInfo;
        private static IPEndPoint localEP;
        private static Socket server;
        private static EndPoint remoteClient;

        private static IPEndPoint localEP2;
        private static EndPoint remoteClient2;

        public static bool hasStartedServer = false;


        public static void SetUpServer()
        {
            hostInfo = Dns.GetHostEntry(Dns.GetHostName());

            //IPAddress ip = hostInfo.AddressList[4]; //IPv4
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            Console.WriteLine($"Server Name: { hostInfo.HostName} | IP: {ip}");
            Console.WriteLine("Waiting for connection...");

            localEP = new IPEndPoint(ip, 11112);
            remoteClient = (EndPoint)localEP;

            localEP2 = new IPEndPoint(ip, 11113);
            remoteClient2 = (EndPoint)localEP;


            server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            

            server.Bind(localEP);

            server.Blocking = false;
        }

        public static void ReceiveData(byte[] buffer1, int clientnum, byte[] buffer2)
        {
            if (server.Available > 0)
            {
                int rec = server.ReceiveFrom(buffer1, ref remoteClient);

                Console.WriteLine("Recieved from: {0}", remoteClient.ToString());
                //Console.WriteLine("Data: {0}", Encoding.ASCII.GetString(buffer, 0, rec));

                // If client sends floats
                Console.WriteLine("Client {0} X Data {1}", clientnum, BitConverter.ToSingle(buffer1, 0 * 4));
                Console.WriteLine("Client {0} Y Data {1}", clientnum, BitConverter.ToSingle(buffer1, 1 * 4));
                Console.WriteLine("Client {0} Z Data {1}", clientnum, BitConverter.ToSingle(buffer1, 2 * 4));

                if (BitConverter.ToSingle(buffer1, 3 * 4) == 1)
                {
                    Console.WriteLine("Connected to Client");
                }


                try
                {


                    float[] pos = new float[] { BitConverter.ToSingle(buffer1, 0 * 4), BitConverter.ToSingle(buffer1, 1 * 4), BitConverter.ToSingle(buffer1, 2 * 4) };
                    bpos = new byte[pos.Length * 4];

                    Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);

                    server.SendTo(bpos, remoteClient2);


                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer1, 0 * 4));
                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer1, 1 * 4));
                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer1, 2 * 4));


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString() + " not sending");
                }










                int rec2 = server.ReceiveFrom(buffer2, ref remoteClient2);

                Console.WriteLine("Recieved from: {0}", remoteClient2.ToString());
                //Console.WriteLine("Data: {0}", Encoding.ASCII.GetString(buffer, 0, rec));

                // If client sends floats
                Console.WriteLine("Client {0} X Data {1}", clientnum, BitConverter.ToSingle(buffer2, 0 * 4));
                Console.WriteLine("Client {0} Y Data {1}", clientnum, BitConverter.ToSingle(buffer2, 1 * 4));
                Console.WriteLine("Client {0} Z Data {1}", clientnum, BitConverter.ToSingle(buffer2, 2 * 4));

                if (BitConverter.ToSingle(buffer2, 3 * 4) == 1)
                {
                    Console.WriteLine("Connected to Client");
                }


                try
                {


                    float[] pos = new float[] { BitConverter.ToSingle(buffer2, 0 * 4), BitConverter.ToSingle(buffer2, 1 * 4), BitConverter.ToSingle(buffer2, 2 * 4) };
                    bpos = new byte[pos.Length * 4];

                    Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);

                    server.SendTo(bpos, remoteClient);


                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer1, 0 * 4));
                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer1, 1 * 4));
                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer1, 2 * 4));


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString() + " not sending");
                }
            }
                




        }


        public static void ReceiveData2(byte[] thebuffer, int clientnum)
        {

            int rec2 = server.ReceiveFrom(thebuffer, ref remoteClient2);

            Console.WriteLine("Recieved from: {0}", remoteClient2.ToString());
            //Console.WriteLine("Data: {0}", Encoding.ASCII.GetString(buffer, 0, rec));

            // If client sends floats
            Console.WriteLine("Client {0} X Data {1}", clientnum, BitConverter.ToSingle(thebuffer, 0 * 4));
            Console.WriteLine("Client {0} Y Data {1}", clientnum, BitConverter.ToSingle(thebuffer, 1 * 4));
            Console.WriteLine("Client {0} Z Data {1}", clientnum, BitConverter.ToSingle(thebuffer, 2 * 4));

            if (BitConverter.ToSingle(thebuffer, 3 * 4) == 1)
            {
                Console.WriteLine("Connected to Client");
            }



        }

        public static void SendData()
        {
            try
            {
                
                
                    float[] pos = new float[] { BitConverter.ToSingle(buffer, 0 * 4), BitConverter.ToSingle(buffer, 1 * 4), BitConverter.ToSingle(buffer, 2 * 4) };
                    bpos = new byte[pos.Length * 4];

                    Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);

                    server.SendTo(bpos, remoteClient);


                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer, 0 * 4));
                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer, 1 * 4));
                    Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer, 2 * 4));


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + " not sending");
            }



        }


        public static void SendData2()
        {
            try
            {


                float[] pos2 = new float[] { BitConverter.ToSingle(buffer3, 0 * 4), BitConverter.ToSingle(buffer3, 1 * 4), BitConverter.ToSingle(buffer3, 2 * 4) };
                bpos2 = new byte[pos2.Length * 4];

                Buffer.BlockCopy(pos2, 0, bpos2, 0, bpos2.Length);

                server.SendTo(bpos2, remoteClient);


                Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer3, 0 * 4));
                Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer3, 1 * 4));
                Console.WriteLine("Message Sending is: " + BitConverter.ToSingle(buffer3, 2 * 4));


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + " not sending");
            }



        }




        public static void StartServer()
        {
            byte[] buffer = new byte[512];
            byte[] buffer2 = new byte[512];///



            IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());

            //IPAddress ip = hostInfo.AddressList[4]; //IPv4
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            Console.WriteLine($"Server Name: { hostInfo.HostName} | IP: {ip}");

            IPEndPoint localEP = new IPEndPoint(ip, 11112);


            Socket server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            //server.Blocking = false;

            // Create an End Point to capture info from the sending client
            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0); // 0 = any available port
            EndPoint remoteClient = (EndPoint)client;
            EndPoint remoteClient2 = (EndPoint)client;///

            // Bind, recieve data

            try
            {
                server.Bind(localEP);

                Console.WriteLine("Waiting for data...");

                while (true)
                {
                    int rec = server.ReceiveFrom(buffer, ref remoteClient); // rec = number of bytes
                    int rec2 = server.ReceiveFrom(buffer2, ref remoteClient2); // rec = number of bytes ///

                    Console.WriteLine("Recieved from: {0}", remoteClient.ToString());
                    //Console.WriteLine("Data: {0}", Encoding.ASCII.GetString(buffer, 0, rec));

                    // If client sends floats
                    Console.WriteLine("Client 1 X Data {0}", BitConverter.ToSingle(buffer, 0 * 4));
                    Console.WriteLine("Client 1 Y Data {0}", BitConverter.ToSingle(buffer, 1 * 4));
                    Console.WriteLine("Client 1 Z Data {0}", BitConverter.ToSingle(buffer, 2 * 4));

                    if (BitConverter.ToSingle(buffer, 3 * 4) == 1)
                    {
                        Console.WriteLine("Connected to Client");
                    }




                    Console.WriteLine("Recieved from: {0}", remoteClient2.ToString()); ///
                    //Console.WriteLine("Data: {0}", Encoding.ASCII.GetString(buffer, 0, rec)); ///

                    // If client sends floats ///
                    Console.WriteLine("Client 2 X Data {0}", BitConverter.ToSingle(buffer2, 0 * 4)); ///
                    Console.WriteLine("Client 2 Y Data {0}", BitConverter.ToSingle(buffer2, 1 * 4)); ///
                    Console.WriteLine("Client 2 Z Data {0}", BitConverter.ToSingle(buffer2, 2 * 4)); ///

                    if (BitConverter.ToSingle(buffer2, 3 * 4) == 1) ///
                    {
                        Console.WriteLine("Connected to Client"); ///
                    }

                    try
                    {
                        float[] pos = new float[] { BitConverter.ToSingle(buffer, 0 * 4) };
                        bpos = new byte[pos.Length * 4];

                        server.SendTo(bpos, localEP);

                        Console.WriteLine("Message Sending is: " + pos[pos.Length - 1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString() + " not sending");
                    }

                }

                //server.Shutdown(SocketShutdown.Both);
                //server.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }





        
        public static int Main(String[] args)
        {
            
                SetUpServer();
               

            while (true)
            {
                ReceiveData(buffer, 1, buffer2);
                ReceiveData(buffer2, 2, buffer2);

                //ReceiveData2(buffer3, 3);
                //ReceiveData(buffer4, 4);

                //SendData();

                //SendData2();

            }

            

            //StartServer();


            return 0;
        }
    }
}
