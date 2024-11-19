using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool gravityEnabled = false;

    private CameraControl cameraControl;

    private Rigidbody rb;

    private void Start()
    {
        // CameraControl ������Ʈ ã�Ƽ� �Ҵ�
        cameraControl = FindObjectOfType<CameraControl>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;

        if (cameraControl == null)
        {
            Debug.LogError("Camera ���ؽ�");
        }
    }

    // TODO: ���� �̵������� ��, �ʵ� ������ ����� ���ϵ��� �����ϴ� �ڵ� �߰� �ʿ�.
    private void Update()
    {
        // cameraControl�� null�̸� �۾� x
        if (cameraControl == null) return;

        Vector3 movement = Vector3.zero;

        // 0: xCamera (z�ุ �̵�)
        if (cameraControl.curCamera == 0)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.z += 1; // z�� �̵�
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.z -= 1; // z�� �̵�
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        // 1: yCamera (x, z�� �̵� (�����¿�))
        else if (cameraControl.curCamera == 1)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x += 1; // x�� �̵�
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x -= 1; // x�� �̵�
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movement.z += 1; // z�� �̵�
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movement.z -= 1; // z�� �̵�
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        // 2: zCamera (x�ุ �̵�)
        else if (cameraControl.curCamera == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x -= 1; // x�� �̵�
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x += 1; // x�� �̵�
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
