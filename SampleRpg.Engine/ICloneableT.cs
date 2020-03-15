using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine
{
    public interface ICloneable<T>
    {
        T Clone ( bool deepClone );
    }
}
