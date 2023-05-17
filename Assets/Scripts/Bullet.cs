using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 1;

    void Update()
    {
        //�����蔻����������G����������Ă���񂾂��ǁA�d����
        var targets = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < targets.Length; i++)
        {
            //�G����`�A����(�e)����`�Ƃ��Ĕ���
            bool isHitX = Mathf.Abs(transform.position.x - targets[i].transform.position.x)
            <= transform.localScale.x / 2 + targets[i].transform.localScale.x / 2; //x���W���d�Ȃ��Ă��邩
            bool isHitY = Mathf.Abs(transform.position.y - targets[i].transform.position.y)
                <= transform.localScale.y / 2 + targets[i].transform.localScale.y / 2; //y���W���d�Ȃ��Ă��邩

            if(isHitX && isHitY)
            {
                targets[i].GetComponent<IDamageable>().Damage(1);
                Destroy(gameObject);
            }

        }

        this.transform.position += _speed * Time.deltaTime * transform.right;
    }

}
