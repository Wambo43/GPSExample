﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour {

    public static GPS Instance { set; get; }
    public float latitude;
    public float longitude;

    private bool GPSAktive = false;

    private void Start () {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(Instance);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("user has not enabled GPS");
            GPSAktive = false;
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Time out");
            GPSAktive = false;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determin device loction");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        GPSAktive = true;
    }

    public bool isGPSEnable()
    {
        return GPSAktive;
    }
}
