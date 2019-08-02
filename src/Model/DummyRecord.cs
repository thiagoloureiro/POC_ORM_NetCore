using System;

namespace Model
{
    public class DummyRecord
    {
        /// <summary>
        /// The unique Id of the membership record.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// An integer which is incremented whenever the membership record is updated.
        /// </summary>
        public int RevisionNumber { get; private set; }

        /// <summary>
        /// An integer representing the relative order of the elements of a versioned deque.
        /// </summary>
        public int index2 { get; set; }

        /// <summary>
        /// The owner of the versioned deque.
        /// </summary>
        public long Owner { get; set; }

        /// <summary>
        /// The element contained within the versioned deque.
        /// </summary>
        public long Element { get; set; }

        /// <summary>
        /// The external record date time of the record version represented by the header record,
        /// or <see langword="null" /> if the record is an ordinary membership record.
        /// </summary>
        public DateTime? ExternalRecordDateTime { get; set; }

        /// <summary>
        /// The external record date time of the record version represented by the header record,
        /// or <see langword="null" /> if the record is an ordinary membership record.
        /// </summary>
        public DateTime? ExternalRecordDateTime2 { get; set; }

        /// <summary>
        /// The external record date time of the record version represented by the header record,
        /// or <see langword="null" /> if the record is an ordinary membership record.
        /// </summary>
        public DateTime? ExternalRecordDateTime3 { get; set; }

        /// <summary>
        /// The external record date time of the record version represented by the header record,
        /// or <see langword="null" /> if the record is an ordinary membership record.
        /// </summary>
        public DateTime? ExternalRecordDateTime4 { get; set; }

        /// <summary>
        /// The external record date time of the record version represented by the header record,
        /// or <see langword="null" /> if the record is an ordinary membership record.
        /// </summary>
        public DateTime? ExternalRecordDateTime5 { get; set; }

        /* /// <summary>
        /// The internal record date time of the latest transaction to impact this header record,
        /// or <see langword="null" /> if the record is an ordinary membership record.
        /// </summary>
        public DateTime? LatestInternalRecordDateTime { get; set; } */

        /// <summary>
        /// Indicates whether the membership record is a header record.
        /// </summary>

        public DummyRecord()
        {
        }

        public DummyRecord(int i)
        {
            Id = i;
            RevisionNumber = 1;
            index2 = i;
            Owner = i + 10;
            Element = i + 23;
            ExternalRecordDateTime = i % 2 == 0 ? (DateTime?)null : DateTime.UtcNow;
            ExternalRecordDateTime2 = i % 3 == 0 ? (DateTime?)null : DateTime.UtcNow;
            ExternalRecordDateTime3 = i % 4 == 0 ? (DateTime?)null : DateTime.UtcNow;
            ExternalRecordDateTime4 = i % 3 == 1 ? (DateTime?)null : DateTime.UtcNow;
            ExternalRecordDateTime5 = i % 4 == 1 ? (DateTime?)null : DateTime.UtcNow;
        }
    }
}