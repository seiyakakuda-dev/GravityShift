using UnityEngine;

[RequireComponent(typeof(GravityBody))]
public class GravityController : MonoBehaviour
{
    private GravityBody gravityBody;

    void Start()
    {
        gravityBody = GetComponent<GravityBody>();
    }

    void Update()
    {
        // 矢印キー入力で重力方向を変更
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gravityBody.SetGravityDirection(Vector3.down); // 下（標準）
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gravityBody.SetGravityDirection(Vector3.up); // 上（天井）
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gravityBody.SetGravityDirection(Vector3.left); // 左の壁
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gravityBody.SetGravityDirection(Vector3.right); // 右の壁
        }
    }
}