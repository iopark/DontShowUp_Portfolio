using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateBase<TOwner> where TOwner : MonoBehaviour
{
    protected TOwner owner; // � component�� state�� ���ϰ� �ִ��� �����ϰ� �ȴٸ�, �� State�� ���� ��ü�� ���ؼ� ����, ��ȣ�ۿ��� �����Ͽ�����. 
    public StateBase(TOwner owner)
    {
        this.owner = owner;
    }
    // �̷��� ��� State�� ���ؼ� �� ���ϰԵ� ���µ鿡���ؼ� ��Ȱ�� ĸ��ȭ �۾��� ���ؼ�, �߻�ȭ �۾��� ���������ټ��� �ְڴ�.
    public abstract void SetUp(); 
    public abstract void Enter(); 
    public abstract void Update();
    public abstract void Exit(); 
}
