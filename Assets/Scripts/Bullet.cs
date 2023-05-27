using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 1;

    //void Update()
    //{
    //    //�����蔻����������G����������Ă���񂾂��ǁA�d����
    //    var targets = GameObject.FindGameObjectsWithTag("Enemy");
    //    var circle = GameObject.FindGameObjectsWithTag("EnemyCircle");

    //    for (int i = 0; i < targets.Length; i++)
    //    {
    //        //�G����`�A����(�e)����`�Ƃ��Ĕ���
    //        bool isHitX = Mathf.Abs(transform.position.x - targets[i].transform.position.x)
    //        <= transform.localScale.x / 2 + targets[i].transform.localScale.x / 2; //x���W���d�Ȃ��Ă��邩
    //        bool isHitY = Mathf.Abs(transform.position.y - targets[i].transform.position.y)
    //            <= transform.localScale.y / 2 + targets[i].transform.localScale.y / 2; //y���W���d�Ȃ��Ă��邩

    //        if (isHitX && isHitY)
    //        {
    //            targets[i].GetComponent<IDamageable>().Damage(1);
    //            Destroy(gameObject);
    //        }

    //    }

    //    for (int i = 0; i < circle.Length; i++)
    //    {
    //        //�G���~�A��������`�Ƃ��Ĕ���
    //        Vector2 ul = transform.position + new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2);
    //        Vector2 ur = transform.position + new Vector3(transform.localScale.x / 2, transform.localScale.y / 2);
    //        Vector2 dl = transform.position + new Vector3(-transform.localScale.x / 2, -transform.localScale.y / 2);
    //        Vector2 dr = transform.position + new Vector3(transform.localScale.x / 2, -transform.localScale.y / 2);

    //        //�l���̂ǂ����������Ă�Γ������Ă�
    //        bool isHitting = Mathf.Pow(ul.x - circle[i].transform.position.x, 2) + Mathf.Pow(ul.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2)
    //            || Mathf.Pow(ur.x - circle[i].transform.position.x, 2) + Mathf.Pow(ur.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2)
    //            || Mathf.Pow(dl.x - circle[i].transform.position.x, 2) + Mathf.Pow(dl.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2)
    //            || Mathf.Pow(dr.x - circle[i].transform.position.x, 2) + Mathf.Pow(dr.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2);

    //        if(isHitting)
    //        {
    //            circle[i].GetComponent<IDamageable>().Damage(1);
    //            Destroy(gameObject);
    //        }

    //    }

    //    this.transform.position += _speed * Time.deltaTime * transform.right;
    //}

    private void LateUpdate()
    {
        //�����蔻����������G����������Ă���񂾂��ǁA�d����
        //���œK������Ȃ�Aunity�݂����Ɂu�����蔻�����������N���X�v�����
        var targets = GameObject.FindGameObjectsWithTag("Enemy");
        var circle = GameObject.FindGameObjectsWithTag("EnemyCircle");

        for (int i = 0; i < targets.Length; i++)
        {
            //�G����`�A����(�e)����`�Ƃ��Ĕ���
            bool isHitX = Mathf.Abs(transform.position.x - targets[i].transform.position.x)
            <= transform.localScale.x / 2 + targets[i].transform.localScale.x / 2; //x���W���d�Ȃ��Ă��邩
            bool isHitY = Mathf.Abs(transform.position.y - targets[i].transform.position.y)
                <= transform.localScale.y / 2 + targets[i].transform.localScale.y / 2; //y���W���d�Ȃ��Ă��邩

            if (isHitX && isHitY)
            {
                targets[i].GetComponent<IDamageable>().Damage(1);
                Destroy(gameObject);
            }

        }

        for (int i = 0; i < circle.Length; i++)
        {
            //�~�Ƌ�`�̔���͐F�X����炵��
            //�G���~�A��������`�Ƃ��Ĕ���
            Vector2 ul = transform.position + new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2);
            Vector2 ur = transform.position + new Vector3(transform.localScale.x / 2, transform.localScale.y / 2);
            Vector2 dl = transform.position + new Vector3(-transform.localScale.x / 2, -transform.localScale.y / 2);
            Vector2 dr = transform.position + new Vector3(transform.localScale.x / 2, -transform.localScale.y / 2);

            //�l���̂ǂ����������Ă�Γ������Ă�
            bool isHitting = Mathf.Pow(ul.x - circle[i].transform.position.x, 2) + Mathf.Pow(ul.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2)
                || Mathf.Pow(ur.x - circle[i].transform.position.x, 2) + Mathf.Pow(ur.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2)
                || Mathf.Pow(dl.x - circle[i].transform.position.x, 2) + Mathf.Pow(dl.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2)
                || Mathf.Pow(dr.x - circle[i].transform.position.x, 2) + Mathf.Pow(dr.y - circle[i].transform.position.y, 2) <= Mathf.Pow(circle[i].transform.localScale.x / 2, 2);

            if (isHitting)
            {
                circle[i].GetComponent<IDamageable>().Damage(1);
                Destroy(gameObject);
            }

        }

        this.transform.position += _speed * Time.deltaTime * transform.right;

        if (transform.position.x > 10) Destroy(gameObject);
    }

}
