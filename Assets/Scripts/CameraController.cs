using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("追従対象")]
    [SerializeField] private Transform target; // Playerをアタッチ

    [Header("カメラ位置調整")]
    [SerializeField] private float distance = 8f;     // プレイヤーからの距離
    [SerializeField] private float height = 3f;       // プレイヤーの頭上高さ
    [SerializeField] private float smoothSpeed = 12f; // 追従・回転の滑らかさ

    private float targetYaw = 0f; // Q/Eキーでの回転角度

    private void Update()
    {
        if (target == null) return;

        // Q / E キーで視点を左右90度旋回
        if (Input.GetKeyDown(KeyCode.Q)) targetYaw -= 90f;
        if (Input.GetKeyDown(KeyCode.E)) targetYaw += 90f;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. 現在の重力に応じた「頭上（Up）」方向を取得
        Vector3 gravityUp = -Physics.gravity.normalized;

        // 2. 重力「上」を基準とした回転と、Q/Eによる旋回（gravityUpを軸に回転）を作成
        Quaternion gravityRot = Quaternion.FromToRotation(Vector3.up, gravityUp);
        Quaternion yawRot = Quaternion.AngleAxis(targetYaw, gravityUp);

        // 3. プレイヤーの向きではなく「重力軸」基準でカメラの目標位置を計算
        Vector3 baseOffset = new Vector3(0f, height, -distance);
        Vector3 targetPosition = target.position + (yawRot * gravityRot * baseOffset);

        // 4. 注視点（プレイヤーの少し頭上）
        Vector3 lookTarget = target.position + (gravityUp * 1.2f);

        // 5. カメラの上方向を gravityUp に固定して回転を作成
        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - targetPosition, gravityUp);

        // 6. 滑らかに位置と回転を更新
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}