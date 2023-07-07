using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Keypad : MonoBehaviour 
{
	[SerializeField] private Text _output;
	[SerializeField] private Text _input;

	public void Clicky ()
	{

		GetComponent<AudioSource>().Play();
	}

	public void input()
	{
		if(_output.text.Equals("Enter KeyCode"))
		{
			_output.text = string.Empty;
			_output.text = _input.text.ToString();		
		}
		else
		{
			_output.text += _input.text.ToString();		
		}
	}

	public void verify()
	{
		if(_input.text.Equals("C"))
		{
			reset();
		}
		else
		{
			string code = GameObject.Find("Dialogue").GetComponent<Dialogue>().getCode();

			if(_output.text.Equals(code))
			{
				_output.text = "Correct!";	
				GameObject.Find("DoorWest").SendMessage("Activate");

				Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameObject.Find("Player").GetComponent<MouseLook>().Enable = true;
				
				Destroy(GameObject.Find("Door3Trigger"));
				Destroy(GameObject.Find("Keypad"));
			}
			else
				_output.text = "Invalid!";	

		}

	}

	public void reset()
	{
		_output.text = string.Empty;
		_output.text = "Enter KeyCode";	
	}

}


