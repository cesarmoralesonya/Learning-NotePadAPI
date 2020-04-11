using ApplicationCore.Entities;
using System;

namespace UnitTests.Builder
{
    public class NoteBuilder
    {
        private Note _note;

        public string TestTitle = "Test title";
        public string TestBody = "Test Body";
        public DateTime TestDateTime = DateTime.MinValue;
       
        public string TestTitleUpdate = "Test title Update";
        public string TestBodyUpdate = "Test Body Update";
        public DateTime TestDateTimeUpdate = DateTime.MaxValue;

        public NoteBuilder()
        {
            _note = WithDefaultValues();
        }

        public Note WithDefaultValues()
        {
            _note = new Note(TestTitle, TestBody, TestDateTime);
            return _note;
        }

        public Note UpdateTitleValue()
        {
            _note = new Note(TestTitleUpdate, TestBody, TestDateTime);
            return _note;
        }
    }
}
