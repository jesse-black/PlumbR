using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace PlumbR
{
    /// <summary>
    /// Represents the result of a pipeline operation.
    /// </summary>
    /// <typeparam name="TResult">The type of the successful result.</typeparam>
    [GenerateOneOf]
    public partial class PipelineResult<TResult> : OneOfBase<TResult, ValidationResult, ProblemDetails> { }
}