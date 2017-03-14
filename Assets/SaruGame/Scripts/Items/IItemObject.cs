using UnityEngine;
using System.Collections;

namespace Saru.Items
{
    public interface IItemObject
    {
        ItemType ItemType { get; }
        void PickupItem();

    }
}