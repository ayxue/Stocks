using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Models
{
    public abstract class DynamicModel: DynamicObject
    {
        protected IDictionary<string, object> _exp;

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (var property in this.GetType().GetProperties())
                yield return property.Name;

            if (_exp != null)
            {
                foreach (var key in _exp.Keys)
                    yield return key;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_exp == null)
                _exp = new ExpandoObject();

            _exp[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_exp == null)
            {
                result = null;
                return false;
            }

            return _exp.TryGetValue(binder.Name, out result);
        }
    }

    public static class DynamicExt
    {
        public static dynamic AsDynamic(this Object o)
        {
            dynamic d = o;
            return d;
        }
    }
}
