using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0414
// Testing data class
public class TestingData : MonoBehaviour
{
    [Header("Seeds array")]
    public int[] seeds = new int[10];
    public int seed;

    [Header("Character Rigs")]
    public GameObject rigOne;
    public GameObject rigTwo;

    [Header("Firebase push data references")]
    public FirebasePushData generatedValues;
    public FirebasePushData controllerValues;

    [Header("Image fading")]
    public Image fader;
    public Text round;
    public float timer = 2.0f;

    // Values not generated
    private bool valuesGenerated = false;
    private bool newValuesOne = false;
    private bool newValuesTwo = false;

    // Rank Vector 
    public int[] ranking;
    public int i = 0;
    public int j = 0;
    public bool end = false;

    // Generate values references
    private GenerateValues generateValuesOne;
    private GenerateValues generateValuesTwo;

    // Controller references
    private Controller controllerOne;
    private Controller controllerTwo;

    // Start is called before the first frame update
    void Awake()
    {
        // The ranking array
        ranking = new int[seeds.Length];

        // Random seeds
        Random.InitState(seed);
        for (int i = 0; i < seeds.Length; i++)
            seeds[i] = Random.Range(0, 1000);

        // Get the references
        generateValuesOne = rigOne.GetComponent<GenerateValues>();
        generateValuesTwo = rigTwo.GetComponent<GenerateValues>();
        controllerOne = rigOne.GetComponent<Controller>();
        controllerTwo = rigTwo.GetComponent<Controller>();
        generateValuesOne.generateValues = false;
        generateValuesTwo.generateValues = false;


        //generateValuesOne.seed = 279;
        //generateValuesOne.generateValues = true;

        //// Push to database
        //generatedValues.PushData(generateValuesOne);
        //controllerValues.PushData(controllerOne);
    }

    // Update is called once per frame
    void Update()
    {
        // Upload
        if (end && Input.GetKeyDown(KeyCode.U))
        {
            // Push to database
            generatedValues.PushData(generateValuesOne);
            controllerValues.PushData(controllerOne);

            UnityEditor.EditorApplication.isPlaying = false;
        }

        //  Generate intial values
        if (!valuesGenerated)
        {
            generateValuesOne.seed = seeds[i];
            generateValuesTwo.seed = seeds[j];
            generateValuesOne.generateValues = true;
            generateValuesTwo.generateValues = true;
            valuesGenerated = true;
        }

        // In different indexes
        if (j != i)
        {
            // A selected
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Increase ranking
                ranking[i] += 1;

                // Increase the index j
                if (j < seeds.Length - 1)
                {
                    j++;
                    newValuesTwo = true;
                    StartCoroutine(FadeOutImage(timer));
                }

                // Increase index i
                else
                {
                    // Less the length of array
                    if (i < seeds.Length)
                    {
                        // Set i and j
                        i++;
                        j = i;

                        // Less the length of array
                        if (j < seeds.Length)
                        {
                            // New values generate
                            newValuesOne = true;
                            newValuesTwo = true;
                            StartCoroutine(FadeOutImage(timer));
                        }
                    }
                }
            }

            // B selected
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Increase ranking
                ranking[j] += 1;

                // Increase the index j
                if (j < seeds.Length - 1)
                {
                    j++;
                    newValuesTwo = true;
                    StartCoroutine(FadeOutImage(timer));
                }

                // Increase index i
                else
                {
                    // Less the length of array
                    if (i < seeds.Length)
                    {
                        // Set i and j
                        i++;
                        j = i;

                        // Less the length of array
                        if (j < seeds.Length)
                        {
                            // New values generate
                            newValuesOne = true;
                            newValuesTwo = true;
                            StartCoroutine(FadeOutImage(timer));
                        }
                    }
                }
            }
        }

        // Same index
        else
        {
            // Increase index
            if (j < seeds.Length - 1)
            {
                j++;
                generateValuesTwo.seed = seeds[j];
                generateValuesTwo.generateValues = true;
            }

            // End of search
            else if (!end)
            {
                int max = 0;
                for (int i = 0; i < ranking.Length; i++)
                {
                    if (ranking[i] > max) max = ranking[i];
                }

                // Loop through rankings
                for (int i = 0; i < ranking.Length; i++)
                {
                    // If max ranked push data
                    if (ranking[i] == max)
                    {
                        // Generate the seed values at max ranked for pushing
                        generateValuesOne.seed = seeds[i];
                        generateValuesOne.generateValues = true;

                        //// Push to database
                        //generatedValues.PushData(generateValuesOne);
                        //controllerValues.PushData(controllerOne);
                    }
                }

                // End and quit
                end = true;
            }
        }

        // Generate new values
        if (newValuesOne)
        {
            // Set the seed
            generateValuesOne.seed = seeds[i];

            // Generate values
            generateValuesOne.generateValues = true;
            newValuesOne = false;
        }

        // Generate new values
        if (newValuesTwo)
        {
            // Set the seed
            generateValuesTwo.seed = seeds[j];

            // Generate values
            generateValuesTwo.generateValues = true;
            newValuesTwo = false;
        }
    }

    // Fade out image
    private IEnumerator FadeOutImage(float timeSpeed)
    {
        round.color = new Color(round.color.r, round.color.g, round.color.b, 1);
        fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 1);
        while (fader.color.a > 0.0f)
        {
            round.color = new Color(round.color.r, round.color.g, round.color.b, round.color.a - (Time.deltaTime * timeSpeed));
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, fader.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
}
