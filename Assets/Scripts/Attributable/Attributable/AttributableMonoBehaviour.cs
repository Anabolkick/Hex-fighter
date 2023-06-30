using System;
using System.Collections.Generic;
using System.Linq;
using Attributable.Attributes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Attributable
{
    public class AttributableMonoBehaviour : SerializedMonoBehaviour, IAttributable
    {
        [OdinSerialize]
        protected IDictionary<ITag, List<IAttribute>> _attributes = new Dictionary<ITag, List<IAttribute>>();

        protected Dictionary<Type, ITag> _tags = new Dictionary<Type, ITag>();

        public IDictionary<ITag, List<IAttribute>> Attributes => _attributes;

        public event Action<ITag, IAttribute> OnAttributeAdded;

        public event Action<ITag, IAttribute> OnAttributeRemoved;


        private void Awake()
        {
            InitializeTags();
        }

        private void InitializeTags()
        {
            foreach (var tag in _attributes.Keys)
            {
                var tagType = tag.GetType();
                if(!_tags.ContainsKey(tagType)) _tags.Add(tagType, tag);
            }
        }

        public TAttribute GetAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        {
            if (_tags.Count < 1) InitializeTags();
            
            var attributeList = GetAttributeList<TTag>();
            if (attributeList != null)
            {
                foreach (var attribute in attributeList)
                {
                    if (attribute is TAttribute matchedAttribute) return matchedAttribute;
                }
            }

            return default; 
        }

        public void AddAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        {
            if (!_tags.ContainsKey(typeof(TTag)))
            {
                _tags.Add(typeof(TTag), Activator.CreateInstance<TTag>());
            }

            AddAttribute(_tags[typeof(TTag)], Activator.CreateInstance<TAttribute>());
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
            if (_tags.Count < 1) InitializeTags();
            
            var attributeList = GetAttributeList<TTag>();
            if (attributeList != null)
            {
                var attribute = attributeList.FirstOrDefault(attribute => attribute.GetType() == typeof(TAttribute));
                
                if(attributeList.Remove(attribute)) OnAttributeRemoved?.Invoke(_tags[typeof(TTag)], attribute);
            }
        }

        public bool ContainAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute
        {
            if (_tags.Count < 1) InitializeTags();

            var attributeList = GetAttributeList<TTag>();
            if (attributeList != null)
            {
                return attributeList.Exists(attribute => attribute is TAttribute);
            }
            
            return false;
        }

        private List<IAttribute> GetAttributeList<TTag>()
        {
            if (_tags.ContainsKey(typeof(TTag)) && _attributes.ContainsKey(_tags[typeof(TTag)]))
            {
                _attributes.TryGetValue(_tags[typeof(TTag)], out var attributesList);
                return attributesList;
            }

            return null;
        }
    }
}
