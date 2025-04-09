using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

public class MedicalKit : MonoBehaviour, ICollectible
{
    public event Action<MedicalKit> OnCollected;

    public void Accept(IVisitor visitor)
    {
        OnCollected?.Invoke(this);
        visitor.Visit(this);
    }
}
