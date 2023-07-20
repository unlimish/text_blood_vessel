using System.Collections;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public GameObject textPrefab;
    public int maxTexts = 100;

    private string[] texts;
    private TextMesh[] textMeshes;

    private void Start()
    {
        texts = new string[maxTexts];
        textMeshes = new TextMesh[maxTexts];
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            AddRandomCharacter();
        }

        MoveSnake();
    }

    private void AddRandomCharacter()
    {
        for (int i = 0; i < maxTexts - 1; i++)
        {
            texts[i] = texts[i + 1];
            textMeshes[i].text = texts[i];
        }

        char randomChar = (char)Random.Range(32, 127); // Unicode characters from space to DEL
        texts[maxTexts - 1] = randomChar.ToString();
        CreateText(maxTexts - 1);
    }

    private void MoveSnake()
    {
        for (int i = 0; i < maxTexts; i++)
        {
            if (textMeshes[i] != null)
            {
                textMeshes[i].transform.Translate(Vector3.left * Time.deltaTime);
            }
        }
    }

    private void CreateText(int index)
    {
        GameObject textObject = Instantiate(textPrefab, Vector3.zero, Quaternion.identity);
        textObject.transform.SetParent(transform);
        textObject.transform.position = new Vector3(9f, Random.Range(-4.5f, 4.5f), 0f); // Initial position on the right side
        textObject.GetComponent<TextMesh>().text = texts[index];
        textMeshes[index] = textObject.GetComponent<TextMesh>();
    }
}

