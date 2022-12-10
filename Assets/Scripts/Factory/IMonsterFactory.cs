using Entities;
using Entities.Monster;
using UnityEngine;

namespace Factory
{
    public interface IMonsterFactory
    {
        Monster Create(Monster prefab);
    }
}