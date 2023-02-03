using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 zoomRange; 
    [SerializeField] private float zoomTime; 
    
    public static CameraController Instance => instance;

    private static CameraController instance;

    public Camera cameraRef;

    private Coroutine zoomCoroutine;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }

        cameraRef = GetComponent<Camera>();
    }

    public void ZoomIn() => LerpZoomTo(zoomRange.x);

    public void ZoomOut() => LerpZoomTo(zoomRange.y);

    private void LerpZoomTo(float desiredZoom)
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }

        zoomCoroutine = StartCoroutine(ZoomCoroutine());
        
        IEnumerator ZoomCoroutine()
        {
            float time = 0;

            float currentZoom = cameraRef.orthographicSize;
            
            while (time < zoomTime)
            {
                time += Time.deltaTime;

                var interpolatedT = Mathf.Clamp01(time / zoomTime);

                cameraRef.orthographicSize = Mathf.SmoothStep(currentZoom, desiredZoom, interpolatedT);
                
                yield return null;
            }
            cameraRef.orthographicSize = desiredZoom;
        }
    }
}
