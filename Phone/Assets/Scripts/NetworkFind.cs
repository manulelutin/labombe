using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class NetworkFind : MonoBehaviour {

    public string test;

    public void Start() {
        Debug.Log("ehehehe");
        LocalIPAddress();
    }

    public string LocalIPAddress() {
        IPHostEntry host;
        string localIP = "";
        Debug.Log("hey");
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.Aliases ) {
            Debug.Log(ip);
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
