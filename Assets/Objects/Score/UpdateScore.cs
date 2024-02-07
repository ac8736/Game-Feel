using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        this.score.text = GlobalVars.score.ToString() + "\n" + GlobalVars.combo.ToString() + "x";
    }
}
