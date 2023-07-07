using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Text _messages;
    [SerializeField] private string[] lines;
    [SerializeField] private float _textSpeed;
    private int _index;
    private string code;
    
    // Start is called before the first frame update

    void Start()
    {
        int size = lines.Length;
        code = generatePin().ToString();
        lines[size-1] += code;              //Append keycode with last element of an array.
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(_messages.text.Equals(lines[_index]))
            {
                NextLine();
            }
            // else
            // {
            //     StopCoroutine(TypeLine());
            //     _messages.text = lines[_index];
            // } 
        }
    }

    public void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //print(_index);
        foreach(char c in lines[_index].ToCharArray())
        {
            _messages.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    void NextLine()
    {
        if(_index < lines.Length)
        {
            _index++;
            _messages.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    int generatePin()
    {
       return Random.Range(1000, 10000);
    }

    public string getCode()
    {
        return code;
    }
}
