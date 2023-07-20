using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public GameObject snakeSegmentPrefab;

    private List<GameObject> snakeSegments = new List<GameObject>();
    private string[] unicodeCharacters = new string[0];

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            AddRandomUnicodeCharacter();
        }

        MoveSnake();
    }

    private void AddRandomUnicodeCharacter()
    {
        string randomCharacter = char.ConvertFromUtf32(Random.Range(0x21, 0x7F));
        List<string> tempList = new List<string>(unicodeCharacters);
        tempList.Add(randomCharacter);
        unicodeCharacters = tempList.ToArray();
        CreateSnakeSegment();
    }

    private void MoveSnake()
    {
        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            snakeSegments[i].transform.position = snakeSegments[i - 1].transform.position;
        }

        if (snakeSegments.Count > 0)
        {
            snakeSegments[0].transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    private void CreateSnakeSegment()
    {
        GameObject newSegment = Instantiate(snakeSegmentPrefab, Vector3.zero, Quaternion.identity);
        newSegment.transform.SetParent(transform);
        newSegment.GetComponentInChildren<TextMesh>().text = unicodeCharacters[unicodeCharacters.Length - 1];
        snakeSegments.Add(newSegment);
    }
}

