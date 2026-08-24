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
    }

    public Vector3 CurrentGravityDirection => currentGravityDirection;
}