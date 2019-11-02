using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource {
    ChilledWater = 0,
    HotWater = 1,
    Steam = 2,
    Electricity = 3,
    Unknown = 4,
}

public class Building
{
    public string name;
    public double latitude;
    public double longitude;
    public List<Resource> resources;
    public string purpose;

    public Building (string name, double lat, double lon, Resource res, string purpose) {
        this.name = name;
        this.latitude = lat;
        this.longitude = lon;
        this.resources = new List<Resource>();
        this.resources.Add(res);
        this.purpose = purpose;
    }

    public void addResource (Resource res) {
        this.resources.Add(res);
    }
}
