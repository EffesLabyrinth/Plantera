using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoison
{
    void Poisoned(float duration, int damageOverTime);
}
