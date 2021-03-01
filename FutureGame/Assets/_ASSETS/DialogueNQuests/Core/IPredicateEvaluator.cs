using UnityEngine;

namespace Finark.Core
{
    public interface IPredicateEvaluator
    {

        bool? Evaluate(string predicate, string[] parameters);

    }
}
