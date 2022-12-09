﻿using Entities;
using UnityEngine;

namespace Factory
{
    public class MonsterFactory : IMonsterFactory
    {
        public Monster Create(Monster monsterPrefab)
        {
            return Object.Instantiate(monsterPrefab);
        }
    }
}