using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]Transform playerTransform;
    [SerializeField] GameObject _enemy; //敵のプレハブ
    [SerializeField] float _SpawnRate = 1f; //スポーンレート
    [SerializeField] bool _isPlayerInRange = false; //プレイヤーが射程内に入ったかどうか
    [SerializeField] AudioClip shotSE;//弾出す音

    private float _nextFireTime = 0f; //次の敵が出るまでの時間
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //プレイヤーのトランスフォーム取得
    }

    void Update()
    {
        if (_isPlayerInRange && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + 1f / _SpawnRate;
        }
    }

    void Shoot()
    {
        if (playerTransform != null)
        {
            audioSource.PlayOneShot(shotSE);
            //弾を生成
            GameObject instance = (GameObject)Instantiate(_enemy, transform.position, Quaternion.identity);

            ////プレイヤーの方向を計算
            //Vector3 direction = (_player.position - transform.position).normalized;

            ////球に力を加える
            //bullet.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
        }
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }


}
