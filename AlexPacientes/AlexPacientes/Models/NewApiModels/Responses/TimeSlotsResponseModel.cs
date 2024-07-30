using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class TimeSlotsResponseModel : ResponseBaseModel<TimeSlotListModel>
    {
    }

    public class TimeSlotListModel
    {
        public List<TimeSlotModel> Items { get; set; }
    }

    public class TimeSlotModel
    {
        public long Day { get; set; }
        public List<AvailabilityTimeSlotModel> Availability { get; set; }
    }

    public class AvailabilityTimeSlotModel
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public int StartAt { get; set; }
        public int EndAt { get; set; }
        public bool Available { get; set; }
        public string slot_type { get;set; }
        public int minutes { get; set; }
        public long? start_at_miliseconds_utc { get; set; }

        //Local properties
        public DateTime StartAtDateTime
        {
            get
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, StartAt, minutes,0);
            }
        }
    }
}
