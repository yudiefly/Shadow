using FluentValidation;

namespace Shadow.WebApi.Models.Validators
{
    /// <summary>
    /// 用于 <see cref="ProductCreateViewModel"/> 对象验证器
    /// </summary>
    public class PersonCreateViewModelValidator : AbstractValidator<ProductCreateViewModel>
    {
        public PersonCreateViewModelValidator()
        {
            RuleFor(m => m.NO).NotEmpty();
            RuleFor(m => m.Weight).GreaterThan(0);
        }
    }
}
