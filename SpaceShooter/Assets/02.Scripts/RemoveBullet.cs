using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {

         
                //��ƼŬ ����
                ContactPoint cp = coll.GetContact(0);  //��ƼŬ ������ ���� ��ġ ���� ���ϱ�
                Quaternion rot = Quaternion.LookRotation(-cp.normal);  //��ƼŬ ������ ���� ȸ�� ���� ���ϱ�
                                                                       //ȸ������ �Ǽ������� ���� ����
                GameObject spark = Instantiate(sparkEffect, cp.point, rot); //cp.point ��ġ�� ȸ�������� sparkEffect ����

            
                //�Ѿ� ����
                Destroy(coll.gameObject);
            
            //��ƼŬ ����
            Destroy(spark, 1.0f);  //�Ѿ� ���� �����ϰ� ��ƼŬ ����
        }
    }
}
