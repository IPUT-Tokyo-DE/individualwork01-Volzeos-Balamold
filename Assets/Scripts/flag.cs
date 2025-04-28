using UnityEngine;
using System.Collections;

public class DuplicateSelfWithRandomArrayPosition : MonoBehaviour
{
    public string targetTag = "Player"; // �ΏۂƂȂ�^�O
    public float thresholdX = 1.0f; // X��������臒l
    public float thresholdY = 1.0f; // Y��������臒l

    // X����Y���̕����ʒu���
    public float[] duplicationXPositions = { -6, -3, 0, 3, 6 };
    public float[] duplicationYPositions = { -2, 1.5f };

    private bool hasDuplicated = false; // �������s��ꂽ���̃t���O
    private Vector3 lastPosition = Vector3.zero; // �Ō�ɐ����������W���L�^
    private const float positionTolerance = 0.01f; // �ʒu��r�̋��e�덷

    void Update()
    {
        // ���łɕ������s���Ă���Ώ������X�L�b�v
        if (hasDuplicated) return;

        // �^�[�Q�b�g�̈ʒu�𖈃t���[���擾���A�X�V
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject target in targets)
        {
            // �^�[�Q�b�g�ƌ��݂̃I�u�W�F�N�g�Ƃ̋������v�Z
            Vector3 distance = target.transform.position - transform.position;

            // X����Y���̋�����臒l�����ł���Ε����ƍ폜�̏������J�n
            if (Mathf.Abs(distance.x) < thresholdX && Mathf.Abs(distance.y) < thresholdY)
            {
                hasDuplicated = true; // �t���O�𗧂Ă�
                StartCoroutine(DuplicateAndDestroy());
                return; // �������I��
            }
        }
    }

    IEnumerator DuplicateAndDestroy()
    {
        Vector3 newPosition;

        // �����_����X��Y�̈ʒu��I���i���O�̍��W�ƈقȂ���̂�I�ԁj
        do
        {
            float randomX = duplicationXPositions[Random.Range(0, duplicationXPositions.Length)];
            float randomY = duplicationYPositions[Random.Range(0, duplicationYPositions.Length)];
            newPosition = new Vector3(randomX, randomY, transform.position.z);
        }
        while (IsApproximatelyEqual(newPosition, lastPosition)); // �O��̈ʒu�Ƌߎ����Ă���ꍇ�͍đI��

        lastPosition = newPosition; // �V�������W���L�^

        // ���g�𕡐�
        Instantiate(gameObject, newPosition, Quaternion.identity);

        // 0.5�b�̗P�\��^����
        yield return new WaitForSeconds(0.5f);

        // ���݂̃I�u�W�F�N�g���폜
        Destroy(gameObject);
    }

    // �ʒu�̔�r���s�����߂̃w���p�[���\�b�h
    private bool IsApproximatelyEqual(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) < positionTolerance &&
               Mathf.Abs(a.y - b.y) < positionTolerance &&
               Mathf.Abs(a.z - b.z) < positionTolerance;
    }
}