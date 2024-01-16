using System.Collections.Generic;

namespace FractalTree
{
    public interface ITree
    {
        public List<IBranch> Generate();
    }
}