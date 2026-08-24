using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 6f; // 移動速度

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // WASD入力の取得（W/S: 前後, A/D: 左右）
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        // プレイヤーの現在の回転（ローカル軸）を基準にした移動方向の計算
        Vector3 moveDirection = (transform.right * inputX + transform.forward * inputZ).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            // 重力方向への速度（落下速度）を維持しつつ、接地面上の移動速度を適用
            Vector3 targetVelocity = moveDirection * moveSpeed;
            Vector3 currentMoveVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
            Vector3 velocityChange = targetVelocity - currentMoveVelocity;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            // キー入力がない場合は接地面上の慣性を抑えてピタッと止める
            Vector3 currentMoveVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
            rb.AddForce(-currentMoveVelocity * 0.2f, ForceMode.VelocityChange);
        }
    }
}