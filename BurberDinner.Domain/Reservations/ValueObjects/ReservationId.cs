
using BurberDinner.Domain.Common.Models;
using System;
using System.Collections.Generic;

namespace BurberDinner.Domain.Reservation.ValueObjects
{
    public class ReservationId : ValueObject
    {
        public Guid Value { get; }

        private ReservationId(Guid value)
        {
            Value = value;
        }

        public static ReservationId CreateUnique()
        {
            return new ReservationId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
