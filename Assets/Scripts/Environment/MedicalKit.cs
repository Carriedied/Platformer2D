using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

public class MedicalKit : MonoBehaviour, ICollectible
{
    public event Action<MedicalKit> Collected;

    public void Accept(IVisitor visitor)
    {
        Collected?.Invoke(this);
        visitor.Visit(this);
    }
}
