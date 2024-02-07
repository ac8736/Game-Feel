using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreBox;

    private int score;
    private int combo;
    // Start is called before the first frame update
    void Start()
    {
        scoreBox = GetComponent<TMP_Text>();
    }

    void Update()
    {

        if (score > GlobalVars.score)
        {
            score = GlobalVars.score;
        }
        if (GlobalVars.score > score)
        {
            score += 5;
        }
        if (GlobalVars.combo > combo)
        {
            combo = GlobalVars.combo;
        }
        if (GlobalVars.combo < combo)
        {
            combo--;
        }
        this.scoreBox.text = score.ToString() + "\n" + combo.ToString() + "x";
    }

}
