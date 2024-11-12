using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CameraControl cameraControl;

    private void Start()
    {
        // CameraControl 컴포넌트 찾아서 할당
        cameraControl = FindObjectOfType<CameraControl>();

        if (cameraControl == null)
        {
            Debug.LogError("Camera 엄준식");
        }
    }

    private void Update()
    {
        if (cameraControl == null) return; // cameraControl이 null이면 작업 x

        Vector3 movement = Vector3.zero;

        // 0: xCamera (z축만 이동)
        if (cameraControl.curCamera == 0)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.z += 1; // z축 이동
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.z -= 1; // z축 이동
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        // 1: yCamera (x, z축 이동 (상하좌우))
        else if (cameraControl.curCamera == 1)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x += 1; // x축 이동
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x -= 1; // x축 이동
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movement.z += 1; // z축 이동
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movement.z -= 1; // z축 이동
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        // 2: zCamera (x축만 이동)
        else if (cameraControl.curCamera == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x += 1; // x축 이동
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x -= 1; // x축 이동
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }
}
