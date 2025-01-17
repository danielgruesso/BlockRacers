using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
///  Changes the users chosen car
/// </summary>
public class SwapCars : MonoBehaviour
{
    #region Fields

    // Singleton
    public static SwapCars instance;

    // Index of the currently active prefab & livery
    public static int currentLiveryIndex;

    public int currentPrefabIndex;

    // Array of prefabs to swap between
    public GameObject[] prefabs;

    // Our UI elements for the showroom
    public TextMeshProUGUI carName;

    public Slider engineSlider, handlingSlider, boostSlider;

    // Our available colours for each model
    public Material[] camaroLivery;
    public Material[] fordGTLivery;

    public Material[] ferrariLivery;

    // Car prefabs
    public GameObject car1, car2, car3;

    // Reference to the currently instantiated prefab
    private GameObject currentPrefab;

    // Set the spawnpoint 
    private Vector3 spawnPoint = new Vector3(89.17f, 0.4f, -9.7f);

    // Reference the global manager
    private GlobalManager globalManager;

    // Platform
    [SerializeField] private GameObject platform;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes needed objects
    /// </summary>
    private void Start()
    {
        // Singleton
        instance = this;
        // Finds our global manager
        globalManager = GameObject.FindWithTag("GlobalManager").GetComponent<GlobalManager>();
        // Instantiate the initial prefab
        currentPrefab = Instantiate(prefabs[currentPrefabIndex], spawnPoint, transform.rotation, transform);
        platform.transform.position = new Vector3(currentPrefab.transform.position.x,
            currentPrefab.transform.position.y - 0.45f, currentPrefab.transform.position.z);
    }

    /// <summary>
    /// Changes to the next car in the list
    /// </summary>
    public void NextCar()
    {
        // Destroy the current prefab
        Destroy(currentPrefab);
        // Increment the index to switch to the next prefab
        currentPrefabIndex++;
        if (currentPrefabIndex >= prefabs.Length)
        {
            currentPrefabIndex = 0;
        }

        // Instantiate the next prefab in the array
        currentPrefab = Instantiate(prefabs[currentPrefabIndex], spawnPoint, transform.rotation, transform);

        // Logic for handling the car stats and other UI information
        // Array Index is as follows: 
        // 0 = Chevrolet Camaro (Nitro Nova GTL)
        // 1 = Ford GT (Turbo Storm GT)
        // 2 = Ferrari (Star Stream S6)
        // Names are subject to change

        switch (currentPrefabIndex)
        {
            case 0:
                carName.text = "Nitro Nova GTL";
                engineSlider.value = globalManager.engineLevel;
                handlingSlider.value = globalManager.handlingLevel;
                boostSlider.value = globalManager.nosLevel;
                // Actively select this car
                globalManager.playerCar = car1;
                break;
            case 1:
                carName.text = "Turbo Storm GT";
                engineSlider.value = globalManager.engineLevel;
                handlingSlider.value = globalManager.handlingLevel;
                boostSlider.value = globalManager.nosLevel;
                // Actively select this car
                globalManager.playerCar = car2;
                break;
            case 2:
                carName.text = "Star Stream S6";
                engineSlider.value = globalManager.engineLevel;
                handlingSlider.value = globalManager.handlingLevel;
                boostSlider.value = globalManager.nosLevel;
                // Actively select this car
                globalManager.playerCar = car3;
                break;
        }

        // Play our menu select audio
        GarageMenu.instance.PlayMenuSelect();
    }

    /// <summary>
    /// Changes to the previous car in the list
    /// </summary>
    public void PreviousCar()
    {
        // Destroy the current prefab
        Destroy(currentPrefab);
        // Decrement the index to switch to the next prefab  
        if (currentPrefabIndex > 0)
        {
            currentPrefabIndex--;
        }
        else
        {
            currentPrefabIndex = 2;
        }

        // Instantiate the next prefab in the array
        currentPrefab = Instantiate(prefabs[currentPrefabIndex], spawnPoint, transform.rotation, transform);

        // Logic for handling the car stats and other UI information
        // Array Index is as follows: 
        // 0 = Chevrolet Camaro (Nitro Nova GTL)
        // 1 = Ford GT (Turbo Storm GT)
        // 2 = Ferrari (Star Stream S6)
        // Names are subject to change

        switch (currentPrefabIndex)
        {
            case 0:
                carName.text = "Nitro Nova GTL";
                engineSlider.value = globalManager.engineLevel;
                handlingSlider.value = globalManager.handlingLevel;
                boostSlider.value = globalManager.nosLevel;
                // Actively select this car
                globalManager.playerCar = car1;
                break;

            case 1:
                carName.text = "Turbo Storm GT";
                engineSlider.value = globalManager.engineLevel;
                handlingSlider.value = globalManager.handlingLevel;
                boostSlider.value = globalManager.nosLevel;
                // Actively select this car
                globalManager.playerCar = car2;
                break;

            case 2:
                carName.text = "Star Stream S6";
                engineSlider.value = globalManager.engineLevel;
                handlingSlider.value = globalManager.handlingLevel;
                boostSlider.value = globalManager.nosLevel;
                // Actively select this car
                globalManager.playerCar = car3;
                break;
        }

        // Play our menu select audio
        GarageMenu.instance.PlayMenuSelect();
    }

    /// <summary>
    /// Changes the livery for each car based on the options available for each
    /// </summary>
    public void ChangeLivery()
    {
        switch (currentPrefabIndex)
        {
            case 0:
            {
                currentLiveryIndex++;
                if (currentLiveryIndex > 3)
                {
                    currentLiveryIndex = 0;
                }

                currentPrefab.GetComponentInChildren<MeshRenderer>().material = camaroLivery[currentLiveryIndex];
                break;
            }
            case 1:
            {
                currentLiveryIndex++;
                if (currentLiveryIndex > 3)
                {
                    currentLiveryIndex = 0;
                }

                currentPrefab.GetComponentInChildren<MeshRenderer>().material = fordGTLivery[currentLiveryIndex];
                break;
            }
            case 2:
            {
                currentLiveryIndex++;
                if (currentLiveryIndex > 3)
                {
                    currentLiveryIndex = 0;
                }

                currentPrefab.GetComponentInChildren<MeshRenderer>().material = ferrariLivery[currentLiveryIndex];
                break;
            }
        }

        // Play our menu select audio
        GarageMenu.instance.PlayMenuSelect();
    }

    /// <summary>
    /// Checks for arrow key input navigation
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousCar();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextCar();
        }
    }

    #endregion
}