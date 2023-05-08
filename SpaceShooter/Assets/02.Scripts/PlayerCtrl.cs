using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f; //����
    private float v = 0.0f;  //������
    //���� ������ ���� ����ϱ� ���� h, v�� ����
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    //������Ʈ�� ĳ��ó���� ����
    private Transform tr;
    // Animation ������Ʈ�� ������ ����  
    private Animation anim;

    void Start()
    {
        tr = GetComponent<Transform>();  //��ü �Ҵ�Ǿ Ʈ�������̶�� ������Ʈ�� ����� �� ����
        anim = GetComponent<Animation>();  //�÷��̾ �߰��� animation ������Ʈ�� �������� �۾�
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");   //-0.1 ~ 0.1����

        //Debug.Log("H= " + h.ToString());
        //Debug.Log("V= " + v.ToString());

        //tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime);  -->���� ������ ��


        //������ ������ֱ� 
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //���� ���� * v + ���� ���� * h -> ������ ����
        //���(ȸ�������� ���� ����)->

        //Translate(�̵����� * �ӷ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f) //up
        {
            anim.CrossFade("RunF", 0.25f);  //25%�� ���� ���� �ϴ� ��
        }
        else if(v<=-0.1f) //down
        {
            anim.CrossFade("RunB", 0.25f);  //�����ִϸ��̼�
        }
        else if(h>=0.1f)  //Right
        {
            anim.CrossFade("RunR", 0.25f);  //������ �̵� �ִϸ��̼�
        }
        else if(h<=-0.1f) //left
        {
            anim.CrossFade("RunL", 0.25f);   //���� �̵� �ִϸ��̼�
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);   //������ Idle �ִϸ��̼� ����
        }
    }
}
