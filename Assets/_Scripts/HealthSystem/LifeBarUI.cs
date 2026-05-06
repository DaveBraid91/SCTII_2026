using UnityEngine;
using UnityEngine.UI;

public class LifeBarUI : MonoBehaviour
{
    [SerializeField] private Image lifeBar;

    private Transform _camTransform;

    private void Start()
    {
        _camTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_camTransform);
    }

    public void UpdateLifeBar(BaseHealth characterHealth)
    {
        var currentHealth = characterHealth.CurrentHealth;
        var maxHealth = characterHealth.MaxHealth;
        var lifePercentage = Mathf.Clamp01(currentHealth/maxHealth);
        lifeBar.fillAmount = lifePercentage;
    }
}
