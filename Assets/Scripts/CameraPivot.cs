using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    private float targetYaw = 0f;

    private void Update()
    {
        // Q / E キーで Pivot（軸）をローカルY軸で90度回すだけ
        if (Input.GetKeyDown(KeyCode.Q)) targetYaw -= 90f;
        if (Input.GetKeyDown(KeyCode.E)) targetYaw += 90f;

        Quaternion targetRotation = Quaternion.Euler(0f, targetYaw, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}