using System;
using UnityEngine;
using UnityEngine.InputSystem;

// �÷��̾��� �̵�, ����, ī�޶� ȸ�� �� �ֿ� ������ ó���ϴ� Ŭ����
public class PlayerController : MonoBehaviour
{
    [Header("�÷��̾� ������")]
    public float moveSpeed;                     // �̵� �ӵ�
    private Vector2 curMovementInput;           // ���� �Էµ� �̵� ����
    public float jumpPower;                     // ���� ��
    public LayerMask groundLayerMask;           // �ٴ� ������ ����� ���̾�

    [Header("�÷��̾� ī�޶�")]
    public Transform cameraContainer;           // ī�޶� ȸ���� �����̳�
    public float minXLook;                      // ī�޶� �Ʒ� ȸ�� ����
    public float maxXLook;                      // ī�޶� �� ȸ�� ����
    private float camCurXRot;                   // ���� ī�޶� X�� ȸ����
    public float lookSensitivity;               // ���콺 ����

    private Vector2 mouseDelta;                 // ���콺 �Է� ��ȭ��

    [HideInInspector]
    public bool canLook = true;                 // ī�޶� ȸ�� ���� ����

    private Rigidbody rb;                       // Rigidbody ������Ʈ ĳ��

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();         // Rigidbody ������Ʈ �ʱ�ȭ
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 Ŀ�� ����
    }

    private void FixedUpdate()
    {
        Move();                                    // ���� �ð����� �̵� ó��
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();                          // ī�޶� ȸ�� ó��
        }
    }

    // ���콺 ������ �Է� ó��
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // Ű���� �̵� �Է� ó��
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>(); // �̵� ���� ����
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero; // �Է��� ��ҵǸ� ����
        }
    }

    // ���� �Է� ó��
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); // ���� �� ����
        }
    }

    // �÷��̾� �̵� ó��
    private void Move()
    {
        Vector3 moveDir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        Vector3 velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

        rb.velocity = velocity; // ���� ��� �̵�
    }

    // ���콺 �Է¿� ���� ī�޶� ȸ�� ó��
    void CameraLook()
    {

        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // �ٴڿ� ��� �ִ��� Ȯ���ϴ� �Լ� (�� �������� ���� �߻�)
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

        return false; // �� ���� ��� ���� �ƴϸ� false
    }

    // �ܺο��� Ŀ�� ���¸� ������ ����� �� �ִ� �Լ�
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    // �����뿡�� ȣ���ϴ� ���� ���� ���
    public void SuperJump(float amout)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // ���� ���� �ӵ� ����
        rb.AddForce(Vector3.up * amout, ForceMode.Impulse);          // ū ������ ����
    }
}