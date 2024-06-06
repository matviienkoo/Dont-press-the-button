using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using YG;

namespace YG.Example{
public class GameScript : MonoBehaviour
{
    public TextMeshProUGUI MainText;
    public string[] ListText;
    public string[] ListText_Eng;
    public int IntList;

    [Header("Кнопки")]
    public GameObject MainButton;
    public GameObject MainTextObject;
    public Animation Move_MainButton_Animation;
    private bool BoolInftine;
    private bool BoolSecret;

    public GameObject RaindbowButton;
    public Animation Move_RaindbowButton_Animation;

    public GameObject DifferentButton;
    public Animation Move_DifferentButton_Animation;

    public GameObject FakeButtons_Small;
    public Animation Move_FakeButtons_Small_Animation;

    public GameObject FakeButtons_Medium;
    public Animation Move_FakeButtons_Medium_Animation;

    public GameObject FakeButtons_Hard;
    public Animation Move_FakeButtons_Hard_Animation;

    [Header("Ивенты")]
    public Animation ButtonAnimation;
    public Animation AdsAnimation;
    public Animation BoomAnimation;
    public Animation MainSecretAnimation;
    public GameObject MainSecretObject;
    public Animation MainSecretObjectAnimation;
    public Animation StickerFunAnimation;
    public GameObject StickerFun;
    public Animation StickerSadAnimation;
    public GameObject StickerSad;
    public GameObject YouObject;
    public Animation YouObjectAnimation;
    public GameObject ConfettiObject;
    public GameObject ScaryMusicObject;
    public GameObject LetterObject;
    public GameObject AdsObject;
    public GameObject EndObject;

    [Header("Локализация")]
    public string LanguageString;

    [Header("Реклама")]
    public YandexGame sdk;
    public int IntClicks;
    public int AdsPoint = 1;

    [Header("Звуки")]
    public AudioSource AudioButton;
    public AudioSource AudioMove;
    public AudioSource AudioBomb;
    public AudioSource AudioFun;
    public AudioSource AudioWin;
    public AudioSource AudioScary;

    [Header("Скрипты")]
    public EventSystem EventFunction;

    private void Start ()
    {
        SwitchLanguage(YandexGame.savesData.language);
        IntList = PlayerPrefs.GetInt("IntList");
        AdsPoint = PlayerPrefs.GetInt("AdsPoint");
        IntClicks = PlayerPrefs.GetInt("IntClicks");

        if (AdsPoint <= 0)
        {
            AdsPoint = 1;
            PlayerPrefs.SetInt("AdsPoint",AdsPoint);
        }

        if (LanguageString == "ru"){
        MainText.text = ListText[IntList];
        }
        if (LanguageString == "eng"){
        MainText.text = ListText_Eng[IntList];
        }
        if (IntList == 51 || IntList == 59 || IntList == 103)
        {
            AdsObject.SetActive(true);
            AdsAnimation.Play("AdsShow");

            if (LanguageString == "ru"){
            MainText.text = "Нажми " + IntClicks + " раз";
            }
            if (LanguageString == "eng"){
            MainText.text = "Press " + IntClicks + " times";
            }
        }

        StartCoroutine(IEnumeratorText()); EventFunction.enabled = false;
    }

    private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;
    private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

    public void SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                LanguageString = "ru";
                break;
            case "tr":
                LanguageString = "ru";
                break;
            default:
                LanguageString = "eng";
                break;
        }
    }

    // Система плавного появления текста
    IEnumerator IEnumeratorText () 
    {
        var originalString = MainText.text;
        MainText.text = "";

        var numCharsRevealed = 0;
        while (numCharsRevealed < originalString.Length)
        {
            ++numCharsRevealed;
            MainText.text = originalString.Substring(0, numCharsRevealed);
            if (MainText.text == originalString)
            {
                EventFunction.enabled = true;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void CLickButton ()
    {
        // Кликер текст
        if (IntClicks > 0)
        {
            IntClicks -= AdsPoint;
            AudioButton.Play();

            if (LanguageString == "ru"){
            MainText.text = "Нажми " + IntClicks + " раз";
            }
            if (LanguageString == "eng"){
            MainText.text = "Press " + IntClicks + " times";
            }

            PlayerPrefs.SetInt("IntClicks",IntClicks);
        }

        // Классический текст
        if (IntClicks <= 0)
        {
            IntList++;

            if (LanguageString == "ru"){
            MainText.text = ListText[IntList];
            }
            if (LanguageString == "eng"){
            MainText.text = ListText_Eng[IntList];
            }

            AudioButton.Play();
            StartCoroutine(IEnumeratorText()); EventFunction.enabled = false;
            PlayerPrefs.SetInt("IntList",IntList);
        }

        // 8 (кнопка уменьшается)
        if (IntList == 8){
        ButtonAnimation.Play("SmallButton");
        }

        // 9 (кнопка увеличивается)
        if (IntList == 9){
        ButtonAnimation.Play("NormalButton");
        }

        // 10 (я веселый кот)
        if (IntList == 10){
        StickerFun.SetActive(true);
        }

        // 11 (я грустный кот)
        if (IntList == 11){
        StickerFun.SetActive(false);
        StickerSad.SetActive(true);
        }

        // 12 (я не кот)
        if (IntList == 12){
        StickerSadAnimation.Play("HideImage");
        }

        // 13 (мало кнопок)
        if (IntList == 13){
        StartCoroutine(IEnumeratorMove());
        }

        // 14 (среднее кнопок)
        if (IntList == 14){
        StartCoroutine(IEnumeratorMove());
        }

        // 15 (много кнопок)
        if (IntList == 15){
        StartCoroutine(IEnumeratorMove());
        }

        // 16 (все возвращается до классики)
        if (IntList == 16){
        StartCoroutine(IEnumeratorMove());
        }

        // 22 (разноцветные кнопки)
        if (IntList == 22){
        StartCoroutine(IEnumeratorMove());
        }

        // 35 (взрыв)
        if (IntList == 35){
        BoomAnimation.Play();
        AudioBomb.Play();
        }

        // 47 (движение кнопки)
        if (IntList == 47){
        StartCoroutine(IEnumeratorInfiniteMove());
        BoolInftine = true;
        }

        // 48 (классическое расположение)
        if (IntList == 48){
        BoolInftine = false;
        }

        // 50 (нажми 200 раз)
        if (IntList == 50){
        AdsObject.SetActive(true);
        AdsAnimation.Play("AdsShow");
        BoolInftine = false;
        IntClicks = 200;
        IntList = 51;
        PlayerPrefs.SetInt("IntClicks",IntClicks);
        PlayerPrefs.SetInt("IntList",IntList);
        }

        // 52 убирает бонус от рекламы
        if (IntList == 52){
        AdsAnimation.Play("AdsHide");
        StartCoroutine(IEnumeratorObjects());
        }

        // 58 (нажми 1000 раз)
        if (IntList == 58){
        AdsObject.SetActive(true);
        AdsAnimation.Play("AdsShow");
        IntClicks = 1000;
        IntList = 59;
        PlayerPrefs.SetInt("IntClicks",IntClicks);
        PlayerPrefs.SetInt("IntList",IntList);
        }

        // 60 (все возвращается до классики)
        if (IntList == 60){
        AdsAnimation.Play("AdsHide");
        StartCoroutine(IEnumeratorObjects());
        }

        // 69 (главный секрет)
        if (IntList == 68){
        LetterObject.SetActive(true);
        MainSecretAnimation.Play("AdsShow");
        BoolSecret = true;
        }

        // 74 (салют)
        if (IntList == 74){
        ConfettiObject.SetActive(true);
        AudioWin.Play();

        if (BoolSecret == true)
        {
            MainSecretAnimation.Play("AdsHide");
            StartCoroutine(IEnumeratorObjects());
        }
        }

        // 81 иные кнопки
        if (IntList == 81){
        StartCoroutine(IEnumeratorMove());
        }

        // 81 все вернуть на классику
        if (IntList == 82){
        StartCoroutine(IEnumeratorMove());
        }

        // 85 взрыв
        if (IntList == 85){
        BoomAnimation.Play();
        AudioBomb.Play();
        }

        // 92 страшная музыка
        if (IntList == 92){
        ScaryMusicObject.SetActive(true);
        }

        // 94 посмотри на себя
        if (IntList == 94){
        YouObject.SetActive(true);
        }

        // 98 все поставить на классику
        if (IntList == 98){
        StartCoroutine(IEnumeratorYou());
        }

        // 102 сто тысяч кликов
        if (IntList == 102){
        AdsObject.SetActive(true);
        AdsAnimation.Play("AdsShow");
        IntClicks = 100000;
        IntList = 103;
        PlayerPrefs.SetInt("IntClicks",IntClicks);
        PlayerPrefs.SetInt("IntList",IntList);
        }

        // 104 спрятать бонус по рекламе
        if (IntList == 104){
        AdsAnimation.Play("AdsHide");
        StartCoroutine(IEnumeratorObjects());
        }

        // 114 концовка
        if (IntList == 114){
        ShowFullScreenAd();
        } 
    }

    public void RaindbowClick (int IntRainbow)
    {
        // Нажми на желтую кнопку
        if (IntList == 24 && IntRainbow == 0)
        {
            IntList++;
            if (LanguageString == "ru"){
            MainText.text = ListText[IntList];
            }
            if (LanguageString == "eng"){
            MainText.text = ListText_Eng[IntList];
            }
            StartCoroutine(IEnumeratorText()); EventFunction.enabled = false;

            // Отключить радужные кнопки
            StartCoroutine(IEnumeratorMove());
        }

        // Нажми на желтую кнопку
        if (IntList == 23 && IntRainbow == 0)
        {
            IntList++;
            if (LanguageString == "ru"){
            MainText.text = ListText[IntList];
            }
            if (LanguageString == "eng"){
            MainText.text = ListText_Eng[IntList];
            }
            StartCoroutine(IEnumeratorText()); EventFunction.enabled = false;
        }

        // Нажми на красную кнопку
        if (IntList == 22 && IntRainbow == 1)
        {
            IntList++;
            if (LanguageString == "ru"){
            MainText.text = ListText[IntList];
            }
            if (LanguageString == "eng"){
            MainText.text = ListText_Eng[IntList];
            }
            StartCoroutine(IEnumeratorText()); EventFunction.enabled = false;
        }
    }

    IEnumerator IEnumeratorMove()
    {
        // Малое количество кнопок показывается
        if (IntList == 13)
        {
            FakeButtons_Small.SetActive(true);
            Move_MainButton_Animation.Play("UpMoveButton");
        }

        // Среднее количество кнопок показывается
        if (IntList == 14)
        {
            FakeButtons_Medium.SetActive(true);
            Move_FakeButtons_Small_Animation.Play("UpMoveButton");
        }

        // Большое количество кнопок показывается
        if (IntList == 15)
        {
            FakeButtons_Hard.SetActive(true);
            Move_FakeButtons_Medium_Animation.Play("Medium_UpMoveButton");
        }

        // (Возвращение кнопок на исходное положение)
        if (IntList == 16)
        {
            Move_FakeButtons_Hard_Animation.Play("Hard_UpMoveButton");
        }

        // (Разноцветные кнопки)
        if (IntList == 22)
        {
            RaindbowButton.SetActive(true);
            Move_MainButton_Animation.Play("UpMoveButton");
        }

        // (Возвращение кнопок на исходное положение)
        if (IntList == 25)
        {
            Move_RaindbowButton_Animation.Play("UpMoveButton");
        }

        // (Иные кнопки)
        if (IntList == 81)
        {
            DifferentButton.SetActive(true);
            Move_MainButton_Animation.Play("UpMoveButton");
        }

        // (Возвращение кнопок на исходное положение)
        if (IntList == 82)
        {
            Move_DifferentButton_Animation.Play("Medium_UpMoveButton");
        }

        yield return new WaitForSeconds(0.3f);

        if (IntList == 13)
        {
            Move_FakeButtons_Small_Animation.Play("DownMoveButton");
        }

        if (IntList == 14)
        {
            Move_FakeButtons_Medium_Animation.Play("Medium_DownMoveButton");
        }

        if (IntList == 15)
        {
            Move_FakeButtons_Hard_Animation.Play("Hard_DownMoveButton");
        }

        if (IntList == 16)
        {
            Move_MainButton_Animation.Play("DownMoveButton");
        }

        if (IntList == 22)
        {
            Move_RaindbowButton_Animation.Play("DownMoveButton");
        }

        if (IntList == 25)
        {
            Move_MainButton_Animation.Play("DownMoveButton");
        }

        if (IntList == 81)
        {
            Move_DifferentButton_Animation.Play("Medium_DownMoveButton");
        }

        if (IntList == 82)
        {
            Move_MainButton_Animation.Play("DownMoveButton");
        }

        yield return new WaitForSeconds(0.3f);

        if (IntList == 16)
        {
            FakeButtons_Small.SetActive(false);
            FakeButtons_Medium.SetActive(false);
            FakeButtons_Hard.SetActive(false);
        }

        if (IntList == 25)
        {
            RaindbowButton.SetActive(false);
        }

        if (IntList == 82)
        {
            DifferentButton.SetActive(false);
        }
    }

    IEnumerator IEnumeratorInfiniteMove()
    {
        ButtonAnimation.Play("MoveButton");
        yield return new WaitForSeconds(0.2f);

        if (BoolInftine == true)
        StartCoroutine(IEnumeratorInfiniteMove());
    }

    IEnumerator IEnumeratorYou()
    {
        YouObjectAnimation.Play("YouHide");
        yield return new WaitForSeconds(0.3f);
        YouObject.SetActive(false);
    }

    public void SecretPanel (int i)
    {   
        if (i >= 1)
        {
            MainSecretObjectAnimation.Play("HideLetter");
            MainSecretAnimation.Play("AdsHide");
            StartCoroutine(IEnumeratorSecretPanel());
            StartCoroutine(IEnumeratorObjects());
            BoolSecret = false;
        }

        if (i == 0)
        {
            MainSecretObject.SetActive(true);
        }
    }

    IEnumerator IEnumeratorSecretPanel()
    {
        yield return new WaitForSeconds(0.2f);
        MainSecretObject.SetActive(false);
    }

    IEnumerator IEnumeratorObjects()
    {
        yield return new WaitForSeconds(1f);
        AdsObject.SetActive(false);
        LetterObject.SetActive(false);
    }

    // Звук нажатия 
    public void ButtonSound ()
    {
        AudioButton.Play();
    }

    public void FunSound ()
    {
        AudioFun.Play();
    }

    // Реклама с вознагрождение
    public void ShowRewardedAd()
    {
        sdk._RewardedShow(1);
    }

    public void BonusAds()
    {
        // Второе использование, и остальнные
        if (AdsPoint >= 2)
        {
            AdsPoint = AdsPoint * 2;
        }

        // Первое использование бонуса
        if (AdsPoint == 1)
        {
            AdsPoint = 2;
        }

        PlayerPrefs.SetInt("AdsPoint",AdsPoint);
    }

    // Реклама фулскрин
    public void ShowFullScreenAd()
    {
        sdk._FullscreenShow();
    }

    public void FullScreenEnd ()
    {
        ScaryMusicObject.SetActive(false);
        MainButton.SetActive(false);
        MainTextObject.SetActive(false);
        EndObject.SetActive(true);
    }
}
}