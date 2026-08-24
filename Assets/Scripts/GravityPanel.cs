using UnityEngine;

public class GravityPanel : MonoBehaviour
{
    [Header("切り替え先の重力方向")]
    [SerializeField] private Vector3 targetGravityDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        // 接触検知のログ出力
        Debug.Log($"パネルに接触: {other.name} (Tag: {other.tag})");

        if (other.CompareTag("Player"))
        {
            if (GravityManager.Instance != null)
            {
                GravityManager.Instance.ChangeGravity(targetGravityDirection);
            }
        }
    }
}