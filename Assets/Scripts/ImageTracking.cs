using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonReferences;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;


    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        foreach (var buttonReference in buttonReferences)
        {
            spawnedPrefabs.Add(buttonReference.name, buttonReference);
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedButton = spawnedPrefabs[name];
        if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None)
            trackedButton.SetActive(false);
        else
            trackedButton.SetActive(true);

            foreach (var otherButton in spawnedPrefabs.Values)
        {
            if(otherButton.name == name) return;
            otherButton.SetActive(false);
        }
    }


    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
}
