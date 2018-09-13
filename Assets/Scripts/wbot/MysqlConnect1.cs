using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MysqlConnect1 : MonoBehaviour {

    [SerializeField] private InputField userName;
    [SerializeField] private InputField userPass;
    [SerializeField] private InputField userEmail;
    [SerializeField] private Text messageText;
    [SerializeField] private Button register;
    [SerializeField] private Button login;
    [SerializeField] private string loginURL = "http://worldbattleoftanks.ru:8000/register/login.php";
    [SerializeField] private string registerURL = "http://worldbattleoftanks.ru:8000/reg/register.php";

    void Awake()
    {
        userPass.contentType = InputField.ContentType.Password;
        register.onClick.AddListener(() => { Register(); });
        login.onClick.AddListener(() => { Login(); });
    }

    bool IsValidEmail(string email) // валидация email
    {
        return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
    }

    bool IsValid(string value, int min, int max, string field) // валидация имени и пароля
    {
        if (value.Length < min)
        {
            Message("В поле [ " + field + " ] недостаточно символов, нужно минимум [ " + min + " ]");
            return false;
        }
        else if (value.Length > max)
        {
            Message("В поле [ " + field + " ] допустимый максимум символов, не более [ " + max + " ]");
            return false;
        }
        else if (Regex.IsMatch(value, @"[^\w\.@-]"))
        {
            Message("В поле [ " + field + " ] содержаться недопустимые символы.");
            return false;
        }

        return true;
    }

    void Message(string text)
    {
        messageText.text = text;
        Debug.Log(this + " --> " + text);
    }

    void Login()
    {
        if (!IsValid(userName.text, 3, 15, "Имя") || !IsValid(userPass.text, 6, 20, "Пароль")) return;

        WWWForm form = new WWWForm();
        form.AddField("name", userName.text);
        form.AddField("password", userPass.text);
        WWW www = new WWW(loginURL, form);
        StartCoroutine(LoginFunc(www));
    }

    void Register()
    {
        if (!IsValidEmail(userEmail.text))
        {
            Message("Email адрес указан не верно!");
            return;
        }

        if (!IsValid(userName.text, 3, 15, "Имя") || !IsValid(userPass.text, 6, 20, "Пароль")) return;

        WWWForm form = new WWWForm();
        form.AddField("name", userName.text);
        form.AddField("password", userPass.text);
        form.AddField("email", userEmail.text);
        WWW www = new WWW(registerURL, form);
        StartCoroutine(RegisterFunc(www));
    }

    IEnumerator LoginFunc(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            if (string.Compare(www.text, "Success!") == 0) // получаем в ответе слово-ключ из файла login.php
            {
                Message("Успешный вход!");
            }
            else
            {
                Message(www.text);
            }
        }
        else
        {
            Message("Error: " + www.error);
        }
    }

    IEnumerator RegisterFunc(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Message("Пользователь успешно добавлен в базу.");
        }
        else
        {
            Message("Error: " + www.error);
        }
    }
}