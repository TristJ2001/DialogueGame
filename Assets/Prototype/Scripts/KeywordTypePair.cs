using System;
using UnityEngine;

namespace Prototype.Scripts
{
    [Serializable]
    public class KeywordTypePair
    {
        [SerializeField]
        private string keyword;
        [SerializeField]
        private EntityType type;

        public KeywordTypePair(string keyword, EntityType type)
        {
            this.keyword = keyword;
            this.type = type;
        }

        public string Keyword
        {
            get => keyword;
        }

        public EntityType Type
        {
            get => type; 
        }

    }
}
