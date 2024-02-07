using Assets;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreBox;

    private float score;
    private float growthSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreBox = GetComponent<TMP_Text>();
    }

    void Update()
    {
        score += growthSpeed * Time.deltaTime;

        if (score > GameController.self.Score || !GameFeelConfig.config[GameFeelFeature.TextAnimation])
        {
            score = GameController.self.Score;
            growthSpeed = 0;
        }

        float minSpeed = (GameController.self.Score - score) / 0.5f;
        if (score < GameController.self.Score && (growthSpeed < minSpeed))
        {
            growthSpeed = minSpeed;
        }
        scoreBox.text = $"{(int)score}";
    }

}
