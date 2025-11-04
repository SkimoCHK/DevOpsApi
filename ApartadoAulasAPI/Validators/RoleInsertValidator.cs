using ApartadoAulasAPI.DTOs.Roles;
using FluentValidation;
using System.Data;

namespace ApartadoAulasAPI.Validators
{
  public class RoleInsertValidator : AbstractValidator<CreateRoleDto>
  {
   public RoleInsertValidator()
    {
      RuleFor(x => x.Nombre).NotEmpty().WithMessage("El campo del nombre es obligatorio");
      RuleFor(x => x.Clave).NotEmpty().WithMessage("El campo de la clave es obligatorio")
        .Length(4, 50).WithMessage("La clave debe tener una longitud de entre 4 y 50 letras");

    }
  }
}
