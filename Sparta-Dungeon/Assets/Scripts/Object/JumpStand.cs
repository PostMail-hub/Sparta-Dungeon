using UnityEngine;

public class JumpStand : MonoBehaviour
{
    public float power = 150.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.SuperJump(power);
            }
        }
    }
}