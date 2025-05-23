using System;
using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어의 이동, 점프, 카메라 회전 등 주요 조작을 처리하는 클래스
public class PlayerController : MonoBehaviour
{
    [Header("플레이어 움직임")]
    public float moveSpeed;                     // 이동 속도
    private Vector2 curMovementInput;           // 현재 입력된 이동 방향
    public float jumpPower;                     // 점프 힘
    public LayerMask groundLayerMask;           // 바닥 판정에 사용할 레이어

    [Header("플레이어 카메라")]
    public Transform cameraContainer;           // 카메라 회전용 컨테이너
    public float minXLook;                      // 카메라 아래 회전 제한
    public float maxXLook;                      // 카메라 위 회전 제한
    private float camCurXRot;                   // 현재 카메라 X축 회전값
    public float lookSensitivity;               // 마우스 감도

    private Vector2 mouseDelta;                 // 마우스 입력 변화값

    [HideInInspector]
    public bool canLook = true;                 // 카메라 회전 가능 여부

    private Rigidbody rb;                       // Rigidbody 컴포넌트 캐시

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();         // Rigidbody 컴포넌트 초기화
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 고정
    }

    private void FixedUpdate()
    {
        Move();                                    // 고정 시간마다 이동 처리
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();                          // 카메라 회전 처리
        }
    }

    // 마우스 움직임 입력 처리
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 키보드 이동 입력 처리
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>(); // 이동 방향 저장
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero; // 입력이 취소되면 멈춤
        }
    }

    // 점프 입력 처리
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); // 점프 힘 적용
        }
    }

    // 플레이어 이동 처리
    private void Move()
    {
        Vector3 moveDir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        Vector3 velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

        rb.velocity = velocity; // 물리 기반 이동
    }

    // 마우스 입력에 따른 카메라 회전 처리
    void CameraLook()
    {

        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // 바닥에 닿아 있는지 확인하는 함수 (네 방향으로 레이 발사)
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.1F), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, groundLayerMask))
            {
                return true;
            }
        }

        return false; // 네 방향 모두 땅이 아니면 false
    }

    // 외부에서 커서 상태를 강제로 토글할 수 있는 함수
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    // 점프대에서 호출하는 슈퍼 점프 기능
    public void SuperJump(float amout)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // 기존 수직 속도 제거
        rb.AddForce(Vector3.up * amout, ForceMode.Impulse);          // 큰 힘으로 점프
    }
}