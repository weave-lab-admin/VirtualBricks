using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Globalization;

public class ProximitySensor : MonoBehaviour {

    public GameObject wheel;
    UdpClient listener;
    IPEndPoint ipep;
    float oldval = 0;
    float min = 0f;
    float max = 1f;
    float maxSpeed=0f;
    float minSpeed=0f;
    float fov;
    public float minVal,maxVal;
    public GameObject holder;

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
            getDistance(val);
        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }
    //Use to find the distance of an object from 0m to 1m mapped
    public float getDistance(float val)
    {
        if (val > oldval + 5f || val < oldval - 5f)
        {
            fov = minVal + val/1000f*(max-min) ;
            oldval = val;
        }
        fov = Mathf.Clamp(fov, min, max);
        return fov;
    }
    //Use to find the speed of the holder gameobject which is cotrolled by the user
    //from 0 to 100 mapped
    public float getChange()
    {
        float speed=holder.transform.GetComponent<Rigidbody>().velocity.magnitude;
        /*
         * speed=(speed/maxSpeed)*100f;
         */
        return speed;
    }
    //Use as a switch to get value either 0 or 1 
    //Gives 0 if speed is less than half of the ma
    public int getBinary(float val)
    {
        if (getDistance(val) < 0.5f)
        {
            return 0;
        }
        else
            return 1;
    }
}

