using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Globalization;


public class RotoryEncoder : MonoBehaviour {
    
    public GameObject wheel;
    UdpClient listener;
    IPEndPoint ipep;

    float oldval = 0;
    private float h;
    private float angle;
    private float speed;

    // Use this for initialization
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
            float val = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
            Angle(val);
            Speed(val, oldval);
            oldval = h;
        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }
    //Use to find number of rotations made by the object
    public float getRotations(float val)
    {
        print(val);
        return val;
    }
    //Use to find the angle rotated(in degrees) 
    public float getAngle(float val)
    {
        h = val / 20.0f;
        angle = h;
        print(h);
        return h;
    }
    //Use to find the speed(per time frame) with which the object is rotated 
    public void getSpeed(float val,float oldval)
    {
        h = val / 20.0f;
        h = h - oldval;
        speed = h * 60f;
        print(h);
    }
}
