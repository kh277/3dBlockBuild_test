using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CameraControl cameraControl;

    private void Start()
    {
        // CameraControl ������Ʈ ã�Ƽ� �Ҵ�
        cameraControl = FindObjectOfType<CameraControl>();

        if (cameraControl == null)
        {
            Debug.LogError("Camera ���ؽ�");
        }
    }

    private void Update()
    {
        if (cameraControl == null) return; // cameraControl�� null�̸� �۾� x

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
                movement.x += 1; // x�� �̵�
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x -= 1; // x�� �̵�
            }

            transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }
}
