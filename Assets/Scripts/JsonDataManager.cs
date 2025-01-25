using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonDataManager : MonoBehaviour
{
    [Tooltip("저장하길 원하는 파일 이름(.json 제외)")]
    public string fileName = "JsonSample";

    [System.Serializable]
    public class Data
    {
        public int No;
        public string User;
        public int Score;
    }

    public Data data;

    // test
    void Start()
    {
        #region JsonData 추가
        data.No = 1;
        data.User = "test";
        data.Score = 1;
        SaveDataToJson();
        #endregion

        #region JonData 자료 확인
        LoadDataFromJson();
        string dataString;
        dataString = data.No.ToString();
        dataString += data.User;
        dataString += data.Score.ToString();
        Debug.Log(dataString);
        #endregion
    }

    void SaveDataToJson()
    {
        string jsonData = "";

        jsonData = JsonUtility.ToJson(data, true);
        
        File.WriteAllText(Application.dataPath + @"\" + fileName , jsonData);
    }

    
    void LoadDataFromJson()
    {
        string jsonData = "";
        jsonData = File.ReadAllText(Application.dataPath + @"\" + fileName);

        data = JsonUtility.FromJson<Data>(jsonData);
    }
}
