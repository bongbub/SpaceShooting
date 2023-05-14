using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform targetTr;  //���󰡾� �� ��� 
    private Transform camTr; //ī�޶��� ��ġ ������Ʈ

    //ī�޶��� ��ġ ���� ����
    [Range(2.0f, 20.0f)]  //���� ������ �ް� �Ǿ�����
    public float distance = 10.0f; //���� ���� ���ǿ� ���� ������ �ص� ������ �ɷ�����

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    //damping�� ���������
    private Vector3 velocity = Vector3.zero;
    public float damping = 10.0f;
    public float targetOffset = 2.0f;

    void Start()
    {
        camTr = GetComponent<Transform>();  
        //private�� �Ҵ������ ������  ���⼭ �Ҵ�

    }

    void Update()
    {
        
    }
    
  
    void LateUpdate()
    {

        #region ���1
        //ī�޶��� ��ġ ����
        camTr.position = targetTr.position
            + (-targetTr.forward * distance)
            + (Vector3.up * height); 
        //ī�޶� ���� ���̴� �� ����
        camTr.LookAt(targetTr.position);
         
        #endregion


        #region ���2-���帹�̾���
        
        //ī�޶��� ��ġ pos�� ���� slerp �Լ� ������ ���Լ��� ������ �ٸ��� �Ѿư�
        /*
        Vector3 pos = targetTr.position
            + (-targetTr.forward * distance)
            + (Vector3.up * height);
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime);
        */
        #endregion


           /*
        #region ���3 damping
        //������ ����� �������� distance��ŭ �̵�
        //���̸� height��ŭ �̵�
        Vector3 pos = targetTr.position //�÷��̾���ġ
            + (-targetTr.forward * distance)  //�÷��̾�κ��� �Ÿ�
            + (Vector3.up * height);    //(0,2,0)
        camTr.position = Vector3.SmoothDamp(
            camTr.position,         //������ġ
            pos,                    //��ǥ��ġ
            ref velocity,           //����ӵ�
            damping);               //��ǥ ��ġ������ ���� �ð�
        //ī�޶� �ǹ���ǥ������ ȸ��
        //camTr.LookAt(targetTr.position);
        #endregion
        */


        //ī�޶� �ǹ� ��ǥ�� ���� ȸ�� (+Offset�� ����� �þ߰��� ����)

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));


    }
}


