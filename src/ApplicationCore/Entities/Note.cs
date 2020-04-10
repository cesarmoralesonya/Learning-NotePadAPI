using Ardalis.GuardClauses;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Note: BaseEntity
    {
        private Note()
        {
            // required by EF
        }
        public Note(int id, string title, string body, DateTime dateTime)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.NullOrEmpty(title, nameof(title));
            Guard.Against.NullOrEmpty(body, nameof(body));
            Guard.Against.Null(dateTime, nameof(dateTime));

            Id = id;
            Title = title;
            Body = body;
            DateTime = dateTime;
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}