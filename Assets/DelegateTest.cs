using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using static EventTest;


/// <summary>
/// Delegate(대리자)는 메소드(함수)를 변수처럼 전달할 수 있데 해주는 기능입니다.
/// 
/// 대리자란? 
/// 대리자는 메소드(함수)에 대한 참조를 캡슐화하는 객체입니다.
/// 호환되는 시그니쳐(함수의 반환 타입, 함수의 매개변수)를 가진 모든 메소드(함수)를 참조할 수 있으며,
/// 콜백 메소드 구현, 이벤트 처리 등의 고급 프로그래밍 기법을 쉽게 적용할 수 있게 합니다.
/// 
/// 주요 특징:
// 1, 메소드 참조: 대리자는 하나 이상의 메소드를 참조할 수 있으며,
// 대리자 타입에 맞는 시그니처를 가진 모든 메소드를 참조할 수 있습니다.
// 이를 통해 동적으로 메소드를 호출할 수 있습니다.

// 2, 타입 안전성: 대리자는 특정 시그니처를 가진 메소드만 참조할 수 있어,
// 타입 안전성을 제공합니다.
// 이는 대리자가 참조하는 메소드가 예상한 매개 변수와 반환 타입을 갖도록 합니다.

// 3, 멀티캐스팅:
// 대리자는 여러 메소드를 참조할 수 있는 멀티캐스트 대리자의 형태로 사용될 수 있으며,
// 이를 통해 이벤트 시스템을 구현할 수 있습니다.
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
		Debug.Log("Delegate 호출");
	}
	private void PrintText2()
	{
		GetComponent<EventTest>().OnUnityEvent.Invoke(new Test());
		Debug.Log("Delegate 호출 2");
	}

	private int PrintTextInt()
	{
		Debug.Log("Delegate 호출");
		return 0;
	}

	private int PrinttextIntByInput(int a,int b)
	{
		Debug.Log("Delegate 호출 : " + (a + b));
		return 0;
	}
}
