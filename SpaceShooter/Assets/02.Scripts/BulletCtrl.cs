using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;    //�Ѿ� �ı���
    public float force = 1500.0f;   //�Ѿ˹߻� ��
    private Rigidbody rb;   //������ٵ� ������Ʈ 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);  //�Ѿ��� ���� �������� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
