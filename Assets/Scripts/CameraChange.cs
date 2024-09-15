using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraChange : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomedOutSize = 10f;

    [SerializeField] private float zoomSpeed = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the trigger area
        if (other.CompareTag("Player"))
        {
            // Start the smooth zoom out coroutine
            StartCoroutine(SmoothZoomOut());
        }
    }

    private IEnumerator SmoothZoomOut()
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < zoomSpeed)
        {
            // Smoothly interpolate between the current size and the target zoomed out size
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, zoomedOutSize, elapsedTime / zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is exactly the target size
        virtualCamera.m_Lens.OrthographicSize = zoomedOutSize;
    }
}