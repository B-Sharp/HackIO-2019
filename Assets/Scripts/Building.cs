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
    public string meterID;

    public double largestDailyValue = 0.0;

    public Building (string name, double lat, double lon, Resource res, string purpose, string meterID) {
        this.name = name;
        this.latitude = lat;
        this.longitude = lon;
        this.resources = new List<Resource>();
        this.resources.Add(res);
        this.purpose = purpose;
        this.meterID = meterID;
    }

    public void addResource (Resource res) {
        this.resources.Add(res);
    }

    public void setLargestDailyValue (double val) {
        this.largestDailyValue = largestDailyValue;
    }
}
