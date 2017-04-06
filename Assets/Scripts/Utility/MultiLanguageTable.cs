using UnityEngine;
using System.Collections;

public class MultiLanguageTable : MonoBehaviour
{
    private static MultiLanguageTable instance = null;
    public static MultiLanguageTable Instance { get { return instance; } }
    
    private string tableLocalPath;
    public string TableLocalPath { get { return tableLocalPath; } }

    private JSONObject table;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("MultiLanguageTable duplcate, destroy myself.");
            this.enabled = false;
            GameObject.Destroy(this);
        }
    }

    void Start()
    {
        tableLocalPath = System.IO.Path.Combine(Application.temporaryCachePath + "/", "l10n.txt");
        Load();
    }

    public void Load()
    {
        if (UnityUtility.IsDirectoryExist(tableLocalPath))
        {
            string jsonStringTable = UnityUtility.ReadTextFile(tableLocalPath, System.Text.Encoding.UTF8);
            try
            {
                table = new JSONObject(jsonStringTable);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }
        else
        {
            LoadDefault();
        }
    }

    public string GetValue(string lang, string key)
    {
        if (table == null)
            return "????";

        JSONObject langTable = table.GetField(lang);
        if (langTable == null)
            return "????";

        JSONObject valueJson = langTable.GetField(key);
        if (valueJson == null)
            return "????";

        return valueJson.str;
    }

    private void LoadDefault()
    {
        table = new JSONObject();

        #region English
        JSONObject english = new JSONObject();
        english.AddField("UI.AddBehavButton.ClickOpenWebPage", "click open web page");
        english.AddField("UI.AddBehavButton.ClickPhoneCall", "click phone call");
        english.AddField("UI.AddBehavButton.ClickSendEmail", "click send email");
        english.AddField("UI.AddBehavButton.ClickPlayAnimation", "click play anim");

        english.AddField("UI.Toast.InputICError", "input code error");
        table.AddField("English", english);
        #endregion

        #region Chinese Traditional
        JSONObject chineseTraditional = new JSONObject();
        chineseTraditional.AddField("UI.AddBehavButton.ClickOpenWebPage", "點擊開啟網頁");
        chineseTraditional.AddField("UI.AddBehavButton.ClickPhoneCall", "點擊撥打電話");
        chineseTraditional.AddField("UI.AddBehavButton.ClickSendEmail", "點擊傳送郵件");
        chineseTraditional.AddField("UI.AddBehavButton.ClickPlayAnimation", "點擊播放動作");

        chineseTraditional.AddField("UI.Toast.InputICError", "錯誤的邀請碼");
        table.AddField("ChineseTraditional", chineseTraditional);
        #endregion

        #region Chinese Simplified
        JSONObject chineseSimplified = new JSONObject();
        chineseSimplified.AddField("UI.AddBehavButton.ClickOpenWebPage", "点击开启网页");
        chineseSimplified.AddField("UI.AddBehavButton.ClickPhoneCall", "点击拨打电话");
        chineseSimplified.AddField("UI.AddBehavButton.ClickSendEmail", "点击传送邮件");
        chineseSimplified.AddField("UI.AddBehavButton.ClickPlayAnimation", "点击播放动作");

        chineseSimplified.AddField("UI.Toast.InputICError", "错误的邀请码");
        table.AddField("ChineseSimplified", chineseSimplified);
        #endregion

        #region Chinese
        JSONObject chinese = new JSONObject();
        chinese.AddField("UI.AddBehavButton.ClickOpenWebPage", "點擊開啟網頁");
        chinese.AddField("UI.AddBehavButton.ClickPhoneCall", "點擊撥打電話");
        chinese.AddField("UI.AddBehavButton.ClickSendEmail", "點擊傳送郵件");
        chinese.AddField("UI.AddBehavButton.ClickPlayAnimation", "點擊播放動作");

        chinese.AddField("UI.Toast.InputICError", "錯誤的邀請碼");
        table.AddField("Chinese", chineseTraditional);
        #endregion
    }
}
