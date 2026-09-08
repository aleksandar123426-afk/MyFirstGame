using UnityEngine;

public class HealthbarBehaviour : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitpoints = 5;
    public HealthbarBehaviour Healthbar;
    

    void Start()
    {
        Hitpoints = MaxHitpoints;
        // if an external healthbar instance is assigned use it, otherwise use this
        var target = Healthbar != null ? Healthbar : this;
        target.SetHealth(Hitpoints, MaxHitpoints);
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        var target = Healthbar != null ? Healthbar : this;
        target.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Provide a simple SetHealth implementation so other objects can update a UI healthbar.
    public void SetHealth(float current, float max)
    {
        // Placeholder: implement UI update logic here (e.g. adjust fill amount on an Image)
        // Keeping it empty avoids compile errors when no UI is present.
    }
}