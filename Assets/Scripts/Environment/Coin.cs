using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public event Action<Coin> Collected;

    public void Accept(IVisitor visitor)
    {
        Collected?.Invoke(this);
        visitor.Visit(this);
    }
}