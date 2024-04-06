using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace PlumbR
{
    [GenerateOneOf]
    public partial class PipelineResult<TResult> : OneOfBase<TResult, ValidationResult, ProblemDetails> { }
}