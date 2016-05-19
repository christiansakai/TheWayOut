using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class menuScript : MonoBehaviour {
	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
	public string Level1;
//	public string SceneToLoad;
	State state;

	// Use this for initialization
	void Start () {
		state = GameObject.Find ("GameState").GetComponent<State> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
	}
	
	public void ExitPress()
	{
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel()
	{
		Debug.Log (state.currentLevel);
		state.LoadScene (state.currentLevel);
	}

	public void LoadScene(){
		state.LoadScene ("LevelSelect");
	}

	public void ExitGame(){
		Application.Quit ();
	}
		

}
