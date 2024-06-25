using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalEnemy : MonoBehaviour
{
    [SerializeField]Transform playerTransform;//Player�̃g�����X�t�H�[��
    [SerializeField] float distance = 1f; //�v���C���[�ɋ߂Â��鋗��
    [SerializeField] float attackCooltime = 1.0f;//�U���̃N�[���_�E��
    [SerializeField] float delay = 1f;
    [SerializeField] float speed = 1.5f; //�ړ����x
    [SerializeField] AudioClip zonbieSE; //�]���r�̍U����

    private const string MainCamera = "MainCamera";//���C���J�����̃^�O
    private bool isCameraIn = false;//�J�����ɓ����Ă��邩�̃t���O
    private float Stop = 0f;//�U�����~�܂�
    private float Go = 1.5f;//�U���I������瓮��
    private bool isAttack = false;//�U�������ǂ����Ǘ�����t���O
    private float lastAttackTime;//�Ō�ɍU����������
    private Vector3 lastPosition;
    private Animator anim;
    private AudioSource audioSource;�@//�I�[�f�B�I�\�[�X  
  

    
    
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //�I�[�f�B�I�\�[�X�擾
        lastAttackTime = -attackCooltime; //�ŏ��̍U���������o����
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").
            transform; //�v���C���[�̃g�����X�t�H�[���擾
    }

    private void Move()//�ړ�����
    {
        if (isCameraIn == true)//�J�����ɓ����Ă���ԏ�������
        {
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

        #region �ړ�����
        //�v���C���[�Ƃ̋�����.1f�����ɂȂ����炻��ȏ���s���Ȃ�
        if (Vector2.Distance(transform.position, playerTransform.position) < distance)
            return;

        //�v���C���[�Ɍ����Đi��
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTransform.position.x, playerTransform.position.y),
            speed * Time.deltaTime);

        if (isAttack) return;//�U�����͈ړ����Ȃ�
        #endregion
    }

    public void Attack(GameObject Player)//�U������
    {
        if (Time.time >= lastAttackTime + attackCooltime)
        {            
            #region �U���̏���
            audioSource.PlayOneShot(zonbieSE);�@//���ʉ�����񂾂��炷
            anim.SetBool("EnemyAttack", true);//�U���A�j���[�V�����𗬂�
            lastAttackTime = Time.time;
            Invoke("OnAttackEnd", delay);
            speed = Stop;//�U������Ƃ��~�܂�
            #endregion
        }
    }
    void OnAttackEnd()//�U���̏I��鏈��
    {
        anim.SetBool("EnemyAttack", false);//�U���A�j���[�V�����I���
        speed = Go;//�U���I������瓮��
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.tag == MainCamera)//���C���J�����ɓ����Ă���ԉ��L�̏�������
        {
            isCameraIn = true;//�J�����ɓ����Ă��邩�̃t���O
            Move();//�ړ�����
            // �v���C���[�L�����N�^�[�̈ʒu
            Vector3 playerPosition = playerTransform.position;
            // �G�L�����N�^�[�̈ʒu
            Vector3 enemyPosition = transform.position;
            // �v���C���[�̕������v�Z
            Vector3 direction = playerPosition - enemyPosition;
        }
    }

    public void objTr(GameObject obj)
    {
        playerTransform = obj.transform;
    }

}
