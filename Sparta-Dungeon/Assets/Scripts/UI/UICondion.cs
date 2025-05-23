using UnityEngine;

// �÷��̾��� ü�� �� ���� ������ UI�� �������ִ� Ŭ����
public class UICondition : MonoBehaviour
{
    public Condition health; // ü�� ���¸� ǥ���ϴ� Condition �ν��Ͻ� (UI�� ���ε��� �� ����)

    private void Start()
    {
        // ���� ���� ��, �÷��̾� ���� �ý��ۿ��� �� UICondition�� ����
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
