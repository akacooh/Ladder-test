using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public void SetPosition(Vector3 position);

    public void CheckForBoundaries();
}
