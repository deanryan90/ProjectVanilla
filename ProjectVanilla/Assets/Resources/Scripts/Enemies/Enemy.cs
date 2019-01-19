using System;

public class Enemy : Humanoid, IDamage<float>, IRun, IWalk, IDie
{
    public void Damage(float damageTaken)
    {
        throw new NotImplementedException();
    }
}