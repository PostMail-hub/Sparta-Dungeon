using UnityEngine;

// ��ȣ�ۿ� ������ ������Ʈ�� ���� �������̽� ����
public interface IInteractable
{
    public string GetInteractPrompt(); // ��ȣ�ۿ� �� ǥ���� ������Ʈ(����) ��ȯ
    public void OnInteract();          // ���� ��ȣ�ۿ� ���� �޼���
}

// ������ ������Ʈ Ŭ����, IInteractable �������̽��� ������
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data; // �������� ������ (ScriptableObject�� ������)

    // ��ȣ�ۿ� ������Ʈ�� ���ڿ��� ��ȯ (������ �̸� + ����)
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    // �÷��̾ ��ȣ�ۿ��� �� ����Ǵ� �޼���
    public void OnInteract()
    {
        // �÷��̾� ���� �ý��� ��������
        PlayerConditions player = CharacterManager.Instance.Player.condition;

        // �����ۿ� ���Ե� �Ҹ� ȿ���� ��� ��ȸ
        foreach (var consumable in data.consumables)
        {
            // ü�� ���� �Ҹ�ǰ�� ���
            if (consumable.type == ConsumableType.Health)
            {
                float value = consumable.value;

                if (value >= 0)
                    player.Heal((int)value); // ü�� ȸ��

                else
                    player.TakePhysicalDamage((int)-value); // ������ ������ ó��
            }

            // ���� ���� �Ҹ�ǰ�� ���
            else if (consumable.type == ConsumableType.Buff)
            {
                var buffData = data.buffData;

                if (buffData != null)
                {
                    // �÷��̾�� ���� ��Ʈ�ѷ��� �ִ� ��� ���� ����
                    PlayerBuffController buffController = CharacterManager.Instance.Player.GetComponent<PlayerBuffController>();
                    buffController.ApplyBuff(buffData.duration, buffData.speedMultiplier, buffData.jumpMultiplier);
                }
            }
        }
        //����� �������� �Ҹ�� ���� �������̹Ƿ� �ʿ��� ������Ʈ ����
        Destroy(gameObject);
    }
}