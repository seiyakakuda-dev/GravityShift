using System;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance { get; private set; }

    // 重力が変わった時に全オブジェクトに一斉通知するイベント
    public event Action<Vector3> OnGravityChanged;

    [SerializeField] private Vector3 currentGravityDirection = Vector3.down;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeGravity(Vector3 newDirection)
    {
        currentGravityDirection = newDirection.normalized;
        OnGravityChanged?.Invoke(currentGravityDirection);

        // ★ 重力切替時にカメラシェイクを実行（時間: 0.2秒, 強さ: 0.35）
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(0.2f, 0.35f);
        }
    }

    public Vector3 CurrentGravityDirection => currentGravityDirection;
}