using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("連動させる扉")]
    [SerializeField] private GameObject door;

    [Header("扉が開く移動オフセット")]
    [SerializeField] private Vector3 doorOpenOffset = new Vector3(0, 4f, 0);
    [SerializeField] private float openSpeed = 3f;

    private bool isPressed = false;
    private Vector3 doorClosedPos;
    private Vector3 doorTargetPos;

    private void Start()
    {
        if (door != null)
        {
            doorClosedPos = door.transform.position;
            doorTargetPos = doorClosedPos + doorOpenOffset;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // プレイヤーまたは箱が乗っている間は起動
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 離れたら解除
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            isPressed = false;
        }
    }

    private void Update()
    {
        if (door == null) return;

        // スイッチの押下状態に合わせて扉を移動
        Vector3 targetPos = isPressed ? doorTargetPos : doorClosedPos;
        door.transform.position = Vector3.Lerp(door.transform.position, targetPos, Time.deltaTime * openSpeed);
    }
}