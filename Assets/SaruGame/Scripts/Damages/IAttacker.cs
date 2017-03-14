using UnityEngine;
using System.Collections;

namespace Saru.Damages
{
    public interface IAttacker
    {
        int OwnerId { get; }
    }
}