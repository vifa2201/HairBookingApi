using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Connections;

namespace HairBookingApi.Models;


   public class TimeModel
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public bool Available { get; set; } = true;
        public DateTime Date { get; set; }

     
    }