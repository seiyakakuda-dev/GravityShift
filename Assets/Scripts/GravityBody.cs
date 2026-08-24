using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [Header("重力設定")]
    [SerializeField] private Vector3 gravityDirection = Vector3.down;
    [SerializeField] private float gravityMagnitude = 9.81f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool rotateToGravity = true;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Startのタイミングで確実な登録と初期重力の同期を行う
        RegisterGravityEvent();
    }

    void OnEnable()
    {
        RegisterGravityEvent();
    }

    void OnDisable()
    {
        if (GravityManager.Instance != null)
        {
            GravityManager.Instance.OnGravityChanged -= SetGravityDirection;
        }
    }

    private void RegisterGravityEvent()
    {
        if (GravityManager.Instance != null)
        {
            // 二重登録防止のために一度解除してから登録
            GravityManager.Instance.OnGravityChanged -= SetGravityDirection;
            GravityManager.Instance.OnGravityChanged += SetGravityDirection;

            // 現在の重力方向を反映
            gravityDirection = GravityManager.Instance.CurrentGravityDirection;
        }
    }

    void FixedUpdate()
    {
        Vector3 gravityDir = gravityDirection.normalized;

        Vector3 gravityForce = gravityDir * gravityMagnitude;
        rb.AddForce(gravityForce, ForceMode.Acceleration);

        if (rotateToGravity)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(-transform.up, gravityDir) * transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    public void SetGravityDirection(Vector3 newDirection)
    {
        gravityDirection = newDirection.normalized;
    }
}