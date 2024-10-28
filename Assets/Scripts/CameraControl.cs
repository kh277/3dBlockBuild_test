using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // 카메라 이동 속도 설정
    [SerializeField]
    private float cameraSpeed = 5f;

    // 카메라 이동 범위 제한 처리
    [SerializeField]
    private float _cameraLimit;     // 카메라 최대 이동 범위 제한(5f)

    // 카메라 이동 관련 변수
    private int curCamera = 0;

    // 필요 컴포넌트
    [SerializeField]
    private Camera xCamera;
    [SerializeField]
    private Camera yCamera;
    [SerializeField]
    private Camera zCamera;

    Camera[] cameraType;
    
    void Start()
    {
        cameraType = new Camera[] {xCamera, yCamera, zCamera};
    }

    void Update()
    {
        // 카메라 x, y, z 전환
        if (Input.GetKeyDown(KeyCode.Z))
        {
            curCamera = (curCamera + 1) % 3;
            ChangeCamera();
        }
        // 카메라 이동 처리
        CameraMove();
    }

    // x, y, z 방향 카메라 전환 함수
    private void ChangeCamera()
    {
        cameraType[(curCamera+2)%3].enabled = false;
        cameraType[curCamera].enabled = true;

        // Audio Listener 컴포넌트를 카메라에서 삭제하여 처리함. 필요하면 추가할 것.
        // foreach (var cam in cameraType)
        // {
        //     cam.enabled = false;
        //     cam.GetComponent<AudioListener>().enabled = false;
        // }
        
        // cameraType[curCamera].enabled = true;
        // cameraType[curCamera].GetComponent<AudioListener>().enabled = true;
    }
    
    // 카메라 이동 처리 함수
    private void CameraMove()
    {
        Vector3 p_Velocity = new Vector3();

        // 활성화된 카메라 별 카메라 이동 처리
        if (curCamera == 0)         // x축 카메라
            CameraMoveX();
        else if (curCamera == 1)    // y축 카메라
            CameraMoveY();
        else if (curCamera == 2)    // z축 카메라
            CameraMoveZ();
    }

    // x방향 카메라 이동
    private void CameraMoveX()
    {
        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
            p_Velocity += new Vector3(0, 1f, 0);
        if (Input.GetKey(KeyCode.S))
            p_Velocity += new Vector3(0, -1f, 0);
        if (Input.GetKey(KeyCode.A))
            p_Velocity += new Vector3(0, 0, -1f);
        if (Input.GetKey(KeyCode.D))
            p_Velocity += new Vector3(0, 0, 1f);
        
        // p_Velocity가 0이 아닐 경우에만 이동 수행
        if (p_Velocity.sqrMagnitude > 0)
        {
            // cameraSpeed에 따라 카메라 이동
            Vector3 movement = p_Velocity.normalized * cameraSpeed * Time.deltaTime;
            Vector3 curCameraPos = cameraType[curCamera].transform.position;

            // 이동 값 적용 후 제한된 범위로 설정
            float newY = Mathf.Clamp(curCameraPos.y + movement.y, 0.5f, _cameraLimit);
            float newZ = Mathf.Clamp(curCameraPos.z + movement.z, -_cameraLimit, _cameraLimit);

            // 제한된 위치로 카메라 이동
            cameraType[curCamera].transform.position = new Vector3(curCameraPos.x, newY, newZ);
        }
    }

    // y방향 카메라 이동
    private void CameraMoveY()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
            p_Velocity += new Vector3(0, 0, 1f);
        if (Input.GetKey(KeyCode.S))
            p_Velocity += new Vector3(0, 0, -1f);
        if (Input.GetKey(KeyCode.A))
            p_Velocity += new Vector3(-1f, 0, 0);
        if (Input.GetKey(KeyCode.D))
            p_Velocity += new Vector3(1f, 0, 0);
        
        // p_Velocity가 0이 아닐 경우에만 이동 수행
        if (p_Velocity.sqrMagnitude > 0)
        {
            // cameraSpeed에 따라 카메라 이동
            Vector3 movement = p_Velocity.normalized * cameraSpeed * Time.deltaTime;
            Vector3 curCameraPos = cameraType[curCamera].transform.position;

            // 이동 값 적용 후 제한된 범위로 설정
            float newX = Mathf.Clamp(curCameraPos.x + movement.x, -_cameraLimit, _cameraLimit);
            float newZ = Mathf.Clamp(curCameraPos.z + movement.z, -_cameraLimit, _cameraLimit);

            // 제한된 위치로 카메라 이동
            cameraType[curCamera].transform.position = new Vector3(newX, curCameraPos.y, newZ);
        }
    }

    // xyz방향 카메라 이동
    private void CameraMoveZ()
    {
        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
            p_Velocity += new Vector3(0, 1f, 0);
        if (Input.GetKey(KeyCode.S))
            p_Velocity += new Vector3(0, -1f, 0);
        if (Input.GetKey(KeyCode.A))
            p_Velocity += new Vector3(1f, 0, 0);
        if (Input.GetKey(KeyCode.D))
            p_Velocity += new Vector3(-1f, 0, 0);
        
        // p_Velocity가 0이 아닐 경우에만 이동 수행
        if (p_Velocity.sqrMagnitude > 0)
        {
            // cameraSpeed에 따라 카메라 이동
            Vector3 movement = p_Velocity.normalized * cameraSpeed * Time.deltaTime;
            Vector3 curCameraPos = cameraType[curCamera].transform.position;

            // 이동 값 적용 후 제한된 범위로 설정
            float newX = Mathf.Clamp(curCameraPos.x + movement.x, -_cameraLimit, _cameraLimit);
            float newY = Mathf.Clamp(curCameraPos.y + movement.y, 0.5f, _cameraLimit);

            // 제한된 위치로 카메라 이동
            cameraType[curCamera].transform.position = new Vector3(newX, newY, curCameraPos.z);
        }
    }
}
