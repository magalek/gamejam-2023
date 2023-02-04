using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 zoomRange; 
    [SerializeField] private float zoomTime;
    [SerializeField, Range(0f, 0.1f)] private float followSpeed; 
    
    public static CameraController Instance => instance;

    private static CameraController instance;

    private readonly Vector3 Offset = new Vector3(0, 0, -10);
    
    public Camera cameraRef;

    private PlayerMovement playerMovement;
    
    private Coroutine zoomCoroutine;

    private float movementT;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }

        cameraRef = GetComponent<Camera>();
    }

    private void Start()
    {
        playerMovement = PlayerController.Instance.Movement;
        playerMovement.Moved += OnMoved;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void OnMoved(Vector2 movement)
    {
        movementT = 0;
    }

    private void FollowPlayer()
    {
        Vector3 targetPos = playerMovement.transform.position + Offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, followSpeed);

        transform.position = smoothFollow;
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
