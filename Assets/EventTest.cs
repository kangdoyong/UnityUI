using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Test
{
	public string name;
	public string description;
}

/// <summary>
/// 이벤트는 객체나 클래스가 특정 상황을 다른 객체에게 알리는 기능을 제공합니다.
/// 
/// 
/// </summary>
public class EventTest : MonoBehaviour
{
	[SerializeField] private string Name;
	[SerializeField] private string Description;

	public UnityEvent<Test> OnUnityEvent;

	public UnityEvent OnUnityEvent2;

	public DelegateTest delegateTest;

	// Start is called before the first frame update
	void Start()
	{
		delegateTest = GetComponent<DelegateTest>();
		OnUnityEvent.AddListener(OnEvent);
		OnUnityEvent.AddListener(delegateTest.PrintText);

		// 람다식
		OnUnityEvent.AddListener((test) =>
		{
			Debug.Log(test.name + " : " + test.description);
		});

		OnUnityEvent2.AddListener(() =>
		{
			Debug.Log("OnUnityEvent2 호출");
		});

	}

	private void OnEvent(Test test)
	{
		Debug.Log(test.name + " : " + test.description);
	}

	public void PrintText()
	{
		delegateTest.testDelegate();
		Debug.Log("EventTest 호출");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			Test test = new Test();
			test.name = Name;
			test.description = Description;
			OnUnityEvent.Invoke(test);
		}
	}
}
