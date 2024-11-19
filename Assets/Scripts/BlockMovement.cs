using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool gravityEnabled = false;

    private CameraControl cameraControl;

    private Rigidbody rb;

    private void Start()
    {
        // CameraControl 컴포넌트 찾아서 할당
        cameraControl = FindObjectOfType<CameraControl>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;

        if (cameraControl == null)
        {
            Debug.LogError("Camera 엄준식");
        }
    }

    // TODO: 블럭을 이동시켰을 때, 필드 범위를 벗어나지 못하도록 방지하는 코드 추가 필요.
    private void Update()
    {
        // cameraControl이 null이면 작업 x
        if (cameraControl == null) return;

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
                movement.x -= 1; // x축 이동
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x += 1; // x축 이동
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableGravity();
        }
    }

    private void EnableGravity()
    {
        gravityEnabled = true;
        rb.useGravity = true;
    }
}
