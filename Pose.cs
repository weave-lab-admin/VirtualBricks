using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Globalization;

public class Pose : MonoBehaviour {

    UdpClient listener;
    IPEndPoint ipep;
    float oldval = 0;
    public Vector3 position;
    public Vector3 orientation;
    
    void Start()
    {
        ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 33023);
        listener = new UdpClient(ipep);
    }

    private void Update()
    {
        try
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = listener.Receive(ref remote);
            string s = Encoding.ASCII.GetString(data);
            String[] tokens = s.Split(',');

            float x = float.Parse(tokens[0], CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat);

            position = new Vector3(x, y, z);

            float x1 = float.Parse(tokens[0], CultureInfo.InvariantCulture.NumberFormat);
            float y1 = float.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat);
            float z1 = float.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat);

            orientation = new Vector3(x1, y1, z1);
        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }
    Vector3 getPosition()
    {
        return position;
    }
    Vector3 getOrientation()
    {
        return orientation;
    }
}
