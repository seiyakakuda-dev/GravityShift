using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalArea : MonoBehaviour
{
    private bool isCleared = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCleared) return;

        if (other.CompareTag("Player"))
        {
            isCleared = true;
            Debug.Log("<color=yellow>★ STAGE CLEAR! ★ (Rキーでリトライ)</color>");

            // クリア演出として色収差を一瞬強く発動
            if (ChromaticAberrationController.Instance != null)
            {
                ChromaticAberrationController.Instance.TriggerEffect(1.0f, 0.5f);
            }
        }
    }

    private void Update()
    {
        // クリア後に R キーを押すとシーンを再読み込み（リトライ）
        if (isCleared && Input.GetKeyDown(KeyCode.R))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }

    private void OnGUI()
    {
        // クリア時に画面中央に文字を表示
        if (isCleared)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 48;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.yellow;

            float width = 600;
            float height = 120;
            float x = (Screen.width - width) / 2;
            float y = (Screen.height - height) / 2;

            GUI.Label(new Rect(x, y, width, height), "STAGE CLEAR!\n<size=24>[R] Key to Restart</size>", style);
        }
    }
}