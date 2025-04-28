using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;
    public int score = 0; // スコアを管理する変数

    void Start()
    {
        // 子オブジェクトにあるTextコンポーネントを取得
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "0"; // 初期スコアを表示
    }

    void Update()
    {
        // スコアをリアルタイムに更新
        scoreText.text = score.ToString();

        // Flagタグを持つオブジェクトとPlayerタグを持つオブジェクトの距離をチェック
        CheckDistanceToFlag();
    }

    void CheckDistanceToFlag()
    {
        // Playerタグを持つオブジェクトを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return; // Playerが存在しない場合は処理を中断

        // Flagタグを持つすべてのオブジェクトを取得
        GameObject[] flagObjects = GameObject.FindGameObjectsWithTag("Flag");

        foreach (GameObject flag in flagObjects)
        {
            // 距離を計算
            float distanceX = Mathf.Abs(player.transform.position.x - flag.transform.position.x);
            float distanceY = Mathf.Abs(player.transform.position.y - flag.transform.position.y);

            // X座標とY座標の距離がそれぞれ1.1以下である場合
            if (distanceX <= 1.1f && distanceY <= 1.1f)
            {
                // スコアを増加させ、フラグオブジェクトを1回だけカウントするために削除
                score += 1;
                Destroy(flag);
            }
        }
    }
}