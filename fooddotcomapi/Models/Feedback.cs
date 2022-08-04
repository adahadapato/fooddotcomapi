namespace fooddotcomapi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
