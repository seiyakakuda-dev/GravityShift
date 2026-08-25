using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("追従対象")]
    [SerializeField] private Transform target;

    [Header("カメラオフセット")]
    [SerializeField] private float distance = 8f;   // プレイヤーからの距離
    [SerializeField] private float height = 4f;     // プレイヤーからの高さ
    [SerializeField] private float smoothSpeed = 5f; // 重力変化時のカメラ回転スピード

    private float currentYaw = 0f;

    private void Update()
    {
        if (target == null) return;

        // Q / E キーでカメラを左右90度回転（死角対策）
        if (Input.GetKeyDown(KeyCode.Q)) currentYaw -= 90f;
        if (Input.GetKeyDown(KeyCode.E)) currentYaw += 90f;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Unity標準の重力ベクトル（Physics.gravity）から「上方向」を動的に取得
        Vector3 gravityUp = -Physics.gravity.normalized;

        // 重力「上」を基準とした回転姿勢を作成
        Quaternion gravityRotation = Quaternion.FromToRotation(Vector3.up, gravityUp);
        Quaternion yawRotation = Quaternion.Euler(0f, currentYaw, 0f);

        // カメラの目標位置を計算
        Vector3 localOffset = new Vector3(0, height, -distance);
        Vector3 targetPosition = target.position + (gravityRotation * yawRotation * localOffset);

        // カメラの目標回転（プレイヤーを見下ろす視点）
        Vector3 lookAtTarget = target.position + (gravityUp * 1.5f);
        Quaternion targetRotation = Quaternion.LookRotation(lookAtTarget - targetPosition, gravityUp);

        // 滑らかに位置と回転を移動
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}