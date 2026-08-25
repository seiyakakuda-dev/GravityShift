/*
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
*/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 入力の取得（左右・前後移動）
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // 重力方向を基準にした移動ベクトルの計算
        Vector3 gravityUp = -Physics.gravity.normalized;
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 簡易的な移動処理
        Vector3 moveDir = new Vector3(moveX, 0f, moveZ).normalized;
        if (moveDir.magnitude > 0.1f)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}