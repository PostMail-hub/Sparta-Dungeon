using UnityEngine;

public class JumpStand : MonoBehaviour
{
    public float power = 150.0f; // ������ ����� ���� ũ�� (�⺻��: 150)

    // �ٸ� ������Ʈ�� �浹���� �� ȣ��Ǵ� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �浹�� ������Ʈ���� PlayerController ������Ʈ�� �����´�
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            // PlayerController�� ������ ���, ���� ���� �޼��� ȣ��
            if (controller != null)
            {
                controller.SuperJump(power); // �÷��̾�� ���� �� ����
            }
        }
    }
}