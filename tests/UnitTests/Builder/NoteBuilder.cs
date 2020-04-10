using ApplicationCore.Entities;
using System;

namespace UnitTests.Builder
{
    public class NoteBuilder
    {
        private Note _note;

        public int TestId = 12345;
        public string TestTitle = "Test title";
        public string TestBody = "Test Body";
        public DateTime TestDateTime = DateTime.MinValue;

        public NoteBuilder()
        {
            _note = WithDefaultValues();
        }

        public Note WithDefaultValues()
        {
            _note = new Note(TestId, TestTitle, TestBody, TestDateTime);
            return _note;
        }
    }
}
