using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float moveDistance = 1.0f; // �ړ����鋗��
    public float moveInterval = 1.0f; // �ړ�����Ԋu�i�b�j

    public float duplicateInterval = 5.0f; // ��������Ԋu�i�b�j
    public float duplicateYPosition = 0.0f; // ��������ۂ�Y���W���蓮�Őݒ�

    public float deleteThresholdY = -10.0f; // ����Y���W�ɒB������I�u�W�F�N�g���폜����臒l
    public float waitTimeBeforeDestroy = 2.0f; // �����W�ɒB���Ă���ҋ@���鎞�ԁi�b�j

    private float timeSinceLastMove = 0.0f; // �Ō�Ɉړ�����������̌o�ߎ���
    private float timeSinceLastDuplicate = 0.0f; // �Ō�ɕ�������������̌o�ߎ���
    private float timeWaiting = 0.0f; // �ҋ@���Ԃ̌o�ߎ���

    private bool hasDuplicated = false; // �������ꂽ���ǂ������`�F�b�N����t���O
    private bool isWaiting = false; // �ҋ@�����ǂ����̃t���O

    // X���W���̃��X�g
    private int[] xPositions = new int[] { -6, -3, 0, 3, 6 };

    void Update()
    {
        // �ړ��̎��Ԃ��X�V
        timeSinceLastMove += Time.deltaTime;
        // �����̎��Ԃ��X�V
        timeSinceLastDuplicate += Time.deltaTime;

        // �ҋ@���Ԃ��X�V
        if (isWaiting)
        {
            timeWaiting += Time.deltaTime;

            // �ҋ@���Ԃ��o�߂�����I�u�W�F�N�g���폜
            if (timeWaiting >= waitTimeBeforeDestroy)
            {
                Destroy(gameObject);
            }

            return; // �ҋ@���͑��̏������X�L�b�v
        }

        // �ړ�����
        if (timeSinceLastMove >= moveInterval)
        {
            // �I�u�W�F�N�g�����Ɉړ�
            transform.position += Vector3.down * moveDistance;

            // �Ō�Ɉړ��������Ԃ����Z�b�g
            timeSinceLastMove = 0.0f;
        }

        // ��������
        if (timeSinceLastDuplicate >= duplicateInterval && !hasDuplicated)
        {
            // ������X���W�������_���ɑI��
            int randomX = xPositions[Random.Range(0, xPositions.Length)];

            // �������s��
            Instantiate(gameObject, new Vector3(randomX, duplicateYPosition, transform.position.z), Quaternion.identity);

            // �������ꂽ���Ƃ��L�^�i���ȑ��B��h���j
            hasDuplicated = true;

            // �Ō�ɕ����������Ԃ����Z�b�g
            timeSinceLastDuplicate = 0.0f;
        }

        // ����Y���W�ɒB������I�u�W�F�N�g��ҋ@��Ԃɂ���
        if (transform.position.y <= deleteThresholdY && !isWaiting)
        {
            isWaiting = true; // �ҋ@�J�n
        }
    }
}
