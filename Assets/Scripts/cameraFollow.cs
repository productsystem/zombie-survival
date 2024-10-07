using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float zoomSpeed = 2f; 
    public float minZoom = 3f;
    public float maxZoom = 6f;
    public float zoomAmount = 1f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float targetZoom = Mathf.Clamp(cam.orthographicSize - scrollInput * zoomAmount, minZoom, maxZoom);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.fixedDeltaTime * zoomSpeed);
        }
    }
}

