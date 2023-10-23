using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    [SerializeField] private GameObject leaderboardObject;
    private Leaderboard leaderboard;

    private void Start()
    {
        leaderboard = leaderboardObject.GetComponent<Leaderboard>();
        entryContainer = transform.Find("Container");
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 45f;


        // TODO - Get leaderboard data from data management script
        // Do not put 10 , put the size of the leaderboard

        foreach (string record in leaderboard.GetLeaderboardTimes())
        {
            int rank = leaderboard.GetLeaderboardTimes().IndexOf(record) + 1;
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * (rank - 1));
            entryTransform.gameObject.SetActive(true);

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
            entryTransform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = record;

            // TODO - Get name data from data management script
            // entryTransform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().text = names[i].ToString();
        }

    }
}
