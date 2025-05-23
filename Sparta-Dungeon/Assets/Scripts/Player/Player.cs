using UnityEngine;

// �÷��̾� ��ü�� �����ϴ� �ֿ� ������Ʈ���� �����ϴ� Ŭ����
public class Player : MonoBehaviour
{
    public PlayerController controller;             // �̵�, ����, ī�޶� ȸ�� �� ���� ���
    public PlayerConditions condition;              // ü��, ������ �� ���� ����
    public PlayerBuffController buffController;     // ���� ȿ�� ���� ����

    public ItemData itemData;                       // �÷��̾ ��� �ִ� ������ ���� (�׽�Ʈ��/�⺻ ������ ��)

    private void Awake()
    {
        // ���� ���� �� �ڽ��� CharacterManager�� ��� (���� ���� �����ϰ�)
        CharacterManager.Instance.Player = this;

        // �ֿ� ������Ʈ�� �ʱ�ȭ
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
        // buffController�� �ܺο��� ���� ����ǰų� ���� �ʱ�ȭ�� �� ����
    }
}