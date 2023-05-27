using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>�u�G�̒e�v�Ɓu�v���C���[�̒e�v�̏������܂Ƃ߂�</summary>
public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform _player = default;
    [SerializeField] private List<Transform> _enemies = new List<Transform>(5);
    [SerializeField] private List<GameObject> _playerBullets = new List<GameObject>();
    [SerializeField] private float _playerBulletSpeed = 4f;
    [SerializeField] private List<GameObject> _enemyBullets = new List<GameObject>();
    [SerializeField] private float _enemyBulletSpeed = 4f;
      
    void Update()
    {
        
        //�v���C���[�̒e
        foreach(var pb in _playerBullets)
        {

            //�������Ȃ������烊�X�g����폜
            if (!pb)
            {
                _playerBullets.Remove(pb);
                return;
            }

            //�E�Ɉړ�
            pb.transform.position += _playerBulletSpeed * Time.deltaTime * pb.transform.right;

            //�G�Ƃ̓����蔻��
            foreach(var e in _enemies)
            {
                if (!e)
                {
                    _enemies.Remove(e);
                    return;
                }

                //�G����`�A����(�e)����`�Ƃ��Ĕ���
                bool isHitX = Mathf.Abs(pb.transform.position.x - e.position.x) <= pb.transform.localScale.x / 2 + e.localScale.x / 2; //x���W���d�Ȃ��Ă��邩
                bool isHitY = Mathf.Abs(pb.transform.position.y - e.position.y) <= pb.transform.localScale.y / 2 + e.localScale.y / 2; //y���W���d�Ȃ��Ă��邩

                if (isHitX && isHitY) //x���W��y���W���d�Ȃ��Ă���_���[�W��^���Ď��M��j��
                {
                    e.GetComponent<IDamageable>().Damage(1);
                    Destroy(pb);
                }

            }

        }

        //�G�̒e
        foreach (var eb in _enemyBullets)
        {

            //�������Ȃ������烊�X�g����폜
            if (!eb)
            {
                _enemyBullets.Remove(eb);
                return;
            }

            //���Ɉړ�
            eb.transform.position += _enemyBulletSpeed * Time.deltaTime * -eb.transform.right;

            //�v���C���[����`�A����(�e)��_�Ƃ��Ĕ���
            bool isHitX = Mathf.Abs(eb.transform.position.x - _player.position.x) <= _player.localScale.x / 2; //x���W���d�Ȃ��Ă��邩
            bool isHitY = Mathf.Abs(eb.transform.position.y - _player.position.y) <= _player.localScale.y / 2; //y���W���d�Ȃ��Ă��邩

            if (isHitX && isHitY) //x���W��y���W�ǂ�����d�Ȃ��Ă���_���[�W��^���Ď��M��j��
            {
                _player.gameObject.GetComponent<IDamageable>().Damage(1);
                Destroy(eb);
            }

        }

    }

    public void AddPlayerBullet(GameObject bullet)
    {
        _playerBullets.Add(bullet);
    }

    public void AddEnemyBullet(GameObject bullet)
    {
        _enemyBullets.Add(bullet);
    }

    public void AddEnemy(Transform enemy)
    {
        _enemies.Add(enemy);
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

}
