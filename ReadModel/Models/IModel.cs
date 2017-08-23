using System;

namespace ReadModel.Models
{
    public interface IModel
    {
        string Filename { get; }
        long CurrentSequenceId { get; }
        DateTimeOffset ModelCreatedDate { get; }
        void AddEvent<T>(T e);
    }
}
