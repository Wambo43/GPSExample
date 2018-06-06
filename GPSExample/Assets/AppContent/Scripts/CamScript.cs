using System;
using UnityEngine;
using UnityEngine.UI;

public class CamScript : MonoBehaviour {

    public WebCamTexture mCamera = null;
    public GameObject plane;
    //public Text text;

    private Vector3 rotationVector = new Vector3(0.0f, 0.0f, 0.0f);

    // Image uvRect
    private Rect defaultRect = new Rect(0f, 0f, 1f, 1f);
    private Rect fixedRect = new Rect(0f, 1f, 1f, -1f);

    // Use this for initialization
    void Start()
    {
        Debug.Log("Script has been started");

        if (mCamera == null)
        {
            mCamera = new WebCamTexture(Screen.width,Screen.height);
           // mCamera = new WebCamTexture();
        }

        if(!mCamera.isPlaying)
            mCamera.Play();

        plane.GetComponent<RawImage>().texture = mCamera;
    }

    void Update()
    {
        // Skip making adjustment for incorrect camera data
        if (mCamera.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        // Rotate image to show correct orientation 
        rotationVector.z = -mCamera.videoRotationAngle;
        plane.GetComponent<RawImage>().rectTransform.localEulerAngles = rotationVector;

        // Set AspectRatioFitter's ratio
        float videoRatio =
            (float)mCamera.width / (float)mCamera.height;
        plane.GetComponent<AspectRatioFitter>().aspectRatio = videoRatio;

        //// Unflip if vertically flipped
        //plane.GetComponent<RawImage>().uvRect =
        //    mCamera.videoVerticallyMirrored ? fixedRect : defaultRect;


        if (mCamera.videoVerticallyMirrored)
        {
            //HORIZONTAL
            plane.GetComponent<RawImage>().uvRect = fixedRect;  // flip on HORIZONTAL axis
        }
        else
        {
            //VERTICAL
            plane.GetComponent<RawImage>().uvRect = defaultRect; // no flip
        }
            
    }
}
