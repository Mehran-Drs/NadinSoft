using FluentValidation;
using NadinSoft.Application.CQRS.Products.Commands.CreateProduct;
using NadinSoft.Application.CQRS.Products.Commands.EditProduct;

namespace NadinSoft.Application.Validations.Products
{
    public class EditProductCommandValidation : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name Can Not Be Empty")
                .MaximumLength(150)
                .WithMessage("Maximum Length Is 150 Character");

            RuleFor(x => x.ManufactureEmail)
                .NotEmpty()
                .WithMessage("Manufacture Email Can Not Be Empty")
                .MaximumLength(250)
                .WithMessage("Maximum Length Is 250 Character");

            RuleFor(x => x.ProduceDate)
                .NotEmpty()
                .WithMessage("Please Insert Produce Date");

            RuleFor(x => x.ManufacturePhone)
                .NotEmpty()
                .WithMessage("Manufacture Phone Can Not Be Empty")
                .MaximumLength(11)
                .WithMessage("Maximum Length Is 11 Character");


        }
    }
}
