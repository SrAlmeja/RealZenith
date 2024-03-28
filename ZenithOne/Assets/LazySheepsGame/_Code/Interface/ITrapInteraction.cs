using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

public interface ITrapInteraction 
{
   public TypeOfGadget gadgetType { get; set; }
   public void DamagePlayer(float dmg);
   public void DestroyTrap(TypeOfGadget gadgetType);
   public void DisableTrap(TypeOfGadget gadgetType);
   public void EnableTrap();
}
