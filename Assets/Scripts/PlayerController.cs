using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private bool isActive = true; // ����\���ǂ������Ǘ�����t���O
    public GameObject GameOverText; // �Q�[���I�[�o�[�e�L�X�g�̎Q��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        // ���삪�����ȏꍇ�AUpdate�������X�L�b�v
        if (!isActive) return;

        // �ǋL: �͈͐���
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -6, 6),
            Mathf.Clamp(transform.position.y, -2, 1.5f)
        );

        // �ړ��X�N���v�g
        if (Input.GetKeyDown(KeyCode.LeftArrow))   // ���ړ�
        {
            this.transform.Translate(new Vector2(-3f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))  // �E�ړ�
        {
            this.transform.Translate(new Vector2(3f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))     // ��ړ�
        {
            this.transform.Translate(new Vector2(0f, 3.5f));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))   // ���ړ�
        {
            this.transform.Translate(new Vector2(0f, -3.5f));
        }

        // �����`�F�b�N
        CheckDistanceToObjects();
    }

    void CheckDistanceToObjects()
    {
        // Fire�^�O��Hunmmer�^�O�̃I�u�W�F�N�g�����ׂĎ擾
        GameObject[] fireObjects = GameObject.FindGameObjectsWithTag("Fire");
        GameObject[] hunmmerObjects = GameObject.FindGameObjectsWithTag("Hunmmer");

        // �������`�F�b�N
        foreach (GameObject obj in fireObjects)
        {
            if (IsWithinDistance(obj))
            {
                DisableAndDestroy();
                return;
            }
        }

        foreach (GameObject obj in hunmmerObjects)
        {
            if (IsWithinDistance(obj))
            {
                DisableAndDestroy();
                return;
            }
        }
    }

    bool IsWithinDistance(GameObject target)
    {
        // �����ƑΏۃI�u�W�F�N�g��X���W��Y���W�̋������v�Z
        float distanceX = Mathf.Abs(transform.position.x - target.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - target.transform.position.y);

        // �ǂ���̋�����1�����ł����true��Ԃ�
        return distanceX < 1f && distanceY < 1f;
    }

    void DisableAndDestroy()
    {
        isActive = false; // ����𖳌���
        Destroy(gameObject); // ���݂̃I�u�W�F�N�g���폜
        GameOverText.SetActive(true); // �Q�[���I�[�o�[�e�L�X�g��\��
    }
}