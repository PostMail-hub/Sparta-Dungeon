using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어가 주변 오브젝트와 상호작용할 수 있도록 하는 컴포넌트
public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;               // 레이캐스트 체크 주기 (초 단위)
    private float lastCheckTime;                  // 마지막 체크 시간 기록
    public float maxCheckDistance;                // 상호작용 가능한 최대 거리
    public LayerMask layerMask;                   // 상호작용 감지 대상 레이어 마스크

    public GameObject curInteractGameObject;      // 현재 바라보고 있는 상호작용 대상
    private IInteractable curInteractable;        // 현재 바라보고 있는 상호작용 가능한 인터페이스

    public TextMeshProUGUI promptText;            // 상호작용 프롬프트 텍스트 UI
    private Camera camera;                        // 메인 카메라 참조

    void Start()
    {
        camera = Camera.main;                     // 메인 카메라 참조

        // 프롬프트 텍스트가 연결되지 않았을 경우 자동으로 찾아 연결 시도
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
                Debug.LogWarning("PromptText 오브젝트를 찾을 수 없습니다.");
            }
        }
    }

    void Update()
    {
        // 일정 시간 간격으로 상호작용 가능한 오브젝트 감지
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // 카메라 중심에서 앞으로 레이 쏘기
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // 설정한 거리와 레이어 마스크 안에서 레이캐스트 감지
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // 새로운 오브젝트에 닿았을 경우만 처리
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(); // 상호작용 문구 갱신
                }
            }
            else
            {
                // 감지 대상이 없을 경우 초기화
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    // 상호작용 프롬프트 텍스트 표시
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt(); // 인터페이스에서 프롬프트 정보 가져오기
    }

    // 상호작용 키 입력 처리
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        // 키를 누르는 순간 (Started)이고 대상이 있을 때만 상호작용
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();             // 인터페이스 상호작용 실행
            curInteractGameObject = null;             // 상호작용 후 초기화
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
