using UnityEngine;

public class GravityController : MonoBehaviour
{
    void Update()
    {
        if (GravityManager.Instance == null) return;

        // 矢印キー入力で全体重力を変更
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GravityManager.Instance.ChangeGravity(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GravityManager.Instance.ChangeGravity(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GravityManager.Instance.ChangeGravity(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GravityManager.Instance.ChangeGravity(Vector3.right);
        }
    }
}