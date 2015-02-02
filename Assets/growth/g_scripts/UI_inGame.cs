using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_inGame : MonoBehaviour {

	
    public GameObject MPUI;
    private Text MPUI_c;
    private Text HPUI_c;
    public GameObject HPUI;
    public GameObject playerstat;
    private PlayerDamage currentstat;
    // Use this for initialization
	
    void Start () {

        playerstat = GameObject.FindWithTag("Player");
        currentstat = playerstat.GetComponent<PlayerDamage>();
        MPUI_c = MPUI.GetComponent<Text>();
        HPUI_c = HPUI.GetComponent<Text>();

	}
  	
	// Update is called once per frame
	void Update () {

            
        MPUI_c.text = currentstat.MP.ToString();
        HPUI_c.text = currentstat.HP.ToString();

	}
}
