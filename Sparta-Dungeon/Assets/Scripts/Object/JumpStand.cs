using UnityEngine;

public class JumpStand : MonoBehaviour
{
    public float power = 150.0f; // 점프에 사용할 힘의 크기 (기본값: 150)

    // 다른 오브젝트와 충돌했을 때 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌한 오브젝트에서 PlayerController 컴포넌트를 가져온다
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            // PlayerController가 존재할 경우, 슈퍼 점프 메서드 호출
            if (controller != null)
            {
                controller.SuperJump(power); // 플레이어에게 점프 힘 전달
            }
        }
    }
}