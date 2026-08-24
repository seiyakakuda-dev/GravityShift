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

        // 重力切替時にカメラシェイクを実行
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(0.2f, 0.35f);
        }

        // ★ 重力切替時に色収差演出を実行（強度: 1.0, 時間: 0.25秒）
        if (ChromaticAberrationController.Instance != null)
        {
            ChromaticAberrationController.Instance.TriggerEffect(1.0f, 0.25f);
        }
    }

    public Vector3 CurrentGravityDirection => currentGravityDirection;
}