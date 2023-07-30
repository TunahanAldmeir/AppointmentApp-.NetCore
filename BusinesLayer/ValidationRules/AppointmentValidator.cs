using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.ValidationRules
{
	public class AppointmentValidator:AbstractValidator<Appointment>
	{
        public AppointmentValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama kısmı boş geçilemez");
            RuleFor(x => x.Description).MaximumLength(35).WithMessage("En fazla 35 karakter girebilirsiniz");
            RuleFor(x => x.AppointmentDate).NotEmpty().WithMessage("Tarih boş olamaz");
            RuleFor(x => x.AppointmentTime).NotEmpty().WithMessage("Saat boş geçilemez");

        }
    }
}
