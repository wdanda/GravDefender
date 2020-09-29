using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI HealthText = null;

    private HealthManager HealthManager = null;

    void Start()
    {
        HealthManager = FindObjectOfType<HealthManager>();
        if (HealthManager == null)
        {
            Debug.LogError("Unable to finds HealthManager");
        }
        if (HealthText == null)
        {
            Debug.LogError("No HealthText assigned");
        }
    }

    void Update()
    {
        if (HealthText == null)
        {
            Debug.LogWarning("No health text found");
            return;
        }
        HealthText.text = HealthManager.GetHealth().ToString();
    }

}
