using System.Collections;
using UnityEngine;
using System.Text;

public class SnakeController : MonoBehaviour
{
    public GameObject textPrefab;
    public int maxTexts = 100;

    private string[] texts;
    private TextMesh[] textMeshes;
    private float textSpacing = 4.0f;

    private void Start()
    {
        texts = new string[maxTexts];
        textMeshes = new TextMesh[maxTexts];
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
			Debug.Log("Pressed");
            AddRandomCharacter();
        }
        if (Time.deltaTime % 2 == 0)
        {
            AddRandomCharacter();
        }

        MoveSnake();
    }

    private void AddRandomCharacter()
    {
        Debug.Log("AddRandomCharacter");
        for (int i = 0; i < maxTexts - 1; i++)
        {
            texts[i] = texts[i + 1];
            if (textMeshes[i] != null)
            {
                textMeshes[i].text = texts[i];
            }
        }

        string startWith = GenerateString(new System.Random(), (int) Random.Range(1, maxTexts - 1));
        var asBytes = Encoding.UTF8.GetBytes(startWith);
        var glitched = Encoding.Unicode.GetString(asBytes);

        char randomChar = (char)Random.Range(32, 255); // Unicode characters from space to DEL
        // Debug.Log(randomChar);
        texts[maxTexts - 1] = glitched;
        CreateText(maxTexts - 1);

    }

    private void MoveSnake()
    {
        for (int i = 0; i < maxTexts; i++)
        {
            if (textMeshes[i] != null)
            {
                textMeshes[i].transform.Translate(Vector3.left * Time.deltaTime * textSpacing);
            }
        }
    }

    private void CreateText(int index)
    {
        GameObject textObject = Instantiate(textPrefab, Vector3.zero, Quaternion.identity);
        textObject.transform.SetParent(transform);
        textObject.transform.position = new Vector3(9f, Random.Range(-5f, 5f), 0f); // Initial position on the right side
        textObject.GetComponent<TextMesh>().text = texts[index];
        textMeshes[index] = textObject.GetComponent<TextMesh>();
    }

    private char GenerateChar(System.Random rng)
{
    // 'Z' + 1 because the range is exclusive
    return (char) (rng.Next('A', 'Z' + 1));
}

private string GenerateString(System.Random rng, int length)
{
    char[] letters = new char[length];
    for (int i = 0; i < length; i++)
    {
        letters[i] = GenerateChar(rng);
    }
    return new string(letters);
}

}


