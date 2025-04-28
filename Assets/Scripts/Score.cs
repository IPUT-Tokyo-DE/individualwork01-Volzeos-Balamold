using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;
    public int score = 0; // �X�R�A���Ǘ�����ϐ�

    void Start()
    {
        // �q�I�u�W�F�N�g�ɂ���Text�R���|�[�l���g���擾
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "0"; // �����X�R�A��\��
    }

    void Update()
    {
        // �X�R�A�����A���^�C���ɍX�V
        scoreText.text = score.ToString();

        // Flag�^�O�����I�u�W�F�N�g��Player�^�O�����I�u�W�F�N�g�̋������`�F�b�N
        CheckDistanceToFlag();
    }

    void CheckDistanceToFlag()
    {
        // Player�^�O�����I�u�W�F�N�g���擾
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return; // Player�����݂��Ȃ��ꍇ�͏����𒆒f

        // Flag�^�O�������ׂẴI�u�W�F�N�g���擾
        GameObject[] flagObjects = GameObject.FindGameObjectsWithTag("Flag");

        foreach (GameObject flag in flagObjects)
        {
            // �������v�Z
            float distanceX = Mathf.Abs(player.transform.position.x - flag.transform.position.x);
            float distanceY = Mathf.Abs(player.transform.position.y - flag.transform.position.y);

            // X���W��Y���W�̋��������ꂼ��1.1�ȉ��ł���ꍇ
            if (distanceX <= 1.1f && distanceY <= 1.1f)
            {
                // �X�R�A�𑝉������A�t���O�I�u�W�F�N�g��1�񂾂��J�E���g���邽�߂ɍ폜
                score += 1;
                Destroy(flag);
            }
        }
    }
}