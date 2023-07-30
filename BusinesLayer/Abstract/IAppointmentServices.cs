using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Abstract
{
    public interface IAppointmentServices
    {
        void AddAppointment(Appointment  appointment);
        void DeleteAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);

        List<Appointment> GetAllAppointments();

        Appointment GetAppointmentById(int id);
		List<Appointment> GetAppoinmetWithUser();
	}
}
