using System;
using System.Collections.Generic;
using Attributable.Attributes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Attributable
{
    [CreateAssetMenu(fileName = "Unit Data", menuName = "Data/Unit Data", order = 1)]
    public class UnitData : SerializedScriptableObject, IAttributable
    {
        [OdinSerialize]
        protected IDictionary<ITag, List<IAttribute>> _attributes = new Dictionary<ITag, List<IAttribute>>();

        protected Dictionary<Type, ITag> _tags = new Dictionary<Type, ITag>();

        public IDictionary<ITag, List<IAttribute>> Attributes => _attributes;

        public event Action<ITag, IAttribute> OnAttributeAdded;

        public event Action<ITag, IAttribute> OnAttributeRemoved;
        

        public TAttribute GetAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        {
            if (_attributes.TryGetValue(_tags[typeof(TTag)], out var attributeList))
            {
                foreach (var attribute in attributeList)
                {
                    if (attribute is TAttribute matchedAttribute)
                    {
                        return matchedAttribute;
                    }
                }
            }

            return default; // Return default value if attribute is not found
        }

        public void AddAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        { 
            if (!_tags.ContainsKey(typeof(TTag)))
            {
                _tags.Add(typeof(TTag) ,Activator.CreateInstance<TTag>());
            }
            
            AddAttribute(_tags[typeof(TTag)],Activator.CreateInstance<TAttribute>());
        }

        public void AddAttribute<TTag, TAttribute>(TTag tag, TAttribute attribute) where TTag : ITag where TAttribute : IAttribute
        {
            if (!_tags.ContainsKey(typeof(TTag)))
            {
                _tags.Add(typeof(TTag), tag);
            }

            if (!_attributes.ContainsKey(tag))
            {
                _attributes.Add(tag, new List<IAttribute>());
            }

            _attributes[tag].Add(attribute);
            OnAttributeAdded?.Invoke(tag, attribute);
        }

        public void RemoveAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        {
            if (_attributes.TryGetValue(_tags[typeof(TTag)], out var attributeList))
            {
                var removedAttributes = attributeList.RemoveAll(attribute => attribute is TAttribute);
                if (removedAttributes > 0)
                {
                    OnAttributeRemoved?.Invoke(_tags[typeof(TTag)], null);
                }
            }
        }

        public bool ContainAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        {
            if (_attributes.TryGetValue(_tags[typeof(TTag)], out var attributeList))
            {
                return attributeList.Exists(attribute => attribute is TAttribute);
            }

            return false;
        }
    }
}