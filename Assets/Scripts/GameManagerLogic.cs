using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;

    void Awake()
    {
        // UI에 표시할 정보
        // stageCountText.text = "/ " + totalItemCount.ToString();
    }

    // public void GetItem(int count)
    // {
    //     playerCountText.text = count.ToString();
    // }

    // TODO : 블럭이 필드 밖으로 떨어졌을 때, 게임 종료 판정 추가
    void OnTriggerEnter(Collider other)
    {
        // 박스가 떨어져 밖으로 나갔을 경우 이벤트 처리
        // if (other.gameObject.tag == "Box")
        //     return
    }
}
