using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
	public TextMeshProUGUI textMesh;
	public TMP_Dropdown dropdown;

    void Start()
    {
		textMesh.text = 
			"<color=#FF0000>SBS ����Ƽ ���� ����</color> \n �����Դϴ�.";

		dropdown.onValueChanged.AddListener((t) =>
		{
			Debug.Log(dropdown.options[t]);
		});
	}
}
