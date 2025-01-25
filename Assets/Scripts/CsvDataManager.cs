using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum GameScore
{
    No,
    User,
    Score
}

public class CsvDataManager : MonoBehaviour
{
    [SerializeField]
    public TextAsset csvFile;

    // csv를 나누는 기준
    char lineSeperator = '\n';

    public class CsvData
    {
        public int No;
        public string User;
        public int Score;
    }
    public Dictionary<string, CsvData> dataDictionary = new Dictionary<string, CsvData>();

    private void Start()
    {
       
        #region csv에 자료 추가
        CsvData csvData = new CsvData();
        csvData.No = 1;
        csvData.User = "test";
        csvData.Score = 1;
        dataDictionary.Add(csvData.No.ToString(), csvData);
        CreateCSV();
        #endregion

        #region csv 자료 확인
        CsvToDicionary();
        string dataString;
        dataString = GetObjData("1", GameScore.No);
        dataString += "," + GetObjData("1", GameScore.User);
        dataString += "," + GetObjData("1", GameScore.Score);
        Debug.Log(dataString);
        #endregion
    }

    void CsvToDicionary()
    {
        // 행별로 나눠서 저장
        string[] records = csvFile.text.Split(lineSeperator);

        int lineCount = 0;

        foreach (string record in records)
        {
            lineCount++;

            string[] fields = record.Split(',');

            if (string.IsNullOrEmpty(fields[0]))
            {
                break;
            }

            CsvData objData = new CsvData
            {
                No = int.Parse(fields[0]),
                User = fields[1],
                Score = int.Parse(fields[2])
            };

            if (!dataDictionary.ContainsKey(fields[0]) )
            {
                dataDictionary.Add(fields[0], objData);
                Debug.Log("Added: " + objData.No);
            }
            else
            {
                Debug.Log("duplicated object name exists");
            }
        }
    }

    public string GetObjData(string objName, GameScore dataName)
    {
        string data = "";

        if (dataDictionary.ContainsKey(objName) == false)
        {
            data = "None";
            return data;
        }

        switch (dataName)
        {
            case GameScore.No:
                data = dataDictionary[objName].No.ToString();
                break;
            case GameScore.User:
                data = dataDictionary[objName].User;
                break;
            case GameScore.Score:
                data = dataDictionary[objName].Score.ToString();
                break;
        }

        return data;
    }

    public void CreateCSV()
    {
        //Debug.Log(Application.dataPath + @"\my.csv");
        //Debug.Log(Application.dataPath + "\\my.csv");
        using (StreamWriter writer = new StreamWriter(Application.dataPath + @"\my.csv"))
        {
            foreach (var data in dataDictionary)
            {
                writer.WriteLine("{0},{1},{2}", data.Value.No, data.Value.User, data.Value.Score);
            }
        }
    }




}