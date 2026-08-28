/*using UnityEngine;

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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 gravityUp = -Physics.gravity.normalized;
        Transform mainCam = Camera.main.transform;

        // カメラの正面方向と右方向を、現在の重力平面に投影して取得
        Vector3 camForward = Vector3.ProjectOnPlane(mainCam.forward, gravityUp).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(mainCam.right, gravityUp).normalized;

        // カメラの向き基準で移動方向を決定（これで前キー＝カメラの奥に進む）
        Vector3 moveDir = (camForward * moveZ + camRight * moveX).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float alignSpeed = 15f; // 重力方向に足を向ける回転速度

    private Rigidbody rb;
    private Vector2 inputVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // 物理演算による勝手な回転を固定
        rb.freezeRotation = true;
    }

    private void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        // 1. GravityManager から正確な重力方向を取得
        Vector3 gravityDir = Vector3.down;
        if (GravityManager.Instance != null)
        {
            gravityDir = GravityManager.Instance.CurrentGravityDirection;
        }
        else
        {
            gravityDir = Physics.gravity.normalized;
        }

        Vector3 gravityUp = -gravityDir;

        // 2. 体の足を重力方向（床）へ向ける回転計算
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * alignSpeed));

        // 3. カメラの向きを基準にした移動処理
        Transform mainCam = Camera.main.transform;
        Vector3 camForward = Vector3.ProjectOnPlane(mainCam.forward, gravityUp).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(mainCam.right, gravityUp).normalized;
        Vector3 moveDir = (camForward * inputVector.y + camRight * inputVector.x).normalized;

        // 4. 重力方向の落下速度を保持したまま移動速度を付与
        Vector3 currentGravityVelocity = Vector3.Project(rb.linearVelocity, gravityUp);
        Vector3 targetMoveVelocity = moveDir * moveSpeed;

        rb.linearVelocity = targetMoveVelocity + currentGravityVelocity;
    }
}