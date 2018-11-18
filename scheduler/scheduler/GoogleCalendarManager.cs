﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace scheduler
{
	class GoogleCalendarManager
	{
		static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
		static string ApplicationName = "Google Calendar API .NET Quickstart";
		private static Events events;
		private static CalendarService service;

		public static void init()
		{
			UserCredential credential;

			using (var stream =
				new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
			{
				// The file token.json stores the user's access and refresh tokens, and is created
				// automatically when the authorization flow completes for the first time.
				string credPath = "token.json";
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
			}

			// Create Google Calendar API service.
			service = new CalendarService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			UpdateGoogleCalendar();

		}
		// Define parameters of request.
		static private void UpdateGoogleCalendar()
		{
			EventsResource.ListRequest request = service.Events.List("primary");
			request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            events = request.Execute();
		}

        static public List<DateTime> GetDateTimes()
        {
            if (events.Items != null && events.Items.Count > 0)
                return events.Items
                    .Where(e => e.Start.DateTime.HasValue)
                    .Select(e => e.Start.DateTime.Value).ToList();
            return new List<DateTime>();
        }

        static public List<string> GetTasksOfDay(DateTime dt)
        {
            return events.Items
                .Where(e => e.Start.DateTime.HasValue)
                .Where(e => e.Start.DateTime.Value.Day == dt.Day && e.Start.DateTime.Value.Month == dt.Month 
                    && e.Start.DateTime.Value.Year == dt.Year)
                .Select(e => e.Start.DateTime.Value.ToString("HH:mm ") + e.Summary).ToList<string>();
        }

        static public bool ExistTaskInADay()
        {
			UpdateGoogleCalendar();
            return events.Items
                .Where(e => e.Start.DateTime.HasValue)
                .Where(e => (e.Start.DateTime.Value - DateTime.Now).TotalHours < 24)
                .ToList().Count != 0;
        }
    }
}
