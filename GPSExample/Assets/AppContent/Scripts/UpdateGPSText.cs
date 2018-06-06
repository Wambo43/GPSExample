using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateGPSText : MonoBehaviour {

    public Text m_Coordinates;
    //public GeoCoordManager geoCoordManager;

    private void Start()
    {
        DisableAutoRotation();
    }

    public void DisableAutoRotation()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Update()
    {
        SetText();
    }

    public void SetText()
    {
        bool FoundGeoCoord = false;

        foreach (var Coord in GeoCoordManager.Instance.getCoordinats())
        {
            if (Coord.isLocation())
            {
                m_Coordinates.text = Coord.Message;
                FoundGeoCoord = true;
                break;
            }
        }
        if(!FoundGeoCoord)
        {
            m_Coordinates.text = "Lat: " + GPS.Instance.latitude.ToString() + "\nLon: " + GPS.Instance.longitude.ToString();
        }
    }

    //per Button die localen GPS Daten speichern
    public void btnAddCoord()
    {
        var C = new Coordinat(GPS.Instance.latitude, GPS.Instance.longitude, 0.5f, "Selbst erstellt");
        GeoCoordManager.Instance.addCoord(C);
    }

}
