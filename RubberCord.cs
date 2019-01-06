using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Globalization;

public class RubberCord : MonoBehaviour {

    public GameObject wheel;
    UdpClient listener;
    IPEndPoint ipep;
    float oldval = 0;
    float h = 0;
    float fov;
    float maxStretch;
    float minStretch;
    private int count = 0;
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
            Move(val);
        }
        catch (Exception e)
        {
            print(e.ToString());
        }

    }
    //for a 10cm rubber cord 
    public float Move(float val)
    {
        fov = (val / 1024.0f) * 40.0f;
        return fov;
    }

    //Use to find the speed of the holder gameobject which is cotrolled by the user
    //from 0 to 100 mapped
    public float Speed()
    {
        float speed = holder.transform.GetComponent<Rigidbody>().velocity.magnitude;
        /*
         * speed=(speed/maxSpeed)*100f;
         */
        return speed;
    }
    //Gives 1 if the resistive chord is more than half stretched else gives 0  
    public int discreteStretch(float val)
    {
        if (val > (maxStretch - minStretch) / 2f)
        {
            count++;
            return 1;
        }
        else
            return 0;
    }
    // counts the number of time the chord has been stretched more than half
    public int Count()
    {
        return count;
    }
}
