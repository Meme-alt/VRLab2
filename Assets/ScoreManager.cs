using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Transform centerPoint;
    public Renderer boardRenderer;
    public Color hitColor = Color.green;

    private Color originalColor;
    private int totalScore = 0;

    void Start()
    {
        if (boardRenderer != null)
            originalColor = boardRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart"))
        {
            float dartDistance = Vector3.Distance(other.transform.position, centerPoint.position);
            int points = Mathf.RoundToInt(10f / (dartDistance + 0.1f));

            totalScore += points;
            scoreText.text = "Score: " + totalScore;

            Rigidbody dartRb = other.GetComponent<Rigidbody>();
            if (dartRb != null)
            {
                dartRb.isKinematic = true;
            }

            if (boardRenderer != null)
            {
                StartCoroutine(FlashColor());
            }
        }
    }

    private IEnumerator FlashColor()
    {
        boardRenderer.material.color = hitColor;
        yield return new WaitForSeconds(0.3f);
        boardRenderer.material.color = originalColor;
    }
}