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
/// �̺�Ʈ�� ��ü�� Ŭ������ Ư�� ��Ȳ�� �ٸ� ��ü���� �˸��� ����� �����մϴ�.
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

		// ���ٽ�
		OnUnityEvent.AddListener((test) =>
		{
			Debug.Log(test.name + " : " + test.description);
		});

		OnUnityEvent2.AddListener(() =>
		{
			Debug.Log("OnUnityEvent2 ȣ��");
		});

	}

	private void OnEvent(Test test)
	{
		Debug.Log(test.name + " : " + test.description);
	}

	public void PrintText()
	{
		delegateTest.testDelegate();
		Debug.Log("EventTest ȣ��");
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
