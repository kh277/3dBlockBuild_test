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
    private float _cameraScaleMin = 8.5f;       // 카메라 확대 범위 제한
    private float _cameraScaleMax = 15f;        // 카메라 축소 범위 제한

    // 게임 시작 시 enable 할 카메라 
    [SerializeField]
    private int startCamera = 0;

    // 카메라 이동 관련 변수
    public int curCamera;

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
        initCamera();
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

    // 시작 시 카메라를 startCamera로 설정
    private void initCamera()
    {
        cameraType[(startCamera+1)%3].enabled = false;
        cameraType[(startCamera+2)%3].enabled = false;
        cameraType[startCamera].enabled = true;
        curCamera = startCamera;
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
        // 활성화된 카메라 별 카메라 이동 처리
        if (curCamera == 0)         // x축 카메라
            CameraMoveX();
        else if (curCamera == 1)    // y축 카메라
            CameraMoveY();
        else if (curCamera == 2)    // z축 카메라
            CameraMoveZ();
    }

    // x카메라 이동
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            p_Velocity += new Vector3(-1f, 0, 0);
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            p_Velocity += new Vector3(1f, 0, 0);
        
        // p_Velocity가 0이 아닐 경우에만 이동 수행
        if (p_Velocity.sqrMagnitude > 0)
        {
            // cameraSpeed에 따라 카메라 이동
            Vector3 movement = p_Velocity.normalized * cameraSpeed * Time.deltaTime;
            Vector3 curCameraPos = cameraType[curCamera].transform.position;

            // 이동 값 적용 후 제한된 범위로 설정
            float newY = Mathf.Clamp(curCameraPos.y + movement.y, 0.5f, _cameraLimit);
            float newZ = Mathf.Clamp(curCameraPos.z + movement.z, -_cameraLimit, _cameraLimit);

            // 마우스 휠에 따라 카메라 확대-축소 처리, 1배율로 하면 너무 느려서 *3를 해줌.
            float newX = Mathf.Clamp(curCameraPos.x + movement.x*3, _cameraScaleMin, _cameraScaleMax);

            // 제한된 위치로 카메라 이동
            cameraType[curCamera].transform.position = new Vector3(newX, newY, newZ);
        }
    }

    // y카메라 이동
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            p_Velocity += new Vector3(0, -1f, 0);
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            p_Velocity += new Vector3(0, 1f, 0);
        
        // p_Velocity가 0이 아닐 경우에만 이동 수행
        if (p_Velocity.sqrMagnitude > 0)
        {
            // cameraSpeed에 따라 카메라 이동
            Vector3 movement = p_Velocity.normalized * cameraSpeed * Time.deltaTime;
            Vector3 curCameraPos = cameraType[curCamera].transform.position;

            // 이동 값 적용 후 제한된 범위로 설정
            float newX = Mathf.Clamp(curCameraPos.x + movement.x, -_cameraLimit, _cameraLimit);
            float newZ = Mathf.Clamp(curCameraPos.z + movement.z, -_cameraLimit, _cameraLimit);

            // 마우스 휠에 따라 카메라 확대-축소 처리, 1배율로 하면 너무 느려서 *3를 해줌.
            float newY = Mathf.Clamp(curCameraPos.y + movement.y*3, _cameraScaleMin, _cameraScaleMax);

            // 제한된 위치로 카메라 이동
            cameraType[curCamera].transform.position = new Vector3(newX, newY, newZ);
        }
    }

    // z카메라 이동
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            p_Velocity += new Vector3(0, 0, -1f);
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            p_Velocity += new Vector3(0, 0, 1f);
        
        // p_Velocity가 0이 아닐 경우에만 이동 수행
        if (p_Velocity.sqrMagnitude > 0)
        {
            // cameraSpeed에 따라 카메라 이동
            Vector3 movement = p_Velocity.normalized * cameraSpeed * Time.deltaTime;
            Vector3 curCameraPos = cameraType[curCamera].transform.position;

            // 이동 값 적용 후 제한된 범위로 설정
            float newX = Mathf.Clamp(curCameraPos.x + movement.x, -_cameraLimit, _cameraLimit);
            float newY = Mathf.Clamp(curCameraPos.y + movement.y, 0.5f, _cameraLimit);

            // 마우스 휠에 따라 카메라 확대-축소 처리, 1배율로 하면 너무 느려서 *3를 해줌.
            float newZ = Mathf.Clamp(curCameraPos.z + movement.z*3, _cameraScaleMin, _cameraScaleMax);

            // 제한된 위치로 카메라 이동
            cameraType[curCamera].transform.position = new Vector3(newX, newY, newZ);
        }
    }
}
