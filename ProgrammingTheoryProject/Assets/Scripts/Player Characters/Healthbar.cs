using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image currentHealthBar;

    [SerializeField] ActivePlayerManager activePlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealthBar = GetComponent<Image>();
    }

    public void UpdateHealthbar(float health, float maxHealth)
    {
        float ratio = health / maxHealth;
        Debug.Log("Ratio after taking damage: " + ratio);
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
}
