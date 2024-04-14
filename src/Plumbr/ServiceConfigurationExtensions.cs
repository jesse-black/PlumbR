using PlumbR;
using PlumbR.Behaviors;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfigurationExtensions
    {
        /// <summary>
        /// Adds FluentValidation validation behavior for all IPipelineRequest<> request types in the assembly.
        /// </summary>
        public static MediatRServiceConfiguration AddValidationBehaviorForAssemblyContaining<T>(this MediatRServiceConfiguration cfg)
        {
            return cfg.AddPipelineBehaviorForAssemblyContaining<T>(typeof(ValidationBehavior<,>));
        }

        /// <summary>
        /// Adds the behavior for all IPipelineRequest<> request types in the assembly.
        /// </summary>
        public static MediatRServiceConfiguration AddPipelineBehaviorForAssemblyContaining<T>(this MediatRServiceConfiguration cfg, Type behaviorType)
        {
            var requestTypes = typeof(T).Assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineRequest<>)))
                .ToList();
            foreach (var requestType in requestTypes)
            {
                var resultType = requestType.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineRequest<>))
                    ?.GetGenericArguments()
                    ?.FirstOrDefault();
                if (resultType != null)
                {
                    var genericBehaviorType = behaviorType.MakeGenericType(requestType, resultType);
                    cfg.AddBehavior(genericBehaviorType);
                }
            }
            return cfg;
        }
    }
}