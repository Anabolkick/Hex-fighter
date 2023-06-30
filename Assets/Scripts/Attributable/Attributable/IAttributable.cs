using System;
using System.Collections.Generic;
using Attributable.Attributes;

namespace Attributable
{
    public interface IAttributable
    {
        IDictionary<ITag, List<IAttribute>> Attributes { get; }

        event Action<ITag, IAttribute> OnAttributeAdded;

        event Action<ITag, IAttribute> OnAttributeRemoved;

        TAttribute GetAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute;
        void AddAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute;

        void AddAttribute<TTag, TAttribute>(TTag tag, TAttribute attribute) where TTag : ITag where TAttribute : IAttribute;

        void RemoveAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute;

        bool ContainAttribute<TTag, TAttribute>() where TTag : ITag where TAttribute : IAttribute;
    }
}