using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item; // 아이템 데이터

    public UIInventory inventory;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText; // 수량 표시 Text
    private Outline outline; // 선택 했을 때 Outline 을 표시하기 위한 컴포넌트

    public int index; // 슬롯마다 부여할 인덱스 번호
    public int quantity; // 아이템 갯수 데이터

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}