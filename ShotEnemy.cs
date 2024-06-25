using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    Transform playerTransform;//Player�̃g�����X�t�H�[��

    [SerializeField] float distance = 1f; //�v���C���[�ɋ߂Â��鋗��
    [SerializeField] float speed = .0001f; //�ړ����x

    private Vector3 lastPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").
            transform; //�v���C���[�̃g�����X�t�H�[���擾
        lastPosition = transform.position;
    }

    void Update()
    {
        //�v���C���[�Ƃ̋�����.1f�����ɂȂ����炻��ȏ���s���Ȃ�
        if (Vector2.Distance(transform.position, playerTransform.position) < distance)
            return;

        //�v���C���[�Ɍ����Đi��
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTransform.position.x, playerTransform.position.y),
            speed * Time.deltaTime);

        #region�@���]����
        // �v���C���[�L�����N�^�[�̈ʒu
        Vector3 playerPosition = playerTransform.transform.position;
        // �G�L�����N�^�[�̈ʒu
        Vector3 enemyPosition = transform.position;

        // �v���C���[�̕������v�Z
        Vector3 direction = playerPosition - enemyPosition;
        // �����̔��f
        if (direction.x > 0)
        {
            // �v���C���[���E�ɂ���ꍇ�A�G�L�����N�^�[���E�����ɂ���
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            // �v���C���[�����ɂ���ꍇ�A�G�L�����N�^�[���������ɂ���
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        #endregion
    }

    public void objTr(GameObject obj)
    {
        playerTransform = obj.transform;
    }
}
