using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    Transform playerTr;

    [SerializeField] float distance = 1f; //�v���C���[�ɋ߂Â��鋗��
    [SerializeField] float speed = 3f; //�ړ����x
    [SerializeField] float attackCooltime = 1.0f;//�U���̃N�[���_�E��
    [SerializeField] float delay = 1f;

    private const string MainCamera = "MainCamera";//���C���J�����̃^�O
    private bool CameraIn = false;//�J�����ɓ����Ă��邩�̃t���O
    private float Stop = 0f;//�U�����~�܂�
    private float Go = 3f;//�U���I������瓮��
    private bool isAttack = false;//�U�������ǂ����Ǘ�����t���O
    private float lastAttackTime;//�Ō�ɍU����������
    private Vector3 lastPos;
    private Animator anim;

    void Start()
    {
        lastAttackTime = -attackCooltime; //�ŏ��̍U���������o����
        anim = GetComponent<Animator>();
        lastPos = transform.position;
        playerTr = GameObject.FindGameObjectWithTag("Player").
            transform; //�v���C���[�̃g�����X�t�H�[���擾
    }


    private void move()//�ړ�����
    {

        if (CameraIn == true)
        {
            #region�@���]����
            Vector3 currentPosition = transform.position;
            Vector3 _direction = currentPosition - lastPos;

            if (_direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            lastPos = currentPosition;

            #endregion
        }


        #region �ړ�����
        //�v���C���[�Ƃ̋�����.1f�����ɂȂ����炻��ȏ���s���Ȃ�
        if (Vector2.Distance(transform.position, playerTr.position) < distance)
            return;

        //�v���C���[�Ɍ����Đi��
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTr.position.x, playerTr.position.y),
            speed * Time.deltaTime);

        if (isAttack) return;//�U�����͈ړ����Ȃ�
        #endregion
    }

    public void Attack(GameObject Player)//�U������
    {
        if (Time.time >= lastAttackTime + attackCooltime)
        {
            #region �U���̏���
            anim.SetBool("Attack", true);//�U���A�j���[�V�����𗬂�
            lastAttackTime = Time.time;
            Invoke("DelayMethod", delay);
            speed = Stop;//�U������Ƃ��~�܂�
            #endregion
        }
    }
    void DelayMethod()//�U���̏I��鏈��
    {
        anim.SetBool("Attack", false);//�U���A�j���[�V�����I���
        speed = Go;//�U���I������瓮��
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.tag == MainCamera)//���C���J�����ɓ����Ă���ԉ��L�̏�������
        {
            CameraIn = true;//�J�����ɓ����Ă��邩�̃t���O
            move();//�ړ�����
        }
    }
}
