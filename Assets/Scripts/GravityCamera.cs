using UnityEngine;

public class GravityCamera : MonoBehaviour
{
    [Header("ターゲット設定")]
    [SerializeField] private Transform target; // プレイヤーのTransform

    [Header("カメラオフセット")]
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, -4f); // プレイヤーから見たカメラの位置

    [Header("追従スピード")]
    [SerializeField] private float moveSpeed = 8f;     // 位置の滑らかさ
    [SerializeField] private float rotationSpeed = 6f; // 回転の滑らかさ

    void LateUpdate()
    {
        if (target == null) return;

        // プレイヤーの姿勢（回転）に合わせたカメラの目標位置を計算
        Vector3 targetPosition = target.position + target.rotation * offset;

        // 位置を滑らかに移動
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // プレイヤーの少し上（頭付近）を注視する視線ベクトルを計算
        Vector3 lookTarget = target.position + target.up * 1.0f;
        Vector3 lookDirection = lookTarget - transform.position;

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            // プレイヤーの足元・頭上軸（target.up）をカメラの上方向として維持
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
