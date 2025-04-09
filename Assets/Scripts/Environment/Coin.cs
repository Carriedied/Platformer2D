using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public event Action<Coin> OnCollected;

    public void Accept(IVisitor visitor)
    {
        OnCollected?.Invoke(this);
        visitor.Visit(this);
    }
}