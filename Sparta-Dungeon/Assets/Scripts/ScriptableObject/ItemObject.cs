using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        PlayerConditions player = CharacterManager.Instance.Player.condition;
        foreach (var consumable in data.consumables)
        {
            if (consumable.type == ConsumableType.Health)
            {
                float value = consumable.value;

                if (value >= 0)
                    player.Heal((int)value); // 회복

                else
                    player.TakePhysicalDamage((int)-value); // 데미지
            }

            else if (consumable.type == ConsumableType.Buff)
            {
                var buffData = data.buffData;

                if (buffData != null)
                {
                    PlayerBuffController buffController = CharacterManager.Instance.Player.GetComponent<PlayerBuffController>();
                    buffController.ApplyBuff(buffData.duration, buffData.speedMultiplier, buffData.jumpMultiplier);
                }
            }
        }
        Destroy(gameObject);
    }
}