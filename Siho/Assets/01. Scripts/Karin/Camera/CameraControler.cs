using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public static CameraControler instance;

    [SerializeField] private CinemachineCamera _mainCam;
    [SerializeField] private CinemachineCamera _zoomCam;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;

        _zoomCam.Priority = 0;
        _mainCam.Priority = 1;
    }

    public void ZoomIn()
    {
        _zoomCam.Priority = 1;
        _mainCam.Priority = 0;
    }

    public void ZoomOut()
    {
        _zoomCam.Priority = 0;
        _mainCam.Priority = 1;
    }
}
