using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrikable
{
#nullable enable
    public void GiveDamage(IHittable? target, int damage);
}
