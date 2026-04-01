using System;
using Adic.Injection;

namespace Adic {
    /// <summary>
    /// Defines a factory of instances.
    /// </summary>
    public interface IFactory {
        /// <summary>
        /// Creates an instance of the object of the type created by the factory.
        /// </summary>
        /// <param name="context">Injection sceneContext.</param>
        /// <returns>The instance.</returns>
        object Create(InjectionContext context);
    }
}