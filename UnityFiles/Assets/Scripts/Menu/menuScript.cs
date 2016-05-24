using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class menuScript : MonoBehaviour {
	public Canvas quitMenu;
	public Button startText;
	public Button exitText;

	State state;

	void Start () {
		state = State.instance;
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
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
		state.LoadScene (state.currentLevel == "Menu" ? "1": state.currentLevel);
	}

	public void LoadScene(){
		state.LoadScene ("LevelSelect");
	}

	public void ExitGame(){
		Application.Quit ();
	}

}
