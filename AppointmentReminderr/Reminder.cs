using System.Net.Mail;
using System.Net;
using BusinesLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Appointment.AppointmentReminder
{
	public class Reminder:BackgroundService
	{
		private readonly IConfiguration _config;

		private readonly ILogger<Reminder> _logger;
		AppointmentManager apm = new AppointmentManager(new EfAppointmentRepositorycs());


		public Reminder(ILogger<Reminder> logger , IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				CheckAndSendAppointmentReminders();
				await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
			}
		}
		private void CheckAndSendAppointmentReminders()
		{
			

			var appointments = apm.GetAppoinmetWithUser();

			var now = DateTime.Now;
			if(appointments != null) 
			{
				foreach (var appointment in appointments)
				{
					if (!appointment.IsSentEmail && (appointment.AppointmentDate - now) < TimeSpan.FromDays(1))
					{
						string emailTemplatePath = "C:\\Users\\Tunahan\\source\\repos\\Appointment\\AppointmentReminderr\\MailMesaj\\ha.html";
						string emailTemplate = File.ReadAllText(emailTemplatePath);

						string formattedBody = emailTemplate
							.Replace("{Name}", appointment.User.Name) // Replace "{Name}" with the appropriate placeholder in your template
							.Replace("{AppointmentDate}", appointment.AppointmentDate.ToString("dd.MM.yyyy")+" "+appointment.AppointmentTime.ToString()); // Replace "{AppointmentDate}" with the appropriate placeholder in your template and format the date as needed

						SendEmail(appointment.User.Email, "Appointment Reminder", formattedBody);
						appointment.IsSentEmail = true;
						apm.UpdateAppointment(appointment);
					}
				}
			}
		}
		private void SendEmail(string receiverEmail, string subject, string body)
		{
			
			var senderEmail = _config["SENDER_EMAIL"];
			var password = _config["EMAIL_PASSWORD"];

			var smtpClient = new SmtpClient("smtp.gmail.com", 587)
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(senderEmail, password),
				EnableSsl = true
			};

			var message = new MailMessage(senderEmail, receiverEmail, subject, body)
			{
				IsBodyHtml = true	
			};

			try
			{
				smtpClient.Send(message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error sending email.");
			}
			finally
			{
				message.Dispose();
				smtpClient.Dispose();
			}
		}

	}
}
