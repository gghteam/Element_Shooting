using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [field: SerializeField]
    public bool cameraPositionWithMouse { get; set; } = true;
    [SerializeField] private float cameraMoveSpeed = 6f;
    Vector3 originPos;
    private Camera myCamera;
    private Func<Vector3> GetCameraFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 size;
    private float height;
    private float width;

    private void Awake()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    void Start()
    {
        originPos = transform.localPosition;
        Setup(GetCameraPosition, () => 5f, true, true);
        height = Camera.main.orthographicSize;
        //월드 가로 = 월드 세로 * 스크린 가로 / 스크린 세로
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    private void Update()
    {

    }
    private void FixedUpdate() {
        HandleMovement();
    }

    public void ChangeMouse(bool isChange)
    {
        cameraPositionWithMouse = isChange;
    }

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc, Func<float> GetCameraZoomFunc, bool teleportToFollowPosition, bool instantZoom)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;

        if (teleportToFollowPosition)
        {
            Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
            cameraFollowPosition.z = transform.position.z;
            transform.position = cameraFollowPosition;
        }

        if (instantZoom)
        {
            myCamera.orthographicSize = GetCameraZoomFunc();
        }
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        if (GameManager.Instance.shield.isAni) yield break;
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + myCamera.transform.localPosition;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = myCamera.transform.localPosition;

    }

    private void HandleMovement()
    {
        if (GameManager.Instance.shield.isAni) return;
        if (GetCameraFollowPositionFunc == null) return;
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
 

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed* Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
            float lx = size.x * 0.5f - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

            float ly = size.y * 0.5f - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }

    private Vector3 GetCameraPosition()
    {
        if (cameraPositionWithMouse)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 playerToMouseDirection = mousePosition - followTransform.position;
            return followTransform.position + playerToMouseDirection.normalized * 2f;
        }
        else
        {
            return followTransform.position;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
