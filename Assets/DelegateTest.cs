using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using static EventTest;


/// <summary>
/// Delegate(�븮��)�� �޼ҵ�(�Լ�)�� ����ó�� ������ �� �ֵ� ���ִ� ����Դϴ�.
/// 
/// �븮�ڶ�? 
/// �븮�ڴ� �޼ҵ�(�Լ�)�� ���� ������ ĸ��ȭ�ϴ� ��ü�Դϴ�.
/// ȣȯ�Ǵ� �ñ״���(�Լ��� ��ȯ Ÿ��, �Լ��� �Ű�����)�� ���� ��� �޼ҵ�(�Լ�)�� ������ �� ������,
/// �ݹ� �޼ҵ� ����, �̺�Ʈ ó�� ���� ��� ���α׷��� ����� ���� ������ �� �ְ� �մϴ�.
/// 
/// �ֿ� Ư¡:
// 1, �޼ҵ� ����: �븮�ڴ� �ϳ� �̻��� �޼ҵ带 ������ �� ������,
// �븮�� Ÿ�Կ� �´� �ñ״�ó�� ���� ��� �޼ҵ带 ������ �� �ֽ��ϴ�.
// �̸� ���� �������� �޼ҵ带 ȣ���� �� �ֽ��ϴ�.

// 2, Ÿ�� ������: �븮�ڴ� Ư�� �ñ״�ó�� ���� �޼ҵ常 ������ �� �־�,
// Ÿ�� �������� �����մϴ�.
// �̴� �븮�ڰ� �����ϴ� �޼ҵ尡 ������ �Ű� ������ ��ȯ Ÿ���� ������ �մϴ�.

// 3, ��Ƽĳ����:
// �븮�ڴ� ���� �޼ҵ带 ������ �� �ִ� ��Ƽĳ��Ʈ �븮���� ���·� ���� �� ������,
// �̸� ���� �̺�Ʈ �ý����� ������ �� �ֽ��ϴ�.
/// </summary>
public class DelegateTest : MonoBehaviour
{
	public delegate void TestDelegate();
	public delegate int TestDelegateInt();
	public delegate int TestDelegateIntByInput(int a, int b);

	public TestDelegate testDelegate;
	public TestDelegateInt testDelegateInt;
	public TestDelegateIntByInput testDelegateIntByInput;

	// Start is called before the first frame update
	void Start()
	{
		testDelegate = PrintText2;
		testDelegateInt = PrintTextInt;
		testDelegateIntByInput = PrinttextIntByInput;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			testDelegate();
			testDelegate -= PrintText2;
			testDelegateInt();
			testDelegateIntByInput(5, 7);
		}
	}

	public void PrintText(Test test)
	{
		Debug.Log("Delegate ȣ��");
	}
	private void PrintText2()
	{
		GetComponent<EventTest>().OnUnityEvent.Invoke(new Test());
		Debug.Log("Delegate ȣ�� 2");
	}

	private int PrintTextInt()
	{
		Debug.Log("Delegate ȣ��");
		return 0;
	}

	private int PrinttextIntByInput(int a,int b)
	{
		Debug.Log("Delegate ȣ�� : " + (a + b));
		return 0;
	}
}
