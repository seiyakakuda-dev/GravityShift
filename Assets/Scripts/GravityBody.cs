using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [Header("重力設定")]
    [SerializeField] private Vector3 gravityDirection = Vector3.down;
    [SerializeField] private float gravityMagnitude = 9.81f;
    [SerializeField] private float rotationSpeed = 10f; // 重力方向に追従する回転速度

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 gravityDir = gravityDirection.normalized;

        // 1. 独自重力の加算
        Vector3 gravityForce = gravityDir * gravityMagnitude;
        rb.AddForce(gravityForce, ForceMode.Acceleration);

        // 2. プレイヤーの足元（-transform.up）を重力方向へ向ける回転処理
        Quaternion targetRotation = Quaternion.FromToRotation(-transform.up, gravityDir) * transform.rotation;
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    public void SetGravityDirection(Vector3 newDirection)
    {
        gravityDirection = newDirection.normalized;
    }

    public Vector3 GetGravityDirection()
    {
        return gravityDirection;
    }
}