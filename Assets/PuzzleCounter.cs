using TMPro;
using UnityEngine;
public class PuzzleCounter : MonoBehaviour
{
    // References to textProgess on Computer GameObject and variables for puzzle completion
    public int totalPuzzles = 3;
    public int solvedPuzzles;
    private TMP_Text textProgress;

    private void Start()
    {
        textProgress = GetComponent<TMP_Text>();
        UpdateTextProgress();
    }
    
    // Increments when puzzle is completed
    public void IncrementSolvedPuzzles()
    {
        solvedPuzzles++;
        UpdateTextProgress();
    }

    // Update the text object
    private void UpdateTextProgress()
    {
        if(solvedPuzzles <= 3)
        {
            textProgress.text = $"{solvedPuzzles}/{totalPuzzles}";
        }
    }
}
