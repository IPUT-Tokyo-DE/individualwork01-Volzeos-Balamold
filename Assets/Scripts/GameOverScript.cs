using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.SetActive(false); // ゲームオーバーテキストを非表示にする
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
