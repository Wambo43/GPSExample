using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoCoordManager : MonoBehaviour {


    public static GeoCoordManager Instance = null;

    private List<Coordinat> GeoCoords;

    //Geokoordinaten für die Testszenarios
    //Blankenhain lat: 50.859548 | lon: 11.342719
    //Bad Berka   lat: 50.899934 | lon: 11.28106
    //Erfurt Anger lat: 50.97641 | lon: 11.03469

    void Awake () {
        //Check if instance already exists
        if (Instance == null)
            //if not, set instance to this
            Instance = this;
        //If instance already exists and it's not this:
        else if (Instance != null)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        InitGame();
    }
	
	// Update is called once per frame
	private void InitGame() {
        GeoCoords = new List<Coordinat>();
        GeoCoords.Add( new Coordinat(50.859548, 11.342719, 5.0, "Willkommen zuhause"));
        GeoCoords.Add( new Coordinat(50.899934, 11.28106,  5.0, "bettel Berke"));
        GeoCoords.Add( new Coordinat(50.97641,  11.03469,  0.1, "Willkommen am Anger"));
        GeoCoords.Add( new Coordinat(50.982203, 11.019746, 0.1, "Hier Warten"));
    }

    public void addCoord(Coordinat _C)
    {
        if(!GeoCoords.Contains(_C))
            GeoCoords.Add(_C);
    }

    public List<Coordinat> getCoordinats()
    {
        if(GeoCoords == null)
            return null;
        return GeoCoords;
    }

    public void remove(Coordinat _C)
    {
        GeoCoords.Remove(_C);
    }
    

    public int count()
    {
        return GeoCoords.Count;
    }
}
