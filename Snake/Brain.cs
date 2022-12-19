using System.Collections.Generic;
using System.Linq;
using Snake.Strategy;

namespace Snake
{
    static class Brain
    {
        public static IStrategy GetStrategies(IReadOnlyCollection<IStrategy> strategies)
        {
            if (strategies.Count == 0)
                return new NoStrategy();

            if (strategies.Count == 1)
                return strategies.First();

            return new CompositeStrategy(strategies);
        }
    }
}
