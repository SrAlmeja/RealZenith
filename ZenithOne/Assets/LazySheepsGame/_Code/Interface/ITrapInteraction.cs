using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrapInteraction 
{
   public void DamagePlayer(float dmg);
   public void DestroyTrap();
   public void DisableTrap();
   public void EnableTrap();
}
