using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Hunmmer : MonoBehaviour
{
    public float moveDistance = 1.0f; // �ړ����鋗��
    public float moveInterval = 1.0f; // �ړ�����Ԋu�i�b�j

    public float rotateAngle = 45.0f; // ��]����p�x
    public float rotateInterval = 2.0f; // ��]����Ԋu�i�b�j

    public float duplicateInterval = 5.0f; // ��������Ԋu�i�b�j
    public float duplicateYPosition = 0.0f; // ��������ۂ�Y���W���蓮�Őݒ�

    public float deleteThresholdY = -10.0f; // ����Y���W�ɒB������I�u�W�F�N�g���폜����臒l

    private float timeSinceLastMove = 0.0f; // �Ō�Ɉړ�����������̌o�ߎ���
    private float timeSinceLastRotate = 0.0f; // �Ō�ɉ�]����������̌o�ߎ���
    private float timeSinceLastDuplicate = 0.0f; // �Ō�ɕ�������������̌o�ߎ���

    private bool hasDuplicated = false; // �������ꂽ���ǂ������`�F�b�N����t���O

    // X���W���̃��X�g
    private int[] xPositions = new int[] { -6, -3, 0, 3, 6 };

    void Update()
    {
        // �ړ��̎��Ԃ��X�V
        timeSinceLastMove += Time.deltaTime;
        // ��]�̎��Ԃ��X�V
        timeSinceLastRotate += Time.deltaTime;
        // �����̎��Ԃ��X�V
        timeSinceLastDuplicate += Time.deltaTime;

        // �ړ�����
        if (timeSinceLastMove >= moveInterval)
        {
            // �I�u�W�F�N�g�����Ɉړ�
            transform.position += Vector3.down * moveDistance;

            // �Ō�Ɉړ��������Ԃ����Z�b�g
            timeSinceLastMove = 0.0f;
        }

        // ��]�����iZ�����j
        if (timeSinceLastRotate >= rotateInterval)
        {
            // Z���𒆐S�ɉ�]
            transform.Rotate(Vector3.forward, rotateAngle);

            // �Ō�ɉ�]�������Ԃ����Z�b�g
            timeSinceLastRotate = 0.0f;
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

        // ����Y���W�ɒB������I�u�W�F�N�g���폜
        if (transform.position.y <= deleteThresholdY)
        {
            Destroy(gameObject);
        }
    }
}
