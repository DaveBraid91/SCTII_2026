using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>()?.ApplyDamage(damage);
    }
}
