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
        public Note(string title, string body, DateTime dateTime)
        {
            Guard.Against.NullOrEmpty(title, nameof(title));
            Guard.Against.NullOrEmpty(body, nameof(body));
            Guard.Against.Null(dateTime, nameof(dateTime));

            Title = title;
            Body = body;
            DateTime = dateTime;
        }

        public string Title { get; set; }
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}