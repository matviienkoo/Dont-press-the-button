using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace YG.Example
{
	public class LanguageExample : MonoBehaviour
	{
		[SerializeField] string ru;
		[SerializeField] string en;
		[SerializeField] string tr;

		TextMeshProUGUI textObj;

		private void Awake()
		{
			textObj = GetComponent<TextMeshProUGUI>();
			SwitchLanguage(YandexGame.savesData.language);
		}

		private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;
		private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

		public void UpdateLanguageText ()
		{
			textObj = GetComponent<TextMeshProUGUI>();
			SwitchLanguage(YandexGame.savesData.language);
		}

		public void SwitchLanguage(string lang)
		{
			switch (lang)
			{
				case "ru":
					textObj.text = ru;
					break;
				case "tr":
					textObj.text = tr;
					break;
				default:
					textObj.text = en;
					break;
			}

			StartCoroutine(IEnumeratorUpdate());
		}

		IEnumerator IEnumeratorUpdate () 
	    {
	        yield return new WaitForSeconds(0.1f);
	        SwitchLanguage(YandexGame.savesData.language);
	    }
	}
}