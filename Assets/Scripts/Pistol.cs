using UnityEngine;

public class Pistol : Gun
{
    override protected void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
