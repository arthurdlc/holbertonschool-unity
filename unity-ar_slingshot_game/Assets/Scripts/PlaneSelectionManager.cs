using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaneSelectionManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject startButton;

    public static ARPlane selectedPlane;

    private bool planeSelected = false;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (planeSelected || Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                ARPlane plane = planeManager.GetPlane(hits[0].trackableId);
                SelectPlane(plane);
            }
        }
    }

    void SelectPlane(ARPlane plane)
    {
        selectedPlane = plane;
        planeSelected = true;

        // Désactiver tous les autres plans
        foreach (var p in planeManager.trackables)
        {
            if (p != selectedPlane)
                p.gameObject.SetActive(false);
        }

        // Désactiver la détection de nouveaux plans
        planeManager.enabled = false;

        // Afficher le bouton Start
        if (startButton != null)
            startButton.SetActive(true);
    }
}
