using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float leftLimit, rightLimit, topLimit, bottomLimit, zoomMax, zoomMin;

    private Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {
        ZoomCamera();
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += diff;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                                            Mathf.Clamp(transform.position.y, bottomLimit, topLimit), transform.position.z);
        }
    }

    private void ZoomCamera()
    {
        if (cam.orthographic)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
        else
        {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomMin, zoomMax);
    }
}
