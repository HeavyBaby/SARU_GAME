using UnityEngine;
using System.Collections;
using UniRx;

namespace Saru.Players
{
    public interface IPlayerInput
    {
        IReadOnlyReactiveProperty<Vector2> MoveDirectionRP { get; }
        IObservable<bool> OnAttackButtonObservable { get; }        
    }
}