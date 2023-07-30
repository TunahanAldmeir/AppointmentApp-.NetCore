using BusinesLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Concrete
{
    public class AppointmentManager : IAppointmentServices
    {
        IAppointmentDal _appointmentdal;
        public AppointmentManager(IAppointmentDal appointmentDal)
        {
            _appointmentdal = appointmentDal;
        }
        public void AddAppointment(Appointment appointment)
        {
            _appointmentdal.Insert(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _appointmentdal.Delete(appointment);
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentdal.GetAll();
        }
        public List<Appointment> GetAllAppointmentsByUserId(int id)
        {
            return _appointmentdal.GetAll(x => x.Id == id);
        }

		public List<Appointment> GetAppoinmetWithUser()
		{
			return _appointmentdal.GetAppoinmetWithUser();
		}

		public Appointment GetAppointmentById(int id)
        {
            return _appointmentdal.GetByID(id);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _appointmentdal.Update(appointment);    
        }
    }
}
