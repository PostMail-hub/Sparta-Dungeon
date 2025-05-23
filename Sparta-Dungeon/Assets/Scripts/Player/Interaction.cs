using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// �÷��̾ �ֺ� ������Ʈ�� ��ȣ�ۿ��� �� �ֵ��� �ϴ� ������Ʈ
public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;               // ����ĳ��Ʈ üũ �ֱ� (�� ����)
    private float lastCheckTime;                  // ������ üũ �ð� ���
    public float maxCheckDistance;                // ��ȣ�ۿ� ������ �ִ� �Ÿ�
    public LayerMask layerMask;                   // ��ȣ�ۿ� ���� ��� ���̾� ����ũ

    public GameObject curInteractGameObject;      // ���� �ٶ󺸰� �ִ� ��ȣ�ۿ� ���
    private IInteractable curInteractable;        // ���� �ٶ󺸰� �ִ� ��ȣ�ۿ� ������ �������̽�

    public TextMeshProUGUI promptText;            // ��ȣ�ۿ� ������Ʈ �ؽ�Ʈ UI
    private Camera camera;                        // ���� ī�޶� ����

    void Start()
    {
        camera = Camera.main;                     // ���� ī�޶� ����

        // ������Ʈ �ؽ�Ʈ�� ������� �ʾ��� ��� �ڵ����� ã�� ���� �õ�
        if (promptText == null)
        {
            TextMeshProUGUI[] allTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
            foreach (var t in allTexts)
            {
                if (t.name == "PromptText")
                {
                    promptText = t;
                    break;
                }
            }

            if (promptText == null)
            {
                Debug.LogWarning("PromptText ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }

    void Update()
    {
        // ���� �ð� �������� ��ȣ�ۿ� ������ ������Ʈ ����
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // ī�޶� �߽ɿ��� ������ ���� ���
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // ������ �Ÿ��� ���̾� ����ũ �ȿ��� ����ĳ��Ʈ ����
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // ���ο� ������Ʈ�� ����� ��츸 ó��
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(); // ��ȣ�ۿ� ���� ����
                }
            }
            else
            {
                // ���� ����� ���� ��� �ʱ�ȭ
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    // ��ȣ�ۿ� ������Ʈ �ؽ�Ʈ ǥ��
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt(); // �������̽����� ������Ʈ ���� ��������
    }

    // ��ȣ�ۿ� Ű �Է� ó��
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        // Ű�� ������ ���� (Started)�̰� ����� ���� ���� ��ȣ�ۿ�
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();             // �������̽� ��ȣ�ۿ� ����
            curInteractGameObject = null;             // ��ȣ�ۿ� �� �ʱ�ȭ
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
