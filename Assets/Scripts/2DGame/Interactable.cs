using UnityEngine;

public interface IInteractable
{
    public void Interact(GameObject sentGameObject);
}
public interface IDamagable
{
    public void TakeDamage(int incomeingDamage);
}
public interface IExplode
{
    public void Explode();
}

