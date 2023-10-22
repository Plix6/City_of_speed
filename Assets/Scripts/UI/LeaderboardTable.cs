using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Start()
    {
        entryContainer = transform.Find("Container");
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 45f;

        // TODO - Get leaderboard data from data management script
        // Do not put 10 , put the size of the leaderboard
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default: rankString = rank + "TH"; break;
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("PosText").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;

            // TODO - Get leaderboard data from data management script
            // entryTransform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = values[i].ToString();

            // TODO - Get name data from data management script
            // entryTransform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().text = names[i].ToString();
        }

    }
}
