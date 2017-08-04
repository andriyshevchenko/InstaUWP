using Cactoos;
using Cactoos.Scalar;
using System.Collections.Generic;

namespace App.Domain
{
    public class WrapConfig : InstaSharp.InstagramConfig
    {
        public WrapConfig(IScalar<Dictionary<string, string>> source)
            : base(source.Value()["client_id"], source.Value()["client_pass"])
        {

        }

        public WrapConfig(Properties.InstagramConfig source)
            :this(new CachedScalar<Dictionary<string, string>>(source))
        {

        }
    }
}
