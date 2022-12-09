using Entities;
using UnityEngine;

namespace Factory
{
    public interface IMonsterFactory
    {
        Monster Create(Monster prefab);
    }
}