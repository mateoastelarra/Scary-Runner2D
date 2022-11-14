
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class hst : MonoBehaviour
{
    private Transform hscontainer;
    private Transform hstemplate;
    private List<highScoreEntry> hsEntryList;
    private List<Transform> hsEntryTransList;

    private void Awake()
    {
        hscontainer = transform.Find("HScontainer");
        hstemplate = hscontainer.Find("HStemplate");

        hstemplate.gameObject.SetActive(false);
        hsEntryList = new List<highScoreEntry>(){
            new highScoreEntry{score = 3464, name = "JUL" },
            new highScoreEntry{score = 2423, name = "MAT" },
            new highScoreEntry{score = 2397, name = "GEO" },
            new highScoreEntry{score = 2022, name = "JIU" },
            new highScoreEntry{score = 1812, name = "JOK" },
            new highScoreEntry{score = 1492, name = "BAT" },
            new highScoreEntry{score = 1347, name = "BRU" },
            new highScoreEntry{score = 1223, name = "GER" },
            new highScoreEntry{score = 1122, name = "LAU" },
            new highScoreEntry{score = 1011, name = "LOU" },
        };

        hsEntryTransList = new List<Transform>();

       /*
       if (hsEntryList[9].score < GameManager.Instance.score)
       {
           //AddHighScoreEntry(GameManager.Instance.score, "TEO");
           hsEntryList[9].score = GameManager.Instance.score;
           hsEntryList[9].name = "TEO";
       }*/


        foreach (highScoreEntry hsEntry in hsEntryList)
        {
            CreateHsEntryTransform(hsEntry, hscontainer, hsEntryTransList);
        }

    }

        private void CreateHsEntryTransform(highScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight = 30f;
            Transform entryTrans = Instantiate(hstemplate, container);
            RectTransform entryRectTrans = entryTrans.GetComponent<RectTransform>();
            entryRectTrans.anchoredPosition = new Vector2(-4.8f, -templateHeight * transformList.Count);
            entryTrans.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            string nameString = highScoreEntry.name;

            int score = highScoreEntry.score;
            entryTrans.Find("rank").GetComponent<Text>().text = rankString;
            entryTrans.Find("name").GetComponent<Text>().text = nameString;
            entryTrans.Find("score").GetComponent<Text>().text = score.ToString();

            transformList.Add(entryTrans);
        }


        // Represents a single highscore entry

        private class highScoreEntry
        {
            public int score;
            public string name;
        }
    
}
