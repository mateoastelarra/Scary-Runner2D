using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Security.Principal;
using System;
using System.Linq;
using System.Runtime.Versioning;


public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;


    // Start is called before the first frame update
    public void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        Debug.Log(entryContainer.childCount);
        entryTemplate = entryContainer.transform.GetChild(0);
        //entryTemplate = entryContainer.Find("HighScoreEntryTemplates");
        entryTemplate.gameObject.SetActive(false);
        Debug.Log(entryTemplate == null);
        string jsonString = PlayerPrefs.GetString("highscoreTable", "{\"highScoreEntryList\":" +
            "[{\"score\":2152,\"name\":\"TAE\"}," +
            "{\"score\":1254,\"name\":\"AAA\"}," +
            "{\"score\":1157,\"name\":\"RAT\"}," +
            "{\"score\":854,\"name\":\"EAT\"}," +
            "{\"score\":554,\"name\":\"WYO\"}," +
            "{\"score\":244,\"name\":\"SOU\"}," +
            "{\"score\":201,\"name\":\"TEO\"}," +
            "{\"score\":193,\"name\":\"JON\"}," +
            "{\"score\":154,\"name\":\"WAL\"}," +
            "{\"score\":124,\"name\":\"JON\"}," +
            "{\"score\":1233,\"name\":\"KIL\"}," +
            "{\"score\":483,\"name\":\"Teo\"}," +
            "{\"score\":325,\"name\":\"TEO\"}," +
            "{\"score\":346,\"name\":\"TEO\"}," +
            "{\"score\":410,\"name\":\"TEO\"}," +
            "{\"score\":387,\"name\":\"TEO\"}," +
            "{\"score\":567,\"name\":\"TEO\"}," +
            "{\"score\":380,\"name\":\"TEO\"}," +
            "{\"score\":230,\"name\":\"TEO\"}," +
            "{\"score\":310,\"name\":\"TEO\"}," +
            "{\"score\":651,\"name\":\"TEO\"}," +
            "{\"score\":636,\"name\":\"TEO\"}," +
            "{\"score\":472,\"name\":\"TEO\"}," +
            "{\"score\":678,\"name\":\"TEO\"}]}");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        PlayerPrefs.SetString("highscoreTable", jsonString);
        PlayerPrefs.Save();



        /*
        if (highscores.highScoreEntryList[9].score < GameManager.Instance.score)
        {
            AddHighScoreEntry(GameManager.Instance.score, "TEO");
            highscores.highScoreEntryList[9].score = GameManager.Instance.score;
            highscores.highScoreEntryList[9].name = "TEO";
        }*/

        //Debug.Log(highscores.highScoreEntryList.Count);

        // Sort list
        for (int i = 0; i < highscores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highScoreEntryList.Count; j++)
            {
                if (highscores.highScoreEntryList[i].score < highscores.highScoreEntryList[j].score)
                {
                    highScoreEntry temp = highscores.highScoreEntryList[i];
                    highscores.highScoreEntryList[i] = highscores.highScoreEntryList[j];
                    highscores.highScoreEntryList[j] = temp;
                }
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        int maxNumberOfScores = 0;
        /*
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(entryTemplate == null);
            CreateHighScoreEntryTransform(highscores.highScoreEntryList[i], entryContainer, highScoreEntryTransformList);
            Debug.Log(i);
        }
        */

        foreach (highScoreEntry highScoreEntry in highscores.highScoreEntryList)
        {
            if (maxNumberOfScores < 10)
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
            }
            maxNumberOfScores++;
            Debug.Log(highScoreEntry.score);
        }



    }

    private void CreateHighScoreEntryTransform(highScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {

        float templateHeight = 50f;
        Debug.Log(entryTemplate == null);
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count() + 200f);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count() + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {
        // Create Entry
        highScoreEntry highScoreEntry = new highScoreEntry { score = score, name = name };

        // Load saved Entry list
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        // Add new entry to higscores
        highscores.highScoreEntryList.Add(highScoreEntry);

        //Save 
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<highScoreEntry> highScoreEntryList;
    }

    // Represents a single highscore entry
    [System.Serializable]
    private class highScoreEntry
    {
        public int score;
        public string name;
    }

}