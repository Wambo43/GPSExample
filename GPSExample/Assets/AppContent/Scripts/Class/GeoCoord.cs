using System;
using UnityEngine;

[Serializable]
public class Coordinat : IEquatable<Coordinat>
{
    //Breite
    public double Lat { set; get; }
    //länge
    public double Lon { set; get; }
    public double Dist { set; get; }
    public string Message { set; get; }

    private float Earthrad = 6371000.0f;

    public Coordinat()
    {
        this.Lat = 0;
        this.Lon = 0;
        this.Dist = 0;
        this.Message = "Standarts";
    }

    //Konstruktor
    public Coordinat(double _lat, double _lon, double _dist, string _text)
    {
        this.Lat = _lat;
        this.Lon = _lon;
        this.Dist = _dist;
        this.Message = _text;
    }

    //Distance berechnung für kurze Strecken
    //Strecken Distance bis zu 20km
    public double Distance()
    {
        //lat: 50.859548 | lon: 11.342719
        double dx = 71.5 * (Lon - GPS.Instance.longitude);
        double dy = 111.3 * (Lat - GPS.Instance.latitude);

        //Testen
        //double dx = 71.5 * (Lon - 11.342719);
        //double dy = 111.3 * (Lat - 50.859548);

        return Math.Sqrt(dx * dx + dy * dy);
    }

    public double Distance(double _Lon, double _Lat)
    {
        double dx = 71.5 * (Lon - _Lon);
        double dy = 111.3 * (Lat - _Lat);
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public bool isLocation()
    {
        if (this.Dist > Distance())
            return true;
        return false;
    }

    //Umwandunglich in Kartesische Zahlensystem
    public Vector3 TransformToCaresian(float _long, float _lat)
    {
        // Umwandlung der Kugel-Koordinaten ins kathesische Koordinatensystem:
        // Umrechnung ins Bogenmaß
        float lambda = _long * (float)Math.PI / 180.0f;
        float phi = _lat * (float)Math.PI / 180.0f;

        float geoCoordx = (float)Math.Round(Earthrad * Math.Cos(phi) * Math.Cos(lambda));
        float geoCoordy = (float)Math.Round(Earthrad * Math.Cos(phi) * Math.Sin(lambda));
        float geoCoordz = (float)Math.Round(Earthrad * Math.Sin(phi));

        return new Vector3(geoCoordx, geoCoordy, geoCoordz);
    }

    //Interface überschrieben
    public bool Equals(Coordinat _C)
    {
        if (0.05 > _C.Distance(this.Lon, this.Lat))
            return true;
        return false;
    }
}

